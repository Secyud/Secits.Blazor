using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor;

public class JsEventHandler(IJSRuntime js) : JsModule(js, $"./{SecitsOptions.RootPath}js/event-handler.js")
{
}