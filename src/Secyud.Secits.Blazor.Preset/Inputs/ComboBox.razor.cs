using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Preset;

public partial class ComboBox<TItem, TValue> : IHasValue<TValue>
{
    [Parameter]
    public IReadOnlyList<TItem> Items { get; set; } = [];


    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    [Parameter]
    public Func<TValue, Task<TItem>> ItemFinder { get; set; } = null!;

    [Parameter]
    public RenderFragment<TItem>? ListItemTemplate { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendClass("flex");
    }
}