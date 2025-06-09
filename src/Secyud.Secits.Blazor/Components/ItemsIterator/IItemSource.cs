using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public interface ISciDataSource<TItem>
{
    Task<DataResult<TItem>> GetDataAsync(DataRequest request);
}