using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Header : SLayoutPluginBase<SContentBase>, IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    

}