using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SJsDocument(JsEventHandler eventHandler, IJSRuntime js) : IJsDocument
{
    private readonly Lazy<Task<IJSObjectReference>> _documentEventHandler =
        new(() => eventHandler.InvokeAsync<IJSObjectReference>("getDocumentEventHandler").AsTask()!);

    public async Task<long> AddEventListener<TEventArgs>(string type, Func<TEventArgs, Task> func)
    {
        var handler = await _documentEventHandler.Value;

        return await handler.InvokeAsync<long>("addEventListener", type,
            DotNetObjectReference.Create(new Invoker<TEventArgs>(func)));
    }

    public async Task<long> AddEventListener(string type, Func<Task> func)
    {
        var handler = await _documentEventHandler.Value;
        return await handler.InvokeAsync<long>("addEventListener", type,
            DotNetObjectReference.Create(new Invoker(func)));
    }

    public async Task RemoveEventListener(long id)
    {
        var handler = await _documentEventHandler.Value;

        await handler.InvokeVoidAsync("removeEventListener", id);
    }
}