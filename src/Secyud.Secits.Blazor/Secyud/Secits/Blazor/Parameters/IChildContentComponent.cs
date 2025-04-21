using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Parameters;

public interface IChildContentComponent
{
    RenderFragment? ChildContent { get; set; }
}