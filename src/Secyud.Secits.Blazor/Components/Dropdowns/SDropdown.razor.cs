using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public partial class SDropdown : IScdSelect
{
    protected override string ComponentName => "dropdown";
    protected override string ElementName => "input";

    protected SelectionItem? SelectionItem { get; set; }
    protected IEnumerable<SelectionItem> SelectionItems { get; set; } = [];

    public void DelegateSelectItem(SelectionItem? item)
    {
        SelectionItem = item;
    }

    public void DelegateSelectItems(IEnumerable<SelectionItem> items)
    {
        SelectionItems = items;
    }

    public bool MultiSelectEnabled { get; set; }


    private readonly List<ISccSelect> _components = [];

    public void BindComponent(ISccSelect component)
    {
        _components.Remove(component);
        _components.Add(component);
    }

    public void UnbindComponent(ISccSelect component)
    {
        _components.Remove(component);
    }

    protected async Task UnselectObjectAsync(object item)
    {
        foreach (var component in _components)
        {
            await component.UnselectObjectAsync(item);
        }
    }

    protected async Task ClearSelectAsync()
    {
        foreach (var component in _components)
        {
            await component.ClearSelectAsync();
        }
    }
}