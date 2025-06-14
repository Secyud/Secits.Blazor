namespace Secyud.Secits.Blazor.Settings;
/// <summary>
/// text field is need to get the
/// text of the item.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IHasTextField<TItem>
{
    public Func<TItem, string?>? TextField { get; set; }
}