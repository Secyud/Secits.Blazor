using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;
/// <summary>
/// describe ways to present data.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IIteratorRenderer<TValue>
{
    RenderFragment GenerateItems(RenderFragment<TValue> itemTemplate);
    Task RefreshAsync();
}