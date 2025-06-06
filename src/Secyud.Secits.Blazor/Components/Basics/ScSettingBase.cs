using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// Represents a base class for settings components that are associated with a master component.
/// This class provides functionality to apply and revoke settings, manage the lifecycle of the associated master component,
/// and handle asynchronous disposal. It is designed to be inherited by specific setting implementations.
/// </summary>
public abstract class ScSettingBase<TComponent> : ComponentBase, IAsyncDisposable, IScSetting
    where TComponent : ScBusinessBase
{
    private TComponent? _master;

    protected TComponent Master
    {
        get => _master!;
        private set => _master = value;
    }

    protected bool MasterValid => _master is not null;

    [CascadingParameter]
    public ScSettingMaster? MasterComponent
    {
        get => null;
        set
        {
            if (_master == value?.Value) return;
            if (_master is not null)
            {
                ForgoSetting();
                _master.RefreshUi();
            }
            _master = value?.Value as TComponent;
            if (_master is not null)
            {
                ApplySetting();
                _master.RefreshUi();
            }
        }
    }

    protected abstract void ApplySetting();

    protected abstract void ForgoSetting();

    public virtual ValueTask DisposeAsync()
    {
        MasterComponent = null;
        return ValueTask.CompletedTask;
    }
}