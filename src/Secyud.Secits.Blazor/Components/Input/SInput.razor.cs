using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class SInput<TValue>
{
    protected override string ComponentName => "input";
    protected SSelectableContainer SelectableContainer { get; }

    public SInput()
    {
        SelectableContainer = new SSelectableContainer(this);
    }


    #region Settings

    public SSettings<IValueContainer<TValue>> ValueContainer { get; } = new();

    public SSetting<IValueParser<TValue>> ValueConverter { get; } = new();

    public SSetting<IInputInvoker<TValue>> InputInvoker { get; } = new();

    #endregion
}