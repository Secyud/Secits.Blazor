using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Basic;

public abstract class SBasicComp : SComponentBase
{
    protected SBasicComp()
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

        if (!dp.AddIfIs<ISizeComponent>(this,
                ParameterExtensions.AppendSize,
                nameof(ISizeComponent.Width),
                nameof(ISizeComponent.Height)))
        {
            dp.AddIfIs<IHeightComponent>(this,
                ParameterExtensions.AppendHeight,
                nameof(IHeightComponent.Height));
            dp.AddIfIs<IWidthComponent>(this,
                ParameterExtensions.AppendWidth,
                nameof(IWidthComponent.Width));
        }

        dp.AddIfIs<IColorComponent>(this,
            ParameterExtensions.AppendColor,
            nameof(IColorComponent.Color));
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
        {
            context.Class.Append("s-");
            context.Class.Append(ComponentName);
        }

        BuildInitialClassStyle(context);
        context.AppendClass(Class);
        context.Style.Append(Style);
        DirtyParameter.BuildDirtyClassStyle(this, context);
    }

    protected override string? GetClass() => ClassStyleBuilder.Class;

    protected override string? GetStyle() => ClassStyleBuilder.Style;

    #endregion
}