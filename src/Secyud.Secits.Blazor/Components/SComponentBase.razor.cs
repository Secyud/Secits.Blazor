using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract partial class SComponentBase : IScsTheme, IScsSize
{
    private readonly SSettingMaster _settingMaster;
    private readonly ClassStyleBuilder _classStyleBuilder;
    private readonly List<DirtyParameter> _dirtyParameters = [];

    protected SComponentBase()
    {
        _settingMaster = new SSettingMaster(this);
        _classStyleBuilder = new ClassStyleBuilder(BuildClassStyle);
    }

    protected virtual string ComponentName => "s";

    [Inject]
    private DirtyParameterService DirtyParameterService { get; set; } = null!;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _dirtyParameters.AddRange(DirtyParameterService
            .GetDirtyParameters(this));
    }


    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (_dirtyParameters.Any(parameter => parameter.CheckComponentDirty(this, parameters)))
        {
            _classStyleBuilder.SetDirty();
        }

        await base.SetParametersAsync(parameters);
    }


    #region Internal

    internal void MasterStateHasChanged()
    {
        StateHasChanged();
    }

    internal Task MasterInvokeAsync(Action action)
    {
        return InvokeAsync(action);
    }

    internal Task MasterInvokeAsync(Func<Task> action)
    {
        return InvokeAsync(action);
    }

    #endregion

    #region ClassStyle

    protected virtual void BuildClassStyle(ClassStyleContext context)
    {
        if (!string.IsNullOrEmpty(ComponentName))
            context.Class.Append(ComponentName);
        foreach (var dirtyParameter in _dirtyParameters)
        {
            dirtyParameter.BuildComponentClassStyle(this, context);
        }
    }

    protected string? GetClass() => _classStyleBuilder.Class;

    protected string? GetStyle() => _classStyleBuilder.Style;

    #endregion

    #region Parameters

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }

    [Parameter]
    public SValue Width { get; set; }

    [Parameter]
    public SValue Height { get; set; }

    #endregion
}