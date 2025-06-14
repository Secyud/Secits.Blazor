using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableColumnResize<TItem> : ITableHeaderRenderer
{
    [Inject]
    private IJsDocument JsDocument { get; set; } = null!;

    private bool _isDrag;
    private long _dragEventId;
    private long _mouseupEventId;
    private long _mouseleaveEventId;
    private int _currentColumnIndex;

    protected override void ApplySetting()
    {
        Master.TableHeaders.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.TableHeaders.Forgo(this);
    }

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

    private async Task BeginDragColumnHeader(int index)
    {
        await SetDragAsync(true);
        _currentColumnIndex = index;
    }

    private async Task OnDragColumnHeader(MouseEventArgs args)
    {
        if (!_isDrag) return;

        var columns = Master.TableColumns;
        if (_currentColumnIndex >= columns.Count - 1) return;

        const int gap = 50;
        var column = columns[_currentColumnIndex];
        var nextColumn = columns[_currentColumnIndex + 1];
        var width = column.Width;
        var nextWidth = nextColumn.Width;
        var moveWith = (int)args.MovementX;
        if (moveWith < 0)
            moveWith = -Math.Min(width - gap, -moveWith);
        else if (_currentColumnIndex != columns.Count - 2)
            moveWith = Math.Min(nextWidth - gap, moveWith);
        else
            moveWith = Math.Min(1000 - gap, moveWith);
        column.Width += moveWith;
        nextColumn.Width -= moveWith;

        await InvokeAsync(StateHasChanged);
    }

    private async Task EndDragColumnHeader()
    {
        await SetDragAsync(false);
    }
}