using System.Linq.Expressions;

namespace Secyud.Secits.Blazor.Abstraction;

public interface ISchValueField<TItem, TValue>
{
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }
}