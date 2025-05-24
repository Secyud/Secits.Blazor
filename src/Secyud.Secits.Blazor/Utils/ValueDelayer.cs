using System.Timers;
using Timer = System.Timers.Timer;

namespace Secyud.Secits.Blazor.Utils;

/// <summary>
/// Delays the entered value by the defined interval.
/// </summary>
public class ValueDelayer<TValue> : IDisposable
{
    /// <summary>
    /// Holds the last updated value.
    /// </summary>
    private TValue? _value;

    private Timer? _timer;
    private int _delayInterval;

    /// <summary>
    /// Event raised after the interval has passed and with new updated value.
    /// </summary>
    public event EventHandler<TValue?>? Delayed;

    public int DelayInterval
    {
        get => _delayInterval;
        set
        {
            if (_timer is not null)
            {
                _timer.Stop();
                _timer.Elapsed -= OnElapsed;
            }

            _delayInterval = value;

            if (_delayInterval > 0)
            {
                _timer = new Timer(_delayInterval);
                _timer.AutoReset = false;
                _timer.Elapsed += OnElapsed;
            }
            else
            {
                _timer = null;
            }
        }
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ValueDelayer()
    {
    }

    private void OnElapsed(object? source, ElapsedEventArgs e)
    {
        Delayed?.Invoke(this, _value);
    }

    /// <summary>
    /// Updates the internal value.
    /// </summary>
    /// <param name="value">New value.</param>
    public void Update(TValue? value)
    {
        if (_timer is null)
        {
            Delayed?.Invoke(this, value);
            return;
        }

        _timer.Stop();
        _value = value;
        _timer.Start();
    }

    /// <summary>
    /// Releases all subscribed events.
    /// </summary>
    public void Dispose()
    {
        if (_timer != null)
        {
            _timer.Stop();
            _timer.Elapsed -= OnElapsed;
            _timer = null;
        }
    }
}