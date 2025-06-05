namespace Secyud.Secits.Blazor.Components;

public partial class SPagedItems<TItem> : ISciItemsRenderer<TItem>, ISciFooterRenderer
{
    private List<TItem> _items = [];

    private int _totalCount;

    protected override void ApplySetting()
    {
        Master!.ItemsRenderer.Apply(this);
        Master.Footers.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master!.ItemsRenderer.Forgo(this);
        Master.Footers.Forgo(this);
    }

    public async Task RefreshAsync()
    {
        if (Master?.Items is null) return;
        var result = await Master.Items(Master.DataRequest);
        _items = result.Items.ToList();
        _totalCount = result.TotalCount;
    }

    private async Task PageSizeChangedAsync(int pageSize)
    {
        if (Master is null) return;
        Master.DataRequest.PageSize = pageSize;
        await Master.RefreshAsync();
    }

    private async Task PageIndexChangedAsync(int pageIndex)
    {
        if (Master is null) return;
        Master.DataRequest.PageIndex = pageIndex;
        await Master.RefreshAsync();
    }
}