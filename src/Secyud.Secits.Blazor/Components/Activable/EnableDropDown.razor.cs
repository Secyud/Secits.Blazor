using Microsoft.AspNetCore.Components;
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

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public async Task OnDropDownClickAsync()
    {
        if (_isDropDownVisible)
            await OnCloseDropDownAsync();
        else
            await OnOpenDropDownAsync();
    }

    private long? _openDropDownId;

    public async Task OnCloseDropDownAsync()
    {
        _openDropDownId = await JsDocument.RemoveEventListener(_openDropDownId);
        _isDropDownVisible = false;
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnOpenDropDownAsync()
    {
        _openDropDownId = await JsDocument.RemoveEventListener(_openDropDownId);
        _openDropDownId = await JsDocument.AddEventListener(OnCloseDropDownAsync, "click");
        _isDropDownVisible = true;
        await InvokeAsync(StateHasChanged);
    }
}