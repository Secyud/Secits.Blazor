using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public interface IScList<TItem> : IScwItems<TItem>
{
    RenderFragment GenerateRow(TItem item);
}