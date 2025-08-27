namespace Secyud.Secits.Blazor.Settings;

public interface IHasCurrentValues<TValue>
{
    List<TValue> CurrentValues { get; set; }
}