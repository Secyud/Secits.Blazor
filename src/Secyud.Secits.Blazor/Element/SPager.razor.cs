using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.Element;

public partial class SPager : IHasCustomCss
{
    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public int MaxPageCount { get; set; }

    [Parameter]
    public int NeighbourCount { get; set; } = 1;

    [Parameter]
    public int PageIndex { get; set; }

    [Parameter]
    public EventCallback<int> PageIndexChanged { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected async Task ChangePageIndexAsync(int index)
    {
        if (index < 0 || index >= MaxPageCount || PageIndex == index)
            return;

        await PageIndexChanged.InvokeAsync(index);


        PageIndex = index;
        await InvokeAsync(StateHasChanged);
    }

    protected async Task TurnToFirstPageAsync()
    {
        if (PageIndex != 0) await ChangePageIndexAsync(0);
    }

    protected async Task TurnToPreviewPageAsync()
    {
        if (PageIndex > 0) await ChangePageIndexAsync(PageIndex - 1);
    }

    protected async Task TurnToNextPageAsync()
    {
        if (PageIndex < MaxPageCount - 1) await ChangePageIndexAsync(PageIndex + 1);
    }

    protected async Task TurnToLastPageAsync()
    {
        if (PageIndex != MaxPageCount - 1) await ChangePageIndexAsync(MaxPageCount - 1);
    }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-pager", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }

    protected RenderFragment GeneratePageButtons(int min, int max) =>
        builder =>
        {
            for (int i = min; i < max; i++)
            {
                builder.AddContent(i, GeneratePageButton(i));
            }
        };
}