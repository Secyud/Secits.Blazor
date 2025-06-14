namespace Secyud.Secits.Blazor;

public interface IScwMinMaxValue<TValue>
    where TValue : IComparable<TValue>
{
    TValue Max { get; set; }
    TValue Min { get; set; }
}