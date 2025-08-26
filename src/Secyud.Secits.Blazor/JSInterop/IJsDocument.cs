namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsDocument
{
    Task<long> AddEventListener<TEventArgs>(Func<TEventArgs, Task> func, params string[] types);
    Task<long> AddEventListener(Func<Task> func, params string[] types);
    Task<long?> RemoveEventListener(long? id);
}