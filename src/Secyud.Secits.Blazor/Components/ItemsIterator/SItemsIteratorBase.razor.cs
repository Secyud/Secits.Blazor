using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
    public SSetting<ISciDataSource<TItem>> DataSource { get; } = new();
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

    public DataRequest DataRequest { get; } = new();

    #endregion

    #region Renderer

    protected virtual string RowElementName => "div";

    protected virtual RenderFragment<TItem> GenerateRow() => item => builder =>
    {
        builder.OpenElement(0, RowElementName);
        var rowRender = RowRenderer.Get();
        if (rowRender is not null)
        {
            var @class = rowRender.GetRowClass(item);
            if (!string.IsNullOrWhiteSpace(@class))
                builder.AddAttribute(1, "class", @class);
            var style = rowRender.GetRowStyle(item);
            if (!string.IsNullOrWhiteSpace(@class))
                builder.AddAttribute(2, "style", style);
            if (rowRender.ClickEnabled)
                builder.AddAttribute(3, "onclick",
                    EventCallback.Factory.Create<MouseEventArgs>(
                        this, args => rowRender.OnRowClick(args, item)));
        }

        builder.AddContent(10, GenerateCol());
        builder.CloseElement();
    };

    #endregion
}