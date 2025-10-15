using System.Globalization;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class InputCheckTemplate : SLayoutPluginBase<SInput<bool>>, IValueContainer
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets the value to be used for the input's "name" attribute.
    /// </summary>
    protected string? NameAttributeValue
    {
        get
        {
            if (AdditionalAttributes?.TryGetValue("name", out var nameAttributeValue) ?? false)
            {
                return Convert.ToString(nameAttributeValue, CultureInfo.InvariantCulture);
            }

            return null;
        }
    }

    public override RendererPosition GetLayoutPosition()
    {
        return RendererPosition.Body;
    }

    public override RenderFragment RenderTemplate()
    {
        return builder =>
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "checkbox");
            builder.AddAttributeIf(!string.IsNullOrEmpty(NameAttributeValue),
                3, "name", NameAttributeValue);
            builder.AddAttribute(4, "class", GetClass());
            builder.AddAttribute(5, "style", GetStyle());
            builder.AddAttribute(6, "value", bool.TrueString);
            builder.AddAttribute(7, "onchange",
                EventCallback.Factory.CreateBinder<bool>(this, OnCheckedChangedAsync,
                    Master.InputInvoker.Get()?.GetActiveItem() ?? false));
            builder.SetUpdatesAttributeName("checked");
            builder.CloseElement();
        };
    }

    protected async Task OnCheckedChangedAsync(bool value)
    {
        await Master.InputInvoker.InvokeAsync(u => u.SetActiveItemAsync(this, value));
    }

    public Task OnValueUpdatedAsync(object? sender, bool applied)
    {
        return Task.CompletedTask;
    }
}