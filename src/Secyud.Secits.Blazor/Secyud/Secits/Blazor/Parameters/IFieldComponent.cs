using System.Linq.Expressions;

namespace Secyud.Secits.Blazor.Parameters;

public interface IFieldComponent<TItem>
{
    public Expression<Func<TItem, object?>>? Field { get; set; }
}