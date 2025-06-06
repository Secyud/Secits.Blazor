using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class SItemsIteratorBase<TItem> : IScsSize, IScsTheme
{
    private bool _needRefresh = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_needRefresh)
        {
            _needRefresh = false;
            await ItemsRenderer.InvokeAsync(u => u.RefreshAsync());
            await InvokeAsync(StateHasChanged);
        }
    }

    #region Settings

    public SSetting<ISciRowRenderer<TItem>> RowRenderer { get; } = new();

    public SSettings<ISciHeaderRenderer> Headers { get; } = new();

    public SSettings<ISciFooterRenderer> Footers { get; } = new();
    public SSetting<ISciItemsRenderer<TItem>> ItemsRenderer { get; } = new();

    public Task RefreshAsync()
    {
        return InvokeAsync(() => { _needRefresh = true; });
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