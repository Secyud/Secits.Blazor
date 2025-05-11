using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class SSettingComp<TComponent> : ComponentBase, IAsyncDisposable
    where TComponent : SComponentBase
{
    [CascadingParameter(Name = nameof(MasterComponent))]
    public TComponent? MasterComponent { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter<TComponent>(nameof(MasterComponent), ApplySetting);

        await base.SetParametersAsync(parameters);
    }

    protected void ApplySetting(TComponent? component)
    {
        if (component == MasterComponent) return;
        ForgoSetting();
        MasterComponent = component;
        ApplySetting();
    }

    protected virtual void ApplySetting()
    {
    }

    protected virtual void ForgoSetting()
    {
    }

    public virtual ValueTask DisposeAsync()
    {
        if (MasterComponent is not null &&
            !MasterComponent.IsDisposed)
        {
            ForgoSetting();
        }

        return ValueTask.CompletedTask;
    }
}