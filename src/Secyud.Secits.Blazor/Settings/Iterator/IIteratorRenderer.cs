using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;
/// <summary>
/// describe ways to present data.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IIteratorRenderer<TItem>
{
    RenderFragment GenerateItems(RenderFragment<TItem> itemTemplate);
    Task RefreshAsync();
}