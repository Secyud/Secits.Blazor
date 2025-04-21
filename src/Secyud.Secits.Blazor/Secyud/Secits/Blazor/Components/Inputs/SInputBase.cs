using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Basic;
using Secyud.Secits.Blazor.Parameters;
using Secyud.Secits.Utils;

namespace Secyud.Secits.Blazor.Components;

public abstract class SInputBase<TValue> : SBasicComp,
    IValueComponent<TValue>, IColorComponent
{
    protected override string ComponentName => "input";

    #region Parameters

    [Parameter]
    public ColorType Color { get; set; } = ColorType.Primary;

    [Parameter]
    public InputChangeMode ChangeMode { get; set; } = InputChangeMode.OnInput;

    [Parameter]
    public TValue? Value { get; set; }

    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    #endregion

    #region Mask

    [CascadingParameter]
    public IInputMask? InputMask { get; set; }

    private bool _isMasked;

    public bool IsMasked => _isMasked;

    /// <summary>
    /// 设置遮罩，当值成功改变时，需要反应遮罩后的字符串。
    /// </summary>
    /// <exception cref="InvalidCastException"></exception>
    private void SetFieldWithMask()
    {
        if (!TryConvertToField(_currentValue, out var output))
        {
            throw new InvalidCastException(
                $"Type {typeof(TValue)} value {_currentValue} cast to string failed!");
        }

        if (InputMask is not null &&
            InputMask.TryMaskValue(output, out var masked))
        {
            _isMasked = true;
            _currentFieldOrigin = masked;
            _currentFieldMasked = masked;
        }
        else
        {
            _isMasked = false;
            _currentFieldOrigin = output;
        }
    }

    #endregion

    #region DelegateValueComponent

    [CascadingParameter]
    public IValueDelegateComponent<TValue>? DelegateValueComponent { get; set; }

    private void UnbindValueComponentDelegate()
    {
        if (DelegateValueComponent is not null)
        {
            DelegateValueComponent.ValueSetAction = null;
        }
    }

    private void BindValueComponentDelegate(IValueDelegateComponent<TValue>? component)
    {
        UnbindValueComponentDelegate();
        if (component is not null)
        {
            component.ValueSetAction = OnParameterChanged;
        }

        DelegateValueComponent = component;
    }

    #endregion

    #region Render

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, ElementName);
        builder.AddAttribute(1, "class", GetClass());
        builder.AddAttribute(2, "style", GetStyle());
        builder.AddAttribute(3, "id", Id);
        builder.AddAttribute(4, "name", Name);
        builder.AddContent(5, GenerateChildContent());
        builder.CloseElement();
    }

    protected override RenderFragment GenerateChildContent() => builder =>
    {
        builder.AddContent(0, GenerateInputField());
    };

    protected RenderFragment GenerateInputField() => builder =>
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, Attributes);
        builder.AddAttribute(2, "value", CurrentField);

        builder.AddAttribute(3, ChangeMode switch
            {
                InputChangeMode.OnInput => "oninput",
                InputChangeMode.OnChange => "onchange",
                _ => "oninput"
            },
            EventCallback.Factory.CreateBinder<string?>(this,
                t => CurrentField = t, CurrentField)
        );
        builder.SetUpdatesAttributeName("value");
        builder.CloseElement();
    };

    #endregion

    #region Delayer

    private ValueDelayer<TValue>? _delayer;

    [Parameter]
    public int DelayInterval { get; set; } = 200;

    private void UnbindValueDelayer()
    {
        if (_delayer is null) return;
        _delayer.Delayed -= OnValueChangedDelay;
        _delayer.Dispose();
        _delayer = null;
    }

    private void BindValueDelayer(int timeInterval)
    {
        UnbindValueDelayer();
        _delayer = new ValueDelayer<TValue>(timeInterval);
        _delayer.Delayed += OnValueChangedDelay;
    }

    #endregion

    #region Initialize

    protected override void OnInitialized()
    {
        BindValueDelayer(DelayInterval);
        base.OnInitialized();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue<IValueDelegateComponent<TValue>>(
                nameof(DelegateValueComponent), out var component))
            BindValueComponentDelegate(component);

        if (parameters.TryGetValue<int>(nameof(DelayInterval), out var delayInterval))
            BindValueDelayer(delayInterval);

        await base.SetParametersAsync(parameters);

        if (parameters.TryGetValue<TValue?>(
                nameof(Value), out var parameter))
        {
            OnParameterChanged(parameter);
        }
    }

    protected override ValueTask HandleDisposeAsync()
    {
        UnbindValueComponentDelegate();
        UnbindValueDelayer();
        return base.HandleDisposeAsync();
    }

    #endregion

    #region Events

    private string? _currentFieldOrigin;
    private string? _currentFieldMasked;
    private TValue? _currentValue;
    private bool _parsingFailed;
    public bool ParsingFailed => _parsingFailed;

    /// <summary>
    /// 用于从外部获取参数时设置
    /// </summary>
    public TValue? CurrentValue
    {
        get => _currentValue;
        set
        {
            _currentValue = value;
            _parsingFailed = false;
            OnValueChanged();
        }
    }

    public string? CurrentField
    {
        get => _isMasked
            ? _currentFieldMasked
            : _currentFieldOrigin;
        set
        {
            _parsingFailed = !TryConvertToValue(value, out _currentValue);
            if (_parsingFailed) return;
            OnValueChanged();
        }
    }

    /// <summary>
    /// 当输入变化，并且值成功更新时，通知上级事件。
    /// </summary>
    private void OnValueChanged()
    {
        OnParameterChanged(_currentValue);
        _delayer?.Update(_currentValue);
    }

    private void OnValueChangedDelay(object? sender, TValue? value)
    {
        InvokeAsync(async () =>
        {
            await ValueChanged.InvokeAsync(value);
            if (DelegateValueComponent is not null)
                await DelegateValueComponent
                    .ValueChanged.InvokeAsync(value);
        });
    }

    private void OnParameterChanged(TValue? value)
    {
        _currentValue = value;
        _parsingFailed = false;
        SetFieldWithMask();
        Value = _currentValue;
        if (DelegateValueComponent is null) return;
        DelegateValueComponent.Value = _currentValue;
    }


    protected abstract bool TryConvertToValue(string? value, out TValue? output);

    protected virtual bool TryConvertToField(TValue? value, out string? output)
    {
        output = value?.ToString();
        return true;
    }

    #endregion
}