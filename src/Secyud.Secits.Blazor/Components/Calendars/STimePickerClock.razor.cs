using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.JSInterop;

namespace Secyud.Secits.Blazor.Components;

public partial class STimePickerClock
{
    protected override string ComponentName => "pkr-t clock";

    private ClockState _clockState = ClockState.Default;

    [Inject]
    private IJsElement Element { get; set; } = null!;

    protected override int BuildContentExtra(RenderTreeBuilder builder, int sequence)
    {
        builder.AddAttribute(sequence + 1, "onmouseup", OnMouseUp);
        builder.AddAttribute(sequence + 2, "onmouseleave", OnMouseUp);
        builder.AddAttribute(sequence + 3, "ontouchend", OnMouseUp);
        builder.AddAttribute(sequence + 4, "onmousemove", OnMouseMove);

        return sequence + 4;
    }

    private async Task<(double, double)> GetPointer(MouseEventArgs e)
    {
        var rect = await Element.GetBoundingClientRect(Ref);
        var centerX = (rect.Left + rect.Right) / 2;
        var centerY = (rect.Top + rect.Bottom) / 2;
        var pointerX = e.ClientX - centerX;
        var pointerY = e.ClientY - centerY;
        return (pointerX, pointerY);
    }

    private double GetMouseAngle((double, double) pointer)
    {
        var (x, y) = pointer;
        return Math.Atan2(x, -y) * 180 / Math.PI + 360;
    }

    private bool GetMouseDistance((double, double) pointer)
    {
        var (x, y) = pointer;
        var distance = x * x + y * y;
        return distance < 48 * 48;
    }

    private async Task OnMouseMove(MouseEventArgs e)
    {
        switch (_clockState)
        {
            case ClockState.Default:
                break;
            case ClockState.Hour:
            {
                var pointer = await GetPointer(e);
                var angle = GetMouseAngle(pointer);
                var inner = GetMouseDistance(pointer);
                var hour = (int)Math.Round(angle / 30) % 12;
                if (hour == 0) hour = 12;
                Hour = hour + (inner ? 0 : 12);
            }
                break;
            case ClockState.Minute:
            {
                var pointer = await GetPointer(e);
                var angle = GetMouseAngle(pointer);
                Minute = (int)Math.Round(angle / 6);
            }
                break;
            case ClockState.Second:
            {
                var pointer = await GetPointer(e);
                var angle = GetMouseAngle(pointer);
                Second = (int)Math.Round(angle / 6);
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnMouseDownHour(MouseEventArgs e)
    {
        _clockState = ClockState.Hour;
    }

    private void OnMouseDownMinute(MouseEventArgs e)
    {
        _clockState = ClockState.Minute;
    }

    private void OnMouseDownSecond(MouseEventArgs e)
    {
        _clockState = ClockState.Second;
    }

    private void OnMouseUp(MouseEventArgs e)
    {
        _clockState = ClockState.Default;
    }

    private bool HoverInner => Hour is <= 12 and > 0;

    private (string, string, string) GetCssClass()
    {
        var h = new StringBuilder();
        var m = new StringBuilder();
        var s = new StringBuilder();
        h.Append("hour hand");
        m.Append("minute hand");
        s.Append("second hand");

        if (HoverInner) h.Append(" inner");

        switch (_clockState)
        {
            case ClockState.Default:
                break;
            case ClockState.Hour:
                h.Append(" active");
                break;
            case ClockState.Minute:
                m.Append(" active");
                break;
            case ClockState.Second:
                s.Append(" active");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return (h.ToString(), m.ToString(), s.ToString());
    }


    protected enum ClockState
    {
        Default,
        Hour,
        Minute,
        Second
    }
}