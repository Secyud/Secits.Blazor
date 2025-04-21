using System.Timers;
using Timer = System.Timers.Timer;

namespace Secyud.Secits.Utils;

/// <summary>
/// Delays the entered value by the defined interval.
/// </summary>
public class ValueDelayer<TValue> : IDisposable
{
    /// <summary>
    /// Internal timer used to delay the value.
    /// </summary>
    private Timer? _timer;

    /// <summary>
    /// Holds the last updated value.
    /// </summary>
    private TValue? _value;

    /// <summary>
    /// Event raised after the interval has passed and with new updated value.
    /// </summary>
    public event EventHandler<TValue?>? Delayed;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="interval">Interval by which the value will be delayed.</param>
    public ValueDelayer(int interval)
    {
        _timer = new Timer(interval);
        _timer.Elapsed += OnElapsed;
        _timer.AutoReset = false;
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
        if (_timer is null) return;
        
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