using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public interface IHasLayoutTemplateSlot
{
    SSettings<IContentRenderer> SlotRenderer { get; }
}