namespace Secyud.Secits.Blazor;

public interface ISchTextField<TItem>
{
    public Func<TItem, string?>? TextField { get; set; }
}