using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorFixed<TValue> : IIteratorRenderer<TValue>
{
    protected List<TValue> Items { get; private set; } = [];
    protected int TotalCount { get; private set; } = 1000;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
        RefreshAsync().ConfigureAwait(false);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
    }

    public virtual async Task RefreshAsync()
    {
        if (Master.DataSource.Get() is not { } source) return;
        PreRefresh();
        var result = await source.GetDataAsync(Master.DataRequest);
        Items = result.Items.ToList();
        TotalCount = result.TotalCount;
    }

    protected virtual void PreRefresh()
    {
        Master.DataRequest.PageSize = TotalCount;
    }
}