using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

public partial class ComboBox<TItem, TValue>
{
    [Parameter]
    public IReadOnlyList<TItem> Items { get; set; } = [];
    
    
    
    [Parameter]
    public bool EnableMultiSelect { get; set; }
}