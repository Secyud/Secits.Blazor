﻿using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Layout;

public partial class LayoutMenu
{
    protected bool MenuFixed { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected string GetClass()
    {
        List<string> classes = [];
        if (Class is not null)
            classes.Add(Class);
        if (MenuFixed)
            classes.Add("fixed");

        return string.Join(' ', classes);
    }

    protected async Task ChangeFixedAsync()
    {
        MenuFixed = !MenuFixed;
        await InvokeAsync(StateHasChanged);
    }
}