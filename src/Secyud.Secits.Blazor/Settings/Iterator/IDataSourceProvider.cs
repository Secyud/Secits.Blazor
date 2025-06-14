namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the provider for data source.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IDataSourceProvider<TItem>: IIsSetting
{
    Task<DataResult<TItem>> GetDataAsync(DataRequest request);
}