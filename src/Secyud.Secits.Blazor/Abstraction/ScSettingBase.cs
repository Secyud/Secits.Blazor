using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Components;

namespace Secyud.Secits.Blazor.Abstraction;

/// <summary>
/// Represents a base class for settings components that are associated with a master component.
/// This class provides functionality to apply and revoke settings, manage the lifecycle of the associated master component,
/// and handle asynchronous disposal. It is designed to be inherited by specific setting implementations.
/// </summary>
/// <typeparam name="TComponent">The type of the master component, which must derive from ScBase.</typeparam>
public abstract class ScSettingBase<TComponent> : ComponentBase, IAsyncDisposable
    where TComponent : class
{
    private TComponent? _masterComponent;

    [CascadingParameter(Name = nameof(MasterComponent))]
    public TComponent? MasterComponent
    {
        get => _masterComponent;
        set
        {
            if (_masterComponent == value) return;
            ForgoSetting();
            _masterComponent = value;
            ApplySetting();
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
}