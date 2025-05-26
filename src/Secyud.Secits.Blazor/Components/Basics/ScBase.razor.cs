using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// Serves as an abstract base class for components in the Secyud.Secits.Blazor framework.
/// Provides foundational functionality for rendering, managing attributes, and handling disposal.
/// Implements IAsyncDisposable to support asynchronous cleanup operations.
/// </summary>
/// <remarks>
/// This class is designed to be inherited by components that require customizable rendering
/// and attribute management. It provides methods and properties to control the rendering process,
/// manage CSS classes and styles, and handle component lifecycle events.
/// The BuildRenderTree method is overridden to define the structure of the rendered component,
/// including element attributes and child content. Derived classes can customize rendering
/// behavior by overriding virtual methods such as GenerateChildContent, GetClass, and GetStyle.
/// Additionally, this class supports asynchronous disposal through the DisposeAsync method,
/// allowing derived classes to implement custom cleanup logic in HandleDisposeAsync.
/// </remarks>
public abstract partial class ScBase : IAsyncDisposable
{
    protected abstract string ComponentName { get; }

    protected virtual string ElementName => "div";

    #region Parameters

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();


    [Parameter]
    public string? Class { get; set; }

    protected virtual string? GetClass() => Class;

    [Parameter]
    public string? Style { get; set; }

    protected virtual string? GetStyle() => Style;

    #endregion

    #region Render

    protected ElementReference Ref { get; set; }

    protected virtual RenderFragment GenerateContent() => GenerateDefaultContent();

    protected RenderFragment GenerateDefaultContent() => builder =>
    {
        builder.OpenElement(0, ElementName);
        builder.AddAttribute(1, "class", GetClass());
        builder.AddAttribute(2, "style", GetStyle());
        builder.AddAttribute(3, "s");
        var sequence = BuildContentExtra(builder, 3);
        builder.AddMultipleAttributes(sequence + 1, Attributes);
        builder.AddElementReferenceCapture(sequence + 2, r => Ref = r);
        builder.AddContent(sequence + 3, GenerateChildContent());
        builder.CloseElement();
    };

    protected virtual int BuildContentExtra(RenderTreeBuilder builder, int sequence)
    {
        return sequence;
    }

    protected virtual RenderFragment? GenerateChildContent() => null;

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

    public void TrySetAttribute<TValue>(ParameterView view, string parameterName, string attributeName)
    {
        if (!view.TryGetValue<TValue>(parameterName, out var value)) return;

        if (value is null) Attributes.Remove(attributeName);
        else Attributes[parameterName] = value;
    }

    public void TrySetAttribute<TValue>(ParameterView view, string parameterName)
    {
        TrySetAttribute<TValue>(view, parameterName, parameterName);
    }
}