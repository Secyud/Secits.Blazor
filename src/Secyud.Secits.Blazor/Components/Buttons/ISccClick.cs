using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISccClick
{
    public EventCallback Click { get; set; }
}