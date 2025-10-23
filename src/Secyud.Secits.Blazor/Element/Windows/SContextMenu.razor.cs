using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Services;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SContextMenu : IHasContent, IAsyncDisposable
{
    [Inject]
    private SecitsApp App { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool HideMode { get; set; }

    [Parameter]
    public Func<ElementReference?>? ExtendElement { get; set; }

    [Parameter]
    public bool Visible { get; set; }

    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    private bool _visible;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (Visible) await ShowAsync();
        else await HideAsync();
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-context-menu", Class);
    }

    protected override string GetStyle()
    {
        return $"display:{(_visible ? "static" : "none")};{Style}";
    }

    public async Task HideAsync()
    {
        if (!_visible) return;
        _visible = false;
        App.OnClick -= OnDocumentClickAsync;
        await VisibleChanged.InvokeAsync(_visible);
        await InvokeAsync(StateHasChanged);
    }

    public async Task ShowAsync()
    {
        if (_visible) return;
        _visible = true;
        App.OnClick += OnDocumentClickAsync;
        await VisibleChanged.InvokeAsync(_visible);
        await InvokeAsync(StateHasChanged);
    }

    protected void OnDocumentClickAsync(object? sender, MouseEventArgs args)
    {
        HideAsync().ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await HideAsync();
    }
}