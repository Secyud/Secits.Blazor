using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SContentBase 
{
    #region Settings

    public SSettings<IContentHeaderRenderer> Header { get; } = new();
    public SSettings<IContentFooterRenderer> Footer { get; } = new();

    #endregion
}