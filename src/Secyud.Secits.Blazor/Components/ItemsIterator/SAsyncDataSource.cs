using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public class SAsyncDataSource<TItem> : ScSettingBase<SItemsIteratorBase<TItem>>, ISciDataSource<TItem>
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