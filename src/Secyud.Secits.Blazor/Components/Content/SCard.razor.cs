using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class SCard
{
    protected override string ComponentName => "card";

    #region Body

    public SSettings<IContentBodyRenderer> Body { get; } = new();

    #endregion
}