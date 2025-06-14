namespace Secyud.Secits.Blazor;

public interface ISciInputValueConverter<TValue>
{
    bool TryConvert(string? str, out TValue output);
}