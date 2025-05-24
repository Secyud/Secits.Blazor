using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Abstraction;

public interface IScdSelect
{
    void DelegateSelectItem(SelectionItem? item);
    void DelegateSelectItems(IEnumerable<SelectionItem> items);
    bool MultiSelectEnabled { get; set; }
    void BindComponent(ISccSelect component);
    void UnbindComponent(ISccSelect component);
}