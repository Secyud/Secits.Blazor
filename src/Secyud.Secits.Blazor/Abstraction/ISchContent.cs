using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Abstraction;

public interface ISchContent
{
    RenderFragment? ChildContent { get; set; }
}