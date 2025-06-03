using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Arguments;
using Secyud.Secits.Blazor.JSInterop;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// table组件,可以展示多行数据.
/// </summary>
/// <typeparam name="TItem"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public partial class STable<TItem> : IScsTheme
{
    protected override string ComponentName => "table";

    [Inject]
    private IJsDocument JsDocument { get; set; } = null!;

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    #region Columns

    private readonly List<ISciTableColumn<TItem>> _columns = [];

    public void AddTableColumn(ISciTableColumn<TItem> column)
    {
        RemoveTableColumn(column);
        _columns.Add(column);
    }

    public void RemoveTableColumn(ISciTableColumn<TItem> column)
    {
        _columns.Remove(column);
    }

    private bool _isDrag;
    private long _dragEventId;
    private long _mouseupEventId;
    private long _mouseleaveEventId;
    private TableDragEventType _dragEventType;
    private int _currentColumnIndex;

    private async Task SetDragAsync(bool isDrag)
    {
        if (isDrag == _isDrag) return;
        _isDrag = isDrag;
        if (_isDrag)
        {
            _dragEventId = await JsDocument.AddEventListener<MouseEventArgs>("mousemove", OnDragColumnHeader);
            _mouseupEventId = await JsDocument.AddEventListener("mouseup", EndDragColumnHeader);
            _mouseleaveEventId = await JsDocument.AddEventListener("mouseleave", EndDragColumnHeader);
        }
        else
        {
            await JsDocument.RemoveEventListener(_dragEventId);
            await JsDocument.RemoveEventListener(_mouseupEventId);
            await JsDocument.RemoveEventListener(_mouseleaveEventId);
        }
    }

    public async Task BeginDragColumnHeader(TableDragEventType type, ISciTableColumn<TItem> column)
    {
        await SetDragAsync(true);
        _dragEventType = type;
        _currentColumnIndex = _columns.IndexOf(column);
    }

    public async Task OnDragColumnHeader(MouseEventArgs args)
    {
        switch (_dragEventType)
        {
            case TableDragEventType.ColumnResize:
                if (_currentColumnIndex == _columns.Count - 1) return;
                const int gap = 50;
                var column = _columns[_currentColumnIndex];
                var nextColumn = _columns[_currentColumnIndex + 1];
                var width = column.Width;
                var nextWidth = nextColumn.Width;
                var moveWith = (int)args.MovementX;
                if (moveWith < 0)
                    moveWith = -Math.Min(width - gap, -moveWith);
                else
                    moveWith = Math.Min(nextWidth - gap, moveWith);
                await InvokeAsync(() =>
                {
                    column.Width += moveWith;
                    nextColumn.Width -= moveWith;
                    StateHasChanged();
                });
                break;
            case TableDragEventType.ColumnMove:
                // TODO
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task EndDragColumnHeader()
    {
        await SetDragAsync(false);
    }

    #endregion

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }
}