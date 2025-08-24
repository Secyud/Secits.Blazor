using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SIteratorBase<TItem>
{
    private bool _needRefresh = true;
    public DataRequest DataRequest { get; } = new();

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

    public SSetting<IRowRenderer<TItem>> RowRenderer { get; } = new();
    public SSetting<IDataSourceProvider<TItem>> DataSource { get; } = new();
    public SSetting<IIteratorRenderer<TItem>> ItemsRenderer { get; } = new();

    public Task RefreshAsync()
    {
        return InvokeAsync(() => _needRefresh = true);
    }

    #endregion
}