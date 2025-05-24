using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Abstraction;

public interface ISccClick
{
    public EventCallback Click { get; set; }
}