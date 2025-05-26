using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// Represents a base class for settings components that are associated with a master component.
/// This class provides functionality to apply and revoke settings, manage the lifecycle of the associated master component,
/// and handle asynchronous disposal. It is designed to be inherited by specific setting implementations.
/// </summary>
public abstract class ScSettingBase<TComponent> : ComponentBase, IAsyncDisposable, IScSetting
    where TComponent : class
{
    protected TComponent? Master { get; private set; }

    [CascadingParameter]
    public ScSettingMaster? MasterComponent
    {
        get => null;
        set
        {
            if (Master == value?.Value) return;
            if (Master is not null) ForgoSetting();
            Master = value?.Value as TComponent;
            if (Master is not null) ApplySetting();
        }
    }

    protected virtual void ApplySetting()
    {
    }

    protected virtual void ForgoSetting()
    {
    }

    public virtual ValueTask DisposeAsync()
    {
        MasterComponent = null;
        return ValueTask.CompletedTask;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public virtual int Priority => 0;
}