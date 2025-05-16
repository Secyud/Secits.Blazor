namespace Secyud.Secits.Blazor;

public interface IActivableComponent
{
    bool Readonly { get; set; }
    bool Disabled { get; set; }
}