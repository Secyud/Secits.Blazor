using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TItem))]
public partial class ScList<TItem, TValue> : ISccSelect<TItem, TValue>,
    ISchTextField<TItem>, ISchValueField<TItem, TValue>, IScList<TItem>
{
    protected override string ComponentName => "list";

    #region Setting

    private ISciItemsRenderer? _itemsRenderer;

    protected ISciItemsRenderer? ItemsRenderer => _itemsRenderer;

    public virtual void SetItemsRender(ISciItemsRenderer renderer)
    {
        _itemsRenderer = renderer;
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

    #region Selection

    protected virtual async Task OnItemActivateChangedAsync(TItem item)
    {
        var isSelected = IsItemSelected(item);

        if (MultiSelectEnabled)
        {
            var list = SelectedItems.ToList();
            if (isSelected) list.Remove(item);
            else list.Add(item);
            await OnItemsSelectChangedAsync(list);
        }
        else
        {
            await OnItemSelectChangedAsync(isSelected ? default : item);
        }
    }

    protected virtual async Task OnItemSelectChangedAsync(TItem? item)
    {
        SelectedItem = item;

        if (SelectedItemChanged.HasDelegate)
            await SelectedItemChanged.InvokeAsync(SelectedItem);

        if (!MultiSelectEnabled && SelectDelegate is not null)
        {
            var selectItem = new SelectionItem(item, GetText(item));
            SelectDelegate.DelegateSelectItem(selectItem);
        }

        var value = GetValue(item);
        await OnValueSelectChangedAsync(value);
    }

    protected virtual async Task OnItemsSelectChangedAsync(IEnumerable<TItem> items)
    {
        SelectedItems = items.ToList();

        if (SelectedItemsChanged.HasDelegate)
            await SelectedItemsChanged.InvokeAsync(SelectedItems);

        if (MultiSelectEnabled && SelectDelegate is not null)
        {
            var selectItems = SelectedItems
                .Select(u => new SelectionItem(u, GetText(u))).ToList();
            SelectDelegate.DelegateSelectItems(selectItems);
        }

        var values = SelectedItems.Select(GetValue);
        await OnValuesSelectChangedAsync(values!);
    }

    protected virtual bool IsItemSelected(TItem? item)
    {
        return MultiSelectEnabled
            ? SelectedItems.Contains(item)
            : Equals(SelectedItem, item);
    }


    public override async Task UnselectObjectAsync(object obj)
    {
        if (obj is not TItem item) return;

        var value = GetValue(item);

        if (MultiSelectEnabled)
            await OnValuesSelectChangedAsync(
                Values.Where(u => !Equals(u, value)));
        else
            await OnValueSelectChangedAsync(value);
    }

    protected string? RowClass(TItem item)
    {
        return IsItemSelected(item) ? " selected" : null;
    }

    #endregion

    #region Abstraction

    [Parameter]
    public Func<TItem, string?>? TextField { get; set; }

    private Func<TItem, TValue>? _valueField;

    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    [return: NotNullIfNotNull(nameof(item))]
    protected virtual TValue? GetValue(TItem? item)
    {
        if (item is null) return default;
        _valueField ??= ValueField?.Compile()!;
        return _valueField.Invoke(item)!;
    }

    protected virtual string? GetText(TItem? item)
    {
        return TextField is null || item is null ? item?.ToString() : TextField(item);
    }

    #endregion
}