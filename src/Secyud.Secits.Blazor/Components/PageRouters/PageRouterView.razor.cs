using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.PageRouters;

namespace Secyud.Secits.Blazor;

public partial class PageRouterView
{
    [Parameter]
    public PageRouterItem? Item { get; set; }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (Item is not null)
        {
            // Item.Body ??= CreateRouterTabsItemBody(Item);
        }
    }
}