namespace Secyud.Secits.Blazor;

public interface ISelectComponent<TValue> :
    IValueComponent<TValue>
{
}

public interface ISelectComponent<TItem, TValue> :
    ISelectComponent<TValue>, IDataComponent<TItem>
{
}