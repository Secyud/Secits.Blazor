using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class DynamicDataSource<TValue> : SPluginBase<SIteratorBase<TValue>>, IDataSourceProvider<TValue>
{
    [Parameter]
    public Func<DataRequest, Task<DataResult<TValue>>>? Items { get; set; }

    protected override void ApplySetting()
    {
        Master.DataSource.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.DataSource.Forgo(this);
    }

    public Task<DataResult<TValue>> GetDataAsync(DataRequest request)
    {
        return Items?.Invoke(request) ?? Task.FromResult(new DataResult<TValue>());
    }
}