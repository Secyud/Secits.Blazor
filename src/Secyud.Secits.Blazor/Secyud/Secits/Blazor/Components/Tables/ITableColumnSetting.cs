using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Components;

public interface ITableColumnSetting<TItem> : IFieldComponent<TItem>
{
    bool RenderHeader();
    
    RenderFragment GenerateHeader();

    bool RenderFilter();
    
    RenderFragment GenerateFilter();
    
    RenderFragment GenerateBody(TItem item);

    DataFilter Filter { get; }

    DataSorter Sorter { get; }
}