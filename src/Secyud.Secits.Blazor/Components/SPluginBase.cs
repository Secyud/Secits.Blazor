using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

/// <summary>
/// Represents a base class for settings components that are associated with a master component.
/// This class provides functionality to apply and revoke settings, manage the lifecycle of the associated master component,
/// and handle asynchronous disposal. It is designed to be inherited by specific setting implementations.
/// </summary>
public abstract class SPluginBase<TComponent> : IComponent, IAsyncDisposable, IIsSetting
    where TComponent : class, IPluggable
{
    private TComponent? _master;
    protected TComponent Master => _master!;

    protected bool MasterValid => _master is not null;

    [CascadingParameter]
    public SSettingMaster? MasterComponent
    {
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
        _master?.StateHasChanged();
    }

    protected async Task InvokeAsync(Action action)
    {
        if (_master is not null)
        {
            await _master.InvokeAsync(action);
        }
    }

    protected async Task InvokeAsync(Func<Task> action)
    {
        if (_master is not null)
        {
            await _master.InvokeAsync(action);
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
        var container = new ParameterContainer(parameters);
        BeforeParametersSet(container);
        parameters.SetParameterProperties(this);
        return Task.WhenAll(container.ParameterTasks);
    }

    protected virtual void BeforeParametersSet(ParameterContainer parameters)
    {
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