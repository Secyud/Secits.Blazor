using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

public class AddShowBox<TValue> : AddTemplateBase<TValue>
{
    [Parameter]
    public EventCallback<string?> TextChanged { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttributeIfNotEmpty(2, "name", Master.Name);
        builder.AddAttribute(3, "class", GetClass());
        builder.AddAttribute(4, "style", GetStyle());
        builder.AddAttribute(5, "value", CurrentString);
        builder.AddAttribute(6, "onchange",
            EventCallback.Factory.CreateBinder<string?>(this, OnChangeAsync, InputString));
        builder.AddAttribute(7, "oninput",
            EventCallback.Factory.CreateBinder<string?>(this, OnInputAsync, InputString));
        builder.SetUpdatesAttributeName("value");
        builder.CloseElement();
    }

    protected override Task OnInputValueHandleAsync()
    {
        TextChanged.InvokeAsync(CurrentString);
        return Task.CompletedTask;
    }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result)
    {
        throw new NotImplementedException("this method should not be called in AddShowBox.");
    }
}