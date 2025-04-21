using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Parameters;

public interface IDataComponent<TItem>
{
    IEnumerable<TItem>? Data { get; set; }

    EventCallback<DataRequest> RefreshCallback { get; set; }
}