namespace Secyud.Secits.Blazor;

public interface IMinMaxValueComponent<TValue>
    where TValue : IComparable<TValue>
{
    TValue Max { get; set; }
    TValue Min { get; set; }
}