using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

public class AddTextArea : AddTemplateBase<string?>
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "textarea");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttributeIf(!string.IsNullOrEmpty(NameAttributeValue),
            2, "name", NameAttributeValue);
        builder.AddAttribute(3, "class", GetClass());
        builder.AddAttribute(4, "style", GetStyle());
        builder.AddAttribute(5, "value", InputString);
        builder.AddAttribute(6, "onchange",
            EventCallback.Factory.CreateBinder<string?>(this, OnChangeAsync, InputString));
        builder.AddAttribute(7, "oninput",
            EventCallback.Factory.CreateBinder<string?>(this, OnInputAsync, InputString));
        builder.SetUpdatesAttributeName("value");
        builder.CloseElement();
    }

    protected override bool TryParseValueFromString(string? value, out string? result)
    {
        result = value;
        return true;
    }
}