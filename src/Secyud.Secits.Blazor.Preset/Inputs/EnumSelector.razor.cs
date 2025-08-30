using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

public partial class EnumSelector<TEnum> where TEnum : struct, Enum
{
    private TEnum[]? _allItems;

    private TEnum[] AllItems => _allItems ??= Enum.GetValues<TEnum>();

    [Parameter]
    public IReadOnlyList<TEnum> Items { get; set; } = [];

    [Parameter]
    public RenderFragment<TEnum>? ListItemTemplate { get; set; }

    [Parameter]
    public TEnum Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TEnum> ValueChanged { get; set; }

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendClass("flex");
    }

    private EnableDropDown? _enableDropDown;

    protected async Task OnCloseDropDownAsync()
    {
        if (_enableDropDown is not null)
            await _enableDropDown.OnCloseDropDownAsync();
    }

    protected async Task OnDropDownClickAsync()
    {
        if (_enableDropDown is not null)
            await _enableDropDown.OnDropDownClickAsync();
    }

    public IReadOnlyList<TEnum> GetList()
    {
        return Items.Count == 0 ? AllItems : Items;
    }
}