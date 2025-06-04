using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public interface IScdSelect
{
    Task OnDelegateSelectItemAsync(SelectionItem? item);
    Task OnDelegateSelectItemsAsync(IEnumerable<SelectionItem> items);
    void BindComponent(ISciSelect component);
    void UnbindComponent(ISciSelect component);
}