namespace Secyud.Secits.Blazor;

public interface ISciDataSource<TItem>
{
    Task<DataResult<TItem>> GetDataAsync(DataRequest request);
}