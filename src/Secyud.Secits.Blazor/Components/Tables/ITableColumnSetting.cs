using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public interface ITableColumnSetting<TItem> 
{
    bool RenderHeader();

    RenderFragment GenerateHeader();

    bool RenderFilter();

    RenderFragment GenerateFilter();

    RenderFragment GenerateBody(TItem item);

    DataFilter Filter { get; }

    DataSorter Sorter { get; }
}