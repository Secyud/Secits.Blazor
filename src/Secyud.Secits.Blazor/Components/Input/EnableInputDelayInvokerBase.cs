using System.Timers;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;
using Timer = System.Timers.Timer;

namespace Secyud.Secits.Blazor;

public abstract class EnableInputDelayInvokerBase<TValue> : SPluginBase<SInput<TValue>>, IInputInvoker<TValue>
{
    private Timer? _timer;
    protected TValue LastActiveItem { get; set; } = default!;

    [Parameter]
    public int DelayInterval { get; set; }

    [Parameter]
    public EventCallback ValueUpdated { get; set; }

    protected override void ApplySetting()
    {
        Master.InputInvoker.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.InputInvoker.Forgo(this);
    }

    protected override void PreParametersSet(ParameterContainer parameters)
    {
        parameters.UseParameter(DelayInterval, nameof(DelayInterval), value =>
        {
            if (_timer is not null)
            {
                _timer.Stop();
                _timer.Elapsed -= OnElapsed;
            }

            if (value > 0)
            {
                _timer = new Timer(value);
                _timer.AutoReset = false;
                _timer.Elapsed += OnElapsed;
            }
            else
            {
                _timer = null;
            }
        });
    }

    protected async Task Do(Func<Task> task)
    {
        if (_timer is null)
        {
            await task.Invoke();
            await OnValueChangedAsync();
        }
        else
        {
            _timer.Stop();
            await task.Invoke();
            _timer.Start();
        }
    }

    protected void OnElapsed(object? source, ElapsedEventArgs e)
    {
        OnValueChangedAsync().ConfigureAwait(false);
    }

    protected abstract Task OnValueChangedAsync();

    public override async ValueTask DisposeAsync()
    {
        if (_timer != null)
        {
            _timer.Stop();
            _timer.Elapsed -= OnElapsed;
            _timer = null;
        }

        await base.DisposeAsync();
    }

    public abstract bool IsItemSelected(TValue value);

    protected abstract Task OnClearActiveItemAsync(object sender);
    protected abstract Task OnSetActiveItemAsync(object sender, TValue value);

    public Task ClearActiveItemAsync(object sender)
    {
        return Do(() => OnClearActiveItemAsync(sender));
    }

    public Task SetActiveItemAsync(object sender, TValue value)
    {
        return Do(() => OnSetActiveItemAsync(sender, value));
    }

    public virtual TValue GetActiveItem()
    {
        return LastActiveItem;
    }

    protected async Task NotifyValueChangedAsync(object sender)
    {
        await Master.ValueContainer.InvokeAsync(OnValueUpdatedAsync);
        if (ValueUpdated.HasDelegate)
            await ValueUpdated.InvokeAsync();
        await InvokeAsync(StateHasChanged);
        return;

        Task OnValueUpdatedAsync(IValueContainer container)
        {
            return container.OnValueUpdatedAsync(sender);
        }
    }
}