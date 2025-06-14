namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// input may have different invoker.
/// for input base, all value submit
/// immediately for ui sync.
/// but the real submit may delayed by invoker.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IInputInvoker<in TValue> : IIsSetting
{
    Task InvokeValueChanged(object? sender, TValue value);
}