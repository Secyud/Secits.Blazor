using Microsoft.JSInterop;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor.JSInterop;

public class JsEventHandler(IJSRuntime js) : JsModule(js, $"./{SecitsOptions.RootPath}js/event-handler.js")
{
}