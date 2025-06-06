using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Secyud.Secits.Blazor.Components;

public partial class SVisualizeItems<TItem> : ISciItemsRenderer<TItem>
{
    private Virtualize<TItem>? _virtualize;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
    }

    public async ValueTask<ItemsProviderResult<TItem>> RefreshRowsAsync(ItemsProviderRequest request)
    {
        if (Master.Items is null) return default;
        Master.DataRequest.SkipCount = request.StartIndex;
        Master.DataRequest.PageSize = request.Count;
        Master.DataRequest.CancellationToken = request.CancellationToken;
        var result = await Master.Items(Master.DataRequest);
        return new ItemsProviderResult<TItem>(result.Items, result.TotalCount);
    }

    public async Task RefreshAsync()
    {
        if (_virtualize is not null)
        {
            await _virtualize.RefreshDataAsync();
        }
    }
}