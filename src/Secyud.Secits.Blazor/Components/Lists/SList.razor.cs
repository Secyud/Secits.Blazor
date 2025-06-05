using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TItem))]
public partial class SList<TItem>
{
    protected override string ComponentName => "list";
    
    #region Settings

    private readonly List<ISciColumnRenderer<TItem>> _columns = [];

    public IReadOnlyList<ISciColumnRenderer<TItem>> Columns => _columns;

    public virtual void AddColumnRender(ISciColumnRenderer<TItem> renderer)
    {
        RemoveColumnRender(renderer);
        _columns.Add(renderer);
    }

    public virtual void RemoveColumnRender(ISciColumnRenderer<TItem> renderer)
    {
        _columns.Remove(renderer);
    }

    #endregion
}