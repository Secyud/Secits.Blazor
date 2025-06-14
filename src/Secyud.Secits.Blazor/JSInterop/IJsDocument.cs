namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsDocument
{
    Task<long> AddEventListener<TEventArgs>(string type, Func<TEventArgs, Task> func);
    Task<long> AddEventListener(string type, Func<Task> func);

    Task RemoveEventListener(long id);
}