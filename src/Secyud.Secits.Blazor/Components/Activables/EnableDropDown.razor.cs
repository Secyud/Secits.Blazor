using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Element;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableDropDown : IHasContent
{
    private bool _isDropDownVisible;

    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Inject]
    private IJsDocument JsDocument { get; set; } = null!;

    [Inject]
    private IJsElement JsElement { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public override RendererPosition GetLayoutPosition()
    {
        return RendererPosition.Body;
    }

    public async Task OnDropDownClickAsync()
    {
        if (_isDropDownVisible)
            await OnCloseDropDownAsync();
        else
            await OnOpenDropDownAsync();
    }

    private long? _openDropDownId;
    private ElementReference? _dropDownContent;
    private SIcon? _icon;

    public async Task OnCloseDropDownAsync()
    {
        _openDropDownId = await JsDocument.RemoveEventListener(_openDropDownId);
        _isDropDownVisible = false;
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnOpenDropDownAsync()
    {
        _openDropDownId = await JsDocument.RemoveEventListener(_openDropDownId);
        _openDropDownId = await JsDocument.AddEventListener<MouseEventArgs>(OnDocumentClickAsync, "click");
        _isDropDownVisible = true;
        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnDocumentClickAsync(MouseEventArgs args)
    {
        if (!_dropDownContent.HasValue || _icon is not { ElementRef: not null }) return;
        var rc = await JsElement.GetBoundingClientRect(_dropDownContent.Value);
        var ri = await JsElement.GetBoundingClientRect(_icon.ElementRef.Value);
        var rect = new DomRect()
        {
            Top = Math.Min(rc.Top, ri.Top),
            Bottom = Math.Max(rc.Bottom, ri.Bottom),
            Left = Math.Min(rc.Left, ri.Left),
            Right = Math.Max(rc.Right, ri.Right),
        };
        if (rect.ContainsPoint(args.ClientX, args.ClientY)) return;
        await OnCloseDropDownAsync();
    }
}