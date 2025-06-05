namespace Secyud.Secits.Blazor.Components;

public interface ISciInputValueConverter<TValue>
{
    bool TryConvert(string? str, out TValue output);
}