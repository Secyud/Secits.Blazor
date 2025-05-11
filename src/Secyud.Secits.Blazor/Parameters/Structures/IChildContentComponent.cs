using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface IChildContentComponent
{
    RenderFragment? ChildContent { get; set; }
}