using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorPager<TValue> : IIteratorRenderer<TValue>, IContentFooterRenderer
{
    [Parameter]
    public int[]? PageSizes { get; set; }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        Master.Footer.Apply(this);

        if (PageSizes is { Length: > 0 })
            Master.DataRequest.PageSize = PageSizes[0];
        
        RefreshAsync().ConfigureAwait(false);
    }

    protected override void ForgoSetting()
    {
        base.ForgoSetting();
        Master.Footer.Forgo(this);
    }

    protected override void PreRefresh()
    {
    }

    private async Task PageSizeChangedAsync(int pageSize)
    {
        Master.DataRequest.PageSize = pageSize;
        await Master.RefreshAsync();
    }

    private async Task PageIndexChangedAsync(int pageIndex)
    {
        Master.DataRequest.PageIndex = pageIndex;
        await Master.RefreshAsync();
    }
}