using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class SItemsIteratorBase<TItem> : IScsSize, IScsTheme
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await RefreshAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    #region Settings

    private ISciItemsRenderer<TItem>? _itemsRenderer;
    public ISciItemsRenderer<TItem>? ItemsRenderer => _itemsRenderer;

    public virtual void SetItemsRender(ISciItemsRenderer<TItem> renderer)
    {
        _itemsRenderer = renderer;
    }

    public void UnsetItemsRender(ISciItemsRenderer<TItem> renderer)
    {
        if (_itemsRenderer == renderer)
            _itemsRenderer = null;
    }

    public async Task RefreshAsync()
    {
        if (_itemsRenderer is not null)
            await _itemsRenderer.RefreshAsync();
    }

    private ISciRowRenderer<TItem>? _rowRenderer;

    public ISciRowRenderer<TItem>? RowRenderer => _rowRenderer;

    public virtual void SetRowRender(ISciRowRenderer<TItem> renderer)
    {
        _rowRenderer = renderer;
    }

    public void UnsetRowRender(ISciRowRenderer<TItem> renderer)
    {
        if (_rowRenderer == renderer)
            _rowRenderer = null;
    }

    private readonly List<ISciHeaderRenderer> _headers = [];

    public IReadOnlyList<ISciHeaderRenderer> Headers => _headers;

    public virtual void AddHeaderRender(ISciHeaderRenderer renderer)
    {
        RemoveHeaderRender(renderer);
        _headers.Add(renderer);
    }

    public virtual void RemoveHeaderRender(ISciHeaderRenderer renderer)
    {
        _headers.Remove(renderer);
    }

    private readonly List<ISciFooterRenderer> _footers = [];

    public IReadOnlyList<ISciFooterRenderer> Footers => _footers;

    public virtual void AddFooterRender(ISciFooterRenderer renderer)
    {
        RemoveFooterRender(renderer);
        _footers.Add(renderer);
    }

    public virtual void RemoveFooterRender(ISciFooterRenderer renderer)
    {
        _footers.Remove(renderer);
    }

    #endregion

    #region Parameters

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }

    [Parameter]
    public SValue Width { get; set; }

    [Parameter]
    public SValue Height { get; set; }

    [Parameter]
    public Func<DataRequest, Task<DataResult<TItem>>>? Items { get; set; }

    public DataRequest DataRequest { get; } = new();

    #endregion
}