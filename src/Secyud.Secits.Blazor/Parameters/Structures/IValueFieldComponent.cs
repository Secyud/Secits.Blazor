using System.Linq.Expressions;

namespace Secyud.Secits.Blazor;

public interface IValueFieldComponent<TItem, TValue>
{
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }
}