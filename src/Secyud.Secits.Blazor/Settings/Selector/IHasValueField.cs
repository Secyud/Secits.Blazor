using System.Linq.Expressions;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// text field is need to get or set
/// the value of the item.
/// the value field name is also useful.
/// </summary>
public interface IHasValueField<TItem, TValue>
{
    Expression<Func<TItem, TValue>>? ValueField { get; set; }
}