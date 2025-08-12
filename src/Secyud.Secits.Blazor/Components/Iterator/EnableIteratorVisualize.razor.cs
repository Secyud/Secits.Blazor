using Microsoft.AspNetCore.Components.Web.Virtualization;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorVisualize<TItem> : IIteratorRenderer<TItem>, IExtendClassStyleBuilder
{
    private Virtualize<TItem>? _virtualize;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
        Master.ClassStyleBuilders.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
        Master.ClassStyleBuilders.Forgo(this);
    }

    public async ValueTask<ItemsProviderResult<TItem>> RefreshRowsAsync(ItemsProviderRequest request)
    {
        if (Master.DataSource.Get() is not { } source) return default;
        Master.DataRequest.SkipCount = request.StartIndex;
        Master.DataRequest.PageSize = request.Count;
        Master.DataRequest.CancellationToken = request.CancellationToken;
        var result = await source.GetDataAsync(Master.DataRequest);
        return new ItemsProviderResult<TItem>(result.Items, result.TotalCount);
    }

    public async Task RefreshAsync()
    {
        if (_virtualize is not null)
        {
            await _virtualize.RefreshDataAsync();
        }
    }

    public void BuildExtendClassStyle(ClassStyleContext context)
    {
        context.AppendClass("visualize");
    }
}