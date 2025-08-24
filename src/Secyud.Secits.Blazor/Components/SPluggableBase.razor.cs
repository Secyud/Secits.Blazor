using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Services;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SPluggableBase : IHasTheme, IHasSize, IHasCustomCss, IPluggable
{
    private readonly SSettingMaster _settingMaster;
    private readonly ClassStyleBuilder _classStyleBuilder;
    private readonly List<DirtyParameter> _dirtyParameters = [];

    protected SPluggableBase()
    {
        _settingMaster = new SSettingMaster(this);
        _classStyleBuilder = new ClassStyleBuilder(BuildClassStyle);
    }

    protected virtual string ComponentName => "s";

    [Inject]
    private IDirtyParameterService DirtyParameterService { get; set; } = null!;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _dirtyParameters.AddRange(DirtyParameterService
            .GetDirtyParameters(this));
    }


    #region Parameters

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (_dirtyParameters.Any(parameter => parameter.CheckComponentDirty(this, parameters)))
        {
            _classStyleBuilder.SetDirty();
        }

        var container = new ParameterContainer(parameters);
        BeforeParametersSet(container);
        await base.SetParametersAsync(parameters);
        await Task.WhenAll(container.ParameterTasks);
    }

    protected virtual void BeforeParametersSet(ParameterContainer parameters)
    {
    }

    #endregion


    #region Internal

    public new void StateHasChanged()
    {
        base.StateHasChanged();
    }

    public new Task InvokeAsync(Action action)
    {
        return base.InvokeAsync(action);
    }

    public new Task InvokeAsync(Func<Task> action)
    {
        return base.InvokeAsync(action);
    }

    #endregion

    #region ClassStyle

    protected virtual void BuildClassStyle(ClassStyleContext context)
    {
        if (!string.IsNullOrEmpty(ComponentName))
            context.Class.Append(ComponentName);

        foreach (var extendClassStyleBuilder in ClassStyleBuilders)
        {
            extendClassStyleBuilder.BuildExtendClassStyle(context);
        }

        foreach (var dirtyParameter in _dirtyParameters)
        {
            dirtyParameter.BuildComponentClassStyle(this, context);
        }
    }

    protected string? GetClass() => _classStyleBuilder.Class;

    protected string? GetStyle() => _classStyleBuilder.Style;

    #endregion

    #region Settings

    public SSettings<IExtendClassStyleBuilder> ClassStyleBuilders { get; } = new();

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
    public SValue Width { get; set; }

    [Parameter]
    public SValue Height { get; set; }

    #endregion
}