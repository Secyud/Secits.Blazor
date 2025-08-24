using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class StaticDataSource<TItem> : SPluginBase<SIteratorBase<TItem>>, IDataSourceProvider<TItem>
{
    [Parameter]
    public IReadOnlyList<TItem>? Items { get; set; }

    protected override void ApplySetting()
    {
        Master.DataSource.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.DataSource.Forgo(this);
    }

    protected virtual Task<IEnumerable<TItem>> ApplyDataHandleAsync(IEnumerable<TItem> items, DataRequest request)
    {
        return Task.FromResult(items);
    }

    public async Task<DataResult<TItem>> GetDataAsync(DataRequest request)
    {
        var res = new DataResult<TItem>();
        if (Items is null) return res;
        var items = (await ApplyDataHandleAsync(Items, request)).ToList();
        res.TotalCount = items.Count;
        res.Items = items.Skip(request.SkipCount).Take(request.PageSize).ToList();
        return res;
    }
}