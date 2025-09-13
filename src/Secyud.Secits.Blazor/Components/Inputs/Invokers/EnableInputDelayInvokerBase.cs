﻿using System.Timers;
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

    protected async Task Do(object sender, Func<Task> task)
    {
        if (_timer is null)
        {
            await task.Invoke();
            await OnValueChangedAsync();
            await NotifyValueChangedAsync(sender, true);
        }
        else
        {
            _timer.Stop();
            await task.Invoke();
            await NotifyValueChangedAsync(sender, false);
            _timer.Start();
        }
    }

    protected void OnElapsed(object? source, ElapsedEventArgs e)
    {
        Task.Run(async () =>
        {
            await OnValueChangedAsync();
            await NotifyValueChangedAsync(source, true);
        }).ConfigureAwait(false);
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

    protected abstract Task OnClearActiveItemAsync();
    protected abstract Task OnSetActiveItemAsync(TValue value);

    public Task ClearActiveItemAsync(object sender)
    {
        return Do(sender, OnClearActiveItemAsync);
    }

    public Task SetActiveItemAsync(object sender, TValue value)
    {
        return Do(sender, () => OnSetActiveItemAsync(value));
    }

    public virtual TValue GetActiveItem()
    {
        return LastActiveItem;
    }

    protected async Task NotifyValueChangedAsync(object? sender, bool applied)
    {
        await Master.ValueContainer.InvokeAsync(OnValueUpdatedAsync);
        if (ValueUpdated.HasDelegate) await ValueUpdated.InvokeAsync();

        await InvokeAsync(StateHasChanged);
        return;

        Task OnValueUpdatedAsync(IValueContainer container)
        {
            return container.OnValueUpdatedAsync(sender, applied);
        }
    }
}