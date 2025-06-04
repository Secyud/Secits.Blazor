namespace Secyud.Secits.Blazor.Components;

public interface ISciSelect
{
    bool MultiSelectEnabled { get; set; }
    IScdSelect? SelectDelegate { get; set; }
    Task UnselectObjectAsync(object obj);
    Task ClearSelectAsync();
}