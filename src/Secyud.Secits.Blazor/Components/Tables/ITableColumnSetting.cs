using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

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