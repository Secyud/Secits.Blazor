using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Basic;

public abstract class SComponentBase : ComponentBase, IAsyncDisposable
{
    protected abstract string ComponentName { get; }

    protected virtual string ElementName => "div";

    #region Parameters

    [Parameter(CaptureUnmatchedValues = true)]
    public virtual IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();


    [Parameter]
    public string? Class { get; set; }

    protected virtual string? GetClass() => Class;

    [Parameter]
    public string? Style { get; set; }

    protected virtual string? GetStyle() => Style;

    #endregion

    #region Render

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, ElementName);
        builder.AddAttribute(1, "class", GetClass());
        builder.AddAttribute(2, "style", GetStyle());
        builder.AddMultipleAttributes(3, Attributes);
        builder.AddContent(4, GenerateChildContent());
        builder.CloseElement();
    }

    protected virtual RenderFragment? GenerateChildContent()
    {
        return null;
    }

    #endregion

    #region Dispose

    public bool IsDisposed { get; private set; }

    public async ValueTask DisposeAsync()
    {
        if (IsDisposed) return;

        await HandleDisposeAsync();

        IsDisposed = true;
    }

    protected virtual ValueTask HandleDisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    #endregion
}