using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SIteratorBase<TValue>
{
    public DataRequest DataRequest { get; } = new();

    protected virtual string? GetRowClass(TValue value)
    {
        var @class = ComponentName + "-row";
        if (RowRenderer.Get() is { } rowRenderer && rowRenderer.GetRowClass(value) is { } cls)
        {
            @class += " " + cls;
        }

        return @class;
    }

    protected virtual string? GetRowStyle(TValue value)
    {
        return RowRenderer.Get()?.GetRowStyle(value);
    }

    protected void OnRowClick(MouseEventArgs args, TValue value)
    {
        RowRenderer.Get()?.OnRowClick(args, value);
    }

    #region Settings

    public SSetting<IRowRenderer<TValue>> RowRenderer { get; } = new();
    public SSetting<IDataSourceProvider<TValue>> DataSource { get; } = new();
    public SSetting<IIteratorRenderer<TValue>> ItemsRenderer { get; } = new();

    public Task RefreshAsync(bool resetState)
    {
        return ItemsRenderer.InvokeAsync(u => u.RefreshAsync(resetState));
    }

    #endregion
}