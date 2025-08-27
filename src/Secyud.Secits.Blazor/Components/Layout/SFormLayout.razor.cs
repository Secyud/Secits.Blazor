using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class SFormLayout : IHasLayoutTemplateSlot
{
    protected override string ComponentName => "form-layout";

    public SSettings<ILayoutTemplateRenderer> SlotRenderer { get; } = new();
}