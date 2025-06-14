using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISchContent
{
    RenderFragment? ChildContent { get; set; }
}