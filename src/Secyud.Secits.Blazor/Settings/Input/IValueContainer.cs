namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// settings may have different way to sync value.
/// for special use.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IValueContainer<in TValue> : IIsPlugin
{
    Task SetValueFromParameterAsync(TValue value);
}