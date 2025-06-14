using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class AsyncDataSource<TItem> : SSettingBase<SIteratorBase<TItem>>, IDataSourceProvider<TItem>
{
    [Parameter]
    public Func<DataRequest, Task<DataResult<TItem>>>? Items { get; set; }

    protected override void ApplySetting()
    {
        Master.DataSource.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.DataSource.Forgo(this);
    }

    public Task<DataResult<TItem>> GetDataAsync(DataRequest request)
    {
        return Items?.Invoke(request) ?? Task.FromResult(new DataResult<TItem>());
    }
}