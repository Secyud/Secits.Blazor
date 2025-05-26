using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public interface IScwItems<TItem> : ISccRefresh
{
    Func<DataRequest, Task<DataResult<TItem>>>? Items { get; set; }

    DataRequest DataRequest { get; }
}