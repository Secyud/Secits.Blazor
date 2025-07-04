﻿using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// setting for all column footer.
/// etc. summary.
/// </summary>
public interface ITableFooterRenderer : IIsSetting
{
    RenderFragment GenerateFooter(int index);
}