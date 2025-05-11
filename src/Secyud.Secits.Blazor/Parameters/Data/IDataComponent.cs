using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface IDataComponent<TItem>
{
    IEnumerable<TItem>? Data { get; set; }

    EventCallback<DataRequest> DataLoad { get; set; }
}