using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISchContent
{
    RenderFragment? ChildContent { get; set; }
}