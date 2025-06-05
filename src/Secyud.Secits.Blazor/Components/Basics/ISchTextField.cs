namespace Secyud.Secits.Blazor.Components;

public interface ISchTextField<TItem>
{
    public Func<TItem, string?>? TextField { get; set; }
}