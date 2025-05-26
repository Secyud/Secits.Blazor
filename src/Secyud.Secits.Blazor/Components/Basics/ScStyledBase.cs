using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;
using Secyud.Secits.Blazor.Styles;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// Represents an abstract base class for business-related components in the Secyud.Secits.Blazor namespace.
/// This class provides foundational functionality for managing class styles, parameters, and dirty state tracking.
/// It extends the ScBase class and includes mechanisms for dynamically building class and style attributes
/// based on component properties and interfaces.
/// </summary>
public abstract class ScStyledBase : ScBase
{
    protected ScStyledBase()
    {
        ClassStyleBuilder = new ClassStyleBuilder(BuildClassStyle);
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (DirtyParameter.CheckDirty(parameters))
            ClassStyleBuilder.SetDirty();

        return base.SetParametersAsync(parameters);
    }

    #region Dirty

    private DirtyParameterContainer? _dirtyParameter;

    private DirtyParameterContainer DirtyParameter =>
        _dirtyParameter ??= DirtyParameterContainer.GetOrCreateByComponent(this);

    public virtual void SetDirtyParameter(DirtyParameterContainer dp)
    {
        dp.AddDirtyParameters(nameof(Class), nameof(Style));

        if (!dp.AddIfIs<IScsSize>(this,
                ParameterExtensions.AppendSize,
                nameof(IScsSize.Width),
                nameof(IScsSize.Height)))
        {
            dp.AddIfIs<IScsHeight>(this,
                ParameterExtensions.AppendHeight,
                nameof(IScsHeight.Height));
            dp.AddIfIs<IScsWidth>(this,
                ParameterExtensions.AppendWidth,
                nameof(IScsWidth.Width));
        }

        dp.AddIfIs<IScsTheme>(this,
            ParameterExtensions.AppendTheme,
            nameof(IScsTheme.Theme),
            nameof(IScsTheme.Size),
            nameof(IScsTheme.StyleOption));

        dp.AddIfIs<IScsActive>(this,
            ParameterExtensions.AppendActivable,
            nameof(IScsActive.Disabled),
            nameof(IScsActive.Readonly));
    }

    #endregion

    #region ClassStyle

    private ClassStyleBuilder ClassStyleBuilder { get; }

    protected virtual void BuildInitialClassStyle(ClassStyleBuilderContext context)
    {
    }

    protected void BuildClassStyle(ClassStyleBuilderContext context)
    {
        if (!string.IsNullOrEmpty(ComponentName))
            context.Class.Append(ComponentName);

        BuildInitialClassStyle(context);
        context.AppendClass(Class);
        context.Style.Append(Style);
        DirtyParameter.BuildDirtyClassStyle(this, context);
    }

    protected override string? GetClass() => ClassStyleBuilder.Class;

    protected override string? GetStyle() => ClassStyleBuilder.Style;

    #endregion
}