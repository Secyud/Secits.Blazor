using System.Linq.Expressions;

namespace Secyud.Secits.Blazor;

public interface ITextFieldComponent<TItem>
{
    public Expression<Func<TItem, string?>>? TextField { get; set; }
}