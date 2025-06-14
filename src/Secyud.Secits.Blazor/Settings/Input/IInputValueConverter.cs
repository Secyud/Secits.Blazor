namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// for text convert to value.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IInputValueConverter<TValue> : IIsSetting
{
    bool TryConvert(string? str, out TValue output);
}