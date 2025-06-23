using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

/// <summary>
/// Represents a base class for settings components that are associated with a master component.
/// This class provides functionality to apply and revoke settings, manage the lifecycle of the associated master component,
/// and handle asynchronous disposal. It is designed to be inherited by specific setting implementations.
/// </summary>
public abstract class SSettingBase<TComponent> : IComponent, IAsyncDisposable, IIsSetting
    where TComponent : SComponentBase
{
    private TComponent? _master;

    protected TComponent Master => _master!;

    protected bool MasterValid => _master is not null;

    [CascadingParameter]
    public SSettingMaster? MasterComponent
    {
        get => null;
        set
        {
            if (_master == value?.Value) return;
            if (_master is not null)
            {
                ForgoSetting();
                StateHasChanged();
            }

            _master = value?.Value as TComponent;
            if (_master is not null)
            {
                ApplySetting();
                StateHasChanged();
            }
        }
    }

    protected void StateHasChanged()
    {
        _master?.MasterStateHasChanged();
    }

    protected async Task InvokeAsync(Action action)
    {
        if (_master is not null)
        {
            await _master.MasterInvokeAsync(action);
        }
    }

    protected async Task InvokeAsync(Func<Task> action)
    {
        if (_master is not null)
        {
            await _master.MasterInvokeAsync(action);
        }
    }

    protected abstract void ApplySetting();

    protected abstract void ForgoSetting();

    public virtual ValueTask DisposeAsync()
    {
        MasterComponent = null;
        return ValueTask.CompletedTask;
    }

    public void Attach(RenderHandle renderHandle)
    {
    }

    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        return Task.CompletedTask;
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }

    protected EventCallback<TArgs> CreateEventCallback<TArgs>(Action<TArgs> action)
    {
        return EventCallback.Factory.Create(Master, action);
    }

    protected EventCallback<TArgs> CreateEventCallback<TArgs>(Func<TArgs, Task> action)
    {
        return EventCallback.Factory.Create(Master, action);
    }

    protected EventCallback CreateEventCallback(Action action)
    {
        return EventCallback.Factory.Create(Master, action);
    }

    protected EventCallback CreateEventCallback(Func<Task> action)
    {
        return EventCallback.Factory.Create(Master, action);
    }
}