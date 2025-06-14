using System.Linq.Expressions;

namespace Secyud.Secits.Blazor;

public interface ISchValueField<TItem, TValue>
{
    Expression<Func<TItem, TValue>>? ValueField { get; set; }
}