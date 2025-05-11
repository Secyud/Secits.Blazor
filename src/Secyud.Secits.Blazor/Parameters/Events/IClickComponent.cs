using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface IClickComponent
{
    public EventCallback Click { get; set; }
}