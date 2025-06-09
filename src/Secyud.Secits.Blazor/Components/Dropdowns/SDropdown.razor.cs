namespace Secyud.Secits.Blazor.Components;

public partial class SDropdown : IScdSelect
{
    protected override string ComponentName => "dropdown";
    protected override string ElementName => "input";


    protected async Task ClearSelectAsync()
    {
        await Selector.InvokeAsync(u => u.ClearSelectAsync());
    }

    public SSetting<ISciSelect> Selector { get; } = new();
}