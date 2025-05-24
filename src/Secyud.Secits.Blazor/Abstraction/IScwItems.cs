using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Abstraction;

public interface IScwItems<TItem>
{
    IEnumerable<TItem>? Items { get; set; }

    EventCallback<DataRequest> ItemsLoad { get; set; }
}