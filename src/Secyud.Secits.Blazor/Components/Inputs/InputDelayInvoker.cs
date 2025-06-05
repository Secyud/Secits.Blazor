using System.Timers;
using Microsoft.AspNetCore.Components;
using Timer = System.Timers.Timer;

namespace Secyud.Secits.Blazor.Components;

public class InputDelayInvoker<TValue> : ScSettingBase<SInput<TValue>>, ISciInputInvoker<TValue>
{
    private Timer? _timer;
    private TValue _value = default!;

    [Parameter]
    public int DelayInterval { get; set; }

    protected override void ApplySetting()
    {
        Master!.InputInvoker.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master!.InputInvoker.Forgo(this);
    }

    public override async Task SetParametersAsync(ParameterView parameters)
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

        await base.SetParametersAsync(parameters);
    }


    public async Task InvokeValueChanged(object? sender, TValue value)
    {
        if (_timer is null)
        {
            await ValueChangedAsync(value);
        }
        else
        {
            _timer.Stop();
            _value = value;
            _timer.Start();
        }
    }

    private void OnElapsed(object? source, ElapsedEventArgs e)
    {
        ValueChangedAsync(_value).ConfigureAwait(false);
    }

    private async Task ValueChangedAsync(TValue value)
    {
        if (Master?.ValueChanged is { HasDelegate: true } callback)
        {
            await callback.InvokeAsync(value);
        }
    }

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
}