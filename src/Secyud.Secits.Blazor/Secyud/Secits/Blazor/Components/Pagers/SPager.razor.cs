using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public partial class SPager
{
    protected override string ComponentName => "pager";

    [Parameter]
    public int MaxPageCount { get; set; }

    [Parameter]
    public int PageIndex { get; set; }

    [Parameter]
    public EventCallback<int> PageIndexChanged { get; set; }
}