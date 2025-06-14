namespace Secyud.Secits.Blazor;

public partial class SCard
{
    protected override string ComponentName => "card";

    #region Body

    public SSettings<ISciBodyRenderer> Body { get; } = new();

    #endregion
}