using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SPager
{
    [Parameter]
    public int MaxPageCount { get; set; }

    [Parameter]
    public int PageIndex { get; set; }

    [Parameter]
    public EventCallback<int> PageIndexChanged { get; set; }
}