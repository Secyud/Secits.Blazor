namespace Secyud.Secits.Blazor;

public abstract partial class SContentBase 
{
    #region Settings

    public SSettings<ISciHeaderRenderer> Header { get; } = new();
    public SSettings<ISciFooterRenderer> Footer { get; } = new();

    #endregion
}