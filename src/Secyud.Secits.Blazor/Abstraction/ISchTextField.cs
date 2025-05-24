namespace Secyud.Secits.Blazor.Abstraction;

public interface ISchTextField<TItem>
{
    public Func<TItem, string?>? TextField { get; set; }
}