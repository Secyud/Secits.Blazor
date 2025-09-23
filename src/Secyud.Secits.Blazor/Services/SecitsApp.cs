using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Services;

public class SecitsApp
{
    public EventHandler<MouseEventArgs>? OnClick;

    public void Click(object? sender, MouseEventArgs args)
    {
        OnClick?.Invoke(sender, args);
    }
}