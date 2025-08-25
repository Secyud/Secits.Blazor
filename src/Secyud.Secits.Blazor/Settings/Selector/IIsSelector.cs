namespace Secyud.Secits.Blazor.Settings;

public interface IIsSelector<TItem> : IIsPlugin
{
    bool IsItemSelected(TItem value);
    Task ClearActiveItemAsync();
    Task SetActiveItemAsync(TItem value);
    TItem GetActiveItem();
}