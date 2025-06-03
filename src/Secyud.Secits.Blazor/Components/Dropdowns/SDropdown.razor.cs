using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public partial class SDropdown : IScdSelect
{
    protected override string ComponentName => "dropdown";
    protected override string ElementName => "input";

    protected SelectionItem? SelectionItem { get; set; }

    protected IEnumerable<SelectionItem> SelectionItems { get; set; } = [];

    public Task OnDelegateSelectItemAsync(SelectionItem? item)
    {
        SelectionItem = item;
        return Task.CompletedTask;
    }

    public Task OnDelegateSelectItemsAsync(IEnumerable<SelectionItem> items)
    {
        SelectionItems = items;
        return Task.CompletedTask;
    }

    public bool MultiSelectEnabled { get; set; }


    private ISciSelect? _component;

    public void BindComponent(ISciSelect component)
    {
        _component = component;
    }

    public void UnbindComponent(ISciSelect component)
    {
        if (component == _component)
            _component = null;
    }

    protected async Task UnselectObjectAsync(object item)
    {
        if (_component is not null) await _component.UnselectObjectAsync(item);
    }

    protected async Task ClearSelectAsync()
    {
        if (_component is not null) await _component.ClearSelectAsync();
    }
}