using System.Linq.Expressions;

namespace Secyud.Secits.Blazor;

public interface IFieldComponent<TItem, TValue>
{
    public Expression<Func<TItem, TValue>>? Field { get; set; }
}