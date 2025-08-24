namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the provider for data source.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IDataSourceProvider<TItem>: IIsPlugin
{
    Task<DataResult<TItem>> GetDataAsync(DataRequest request);
}