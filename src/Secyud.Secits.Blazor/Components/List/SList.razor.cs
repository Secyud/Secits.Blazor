using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TItem))]
public partial class SList<TItem> : ISchTextField<TItem>,  IScsSize, ISciSelect
{
    protected override string ComponentName => "list";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await RefreshAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    #region Setting

    private ISciItemsRenderer? _itemsRenderer;

    protected ISciItemsRenderer? ItemsRenderer => _itemsRenderer;


    public virtual void SetItemsRender(ISciItemsRenderer renderer)
    {
        _itemsRenderer = renderer;
    }

    public void UnsetItemsRender(ISciItemsRenderer renderer)
    {
        if (_itemsRenderer == renderer)
            _itemsRenderer = null;
    }

    private readonly List<ISciHeaderRender> _headers = [];

    protected IReadOnlyList<ISciHeaderRender> Headers => _headers;

    public virtual void AddHeaderRender(ISciHeaderRender renderer)
    {
        RemoveHeaderRender(renderer);
        _headers.Add(renderer);
    }

    public virtual void RemoveHeaderRender(ISciHeaderRender renderer)
    {
        _headers.Remove(renderer);
    }

    private readonly List<ISciColumnRender<TItem>> _columns = [];

    protected IReadOnlyList<ISciColumnRender<TItem>> Columns => _columns;

    public virtual void AddColumnRender(ISciColumnRender<TItem> renderer)
    {
        RemoveColumnRender(renderer);
        _columns.Add(renderer);
    }

    public virtual void RemoveColumnRender(ISciColumnRender<TItem> renderer)
    {
        _columns.Remove(renderer);
    }

    private readonly List<ISciFooterRender> _footers = [];

    protected IReadOnlyList<ISciFooterRender> Footers => _footers;

    public virtual void AddFooterRender(ISciFooterRender renderer)
    {
        RemoveFooterRender(renderer);
        _footers.Add(renderer);
    }

    public virtual void RemoveFooterRender(ISciFooterRender renderer)
    {
        _footers.Remove(renderer);
    }

    protected ISciItemSelect<TItem>? Select { get; private set; }

    public virtual void SetSelect(ISciItemSelect<TItem> itemSelect)
    {
        Select = itemSelect;
    }

    public virtual void UnsetSelect(ISciItemSelect<TItem> itemSelect)
    {
        if (itemSelect == Select)
            Select = null;
    }

    #endregion

    #region Parameters

    [Parameter]

    public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }

    [Parameter]
    public IEnumerable<TItem> SelectedItems { get; set; } = [];

    [Parameter]
    public EventCallback<TItem?> SelectedItemChanged { get; set; }

    [Parameter]
    public TItem? SelectedItem { get; set; }

    public TItem? ActiveItem { get; set; }

    #endregion

    #region Data

    public DataRequest DataRequest { get; } = new();

    [Parameter]
    public Func<DataRequest, Task<DataResult<TItem>>>? Items { get; set; }

    public async Task RefreshAsync()
    {
        if (_itemsRenderer is not null)
            await _itemsRenderer.RefreshAsync();
    }

    #endregion

    #region SciSelect

    private IScdSelect? _selectDelegate;

    [CascadingParameter]
    public IScdSelect? SelectDelegate
    {
        get => _selectDelegate;
        set
        {
            _selectDelegate?.UnbindComponent(this);
            _selectDelegate = value;
            _selectDelegate?.BindComponent(this);
        }
    }

    [Parameter]
    public bool MultiSelectEnabled { get; set; }

    public async Task UnselectObjectAsync(object obj)
    {
        if (obj is not TItem item) return;

        if (MultiSelectEnabled)
        {
            await OnItemsSelectChangedAsync(SelectedItems.Where(u => !Equals(item, u)));
        }
        else
        {
            await OnItemSelectChangedAsync(Equals(item, SelectedItem) ? default : item);
        }
    }

    public async Task ClearSelectAsync()
    {
        if (MultiSelectEnabled)
        {
            await OnItemsSelectChangedAsync([]);
        }
        else
        {
            await OnItemSelectChangedAsync(default);
        }
    }

    #endregion

    #region Selection

    protected virtual async Task OnItemActivateChangedAsync(TItem item)
    {
        ActiveItem = item;

        if (MultiSelectEnabled)
        {
            var list = SelectedItems.Where(u => !Equals(item, u)).ToList();
            if (!list.Remove(item)) list.Add(item);
            await OnItemsSelectChangedAsync(list);
        }
        else
        {
            await OnItemSelectChangedAsync(Equals(item, SelectedItem) ? default : item);
        }
    }

    public virtual async Task OnItemSelectChangedAsync(TItem? item)
    {
        SelectedItem = item;

        if (SelectedItemChanged.HasDelegate)
            await SelectedItemChanged.InvokeAsync(SelectedItem);

        if (SelectDelegate is not null) await SelectDelegate.OnDelegateSelectItemAsync(
            SelectionItem.FromObject(item, GetText));
        if (Select is not null) await Select.OnItemSelectChangedAsync(item);
    }

    public virtual async Task OnItemsSelectChangedAsync(IEnumerable<TItem> items)
    {
        SelectedItems = items.ToList();

        if (SelectedItemsChanged.HasDelegate)
            await SelectedItemsChanged.InvokeAsync(SelectedItems);

        if (SelectDelegate is not null) await SelectDelegate.OnDelegateSelectItemsAsync(
            SelectedItems.Select(u => SelectionItem.FromObject(u, GetText)));
        if (Select is not null) await Select.OnItemsSelectChangedAsync(SelectedItems);
    }

    protected virtual bool IsItemSelected(TItem? item)
    {
        return MultiSelectEnabled
            ? SelectedItems.Contains(item)
            : Equals(SelectedItem, item);
    }

    protected string? RowClass(TItem item)
    {
        return IsItemSelected(item) ? " selected" : null;
    }

    #endregion

    #region Abstraction

    [Parameter]
    public Func<TItem, string?>? TextField { get; set; }

    protected virtual string? GetText(TItem? item)
    {
        return TextField is null || item is null ? item?.ToString() : TextField(item);
    }

    #endregion


    [Parameter]
    public SValue Width { get; set; }

    [Parameter]
    public SValue Height { get; set; }
}