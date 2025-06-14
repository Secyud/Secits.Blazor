using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISccClick
{
    public EventCallback Click { get; set; }
}