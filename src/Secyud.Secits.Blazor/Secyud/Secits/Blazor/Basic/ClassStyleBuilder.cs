namespace Secyud.Secits.Blazor.Basic;

/// <summary>
/// 
/// </summary>
/// <param name="buildAction">
/// class builder, style builder
/// </param>
public class ClassStyleBuilder(Action<ClassStyleBuilderContext> buildAction)
{
    private readonly ClassStyleBuilderContext _context = new();
    public bool IsDirty { get; private set; } = true;

    public void SetDirty()
    {
        IsDirty = true;
    }

    private string? _class;

    public string? Class
    {
        get
        {
            TryBuild();
            return _class;
        }
    }

    private string? _style;

    public string? Style
    {
        get
        {
            TryBuild();
            return _style;
        }
    }

    private void TryBuild()
    {
        if (!IsDirty) return;

        _context.Clear();
        buildAction(_context);
        var @class = _context.Class.ToString();
        var style = _context.Style.ToString();
        _class = string.IsNullOrWhiteSpace(@class) ? null : @class;
        _style = string.IsNullOrWhiteSpace(style) ? null : style;
    }
}