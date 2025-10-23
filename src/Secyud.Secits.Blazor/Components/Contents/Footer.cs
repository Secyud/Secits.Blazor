using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Footer : SLayoutPluginBase<SContentBase>, IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}