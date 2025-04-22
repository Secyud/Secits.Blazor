using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Parameters;

public interface IClickComponent
{
    public EventCallback Click { get; set; }
}