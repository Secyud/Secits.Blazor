namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// input may have different invoker.
/// for input base, all value submit
/// immediately for ui sync.
/// but the real submit may delayed by invoker.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IInputInvoker<TValue> : IIsPlugin
{
    bool IsItemSelected(TValue value);
    Task ClearActiveItemAsync();
    Task SetActiveItemAsync(TValue value);
    TValue GetActiveItem();
}