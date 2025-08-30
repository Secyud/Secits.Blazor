using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class GridColumn<TValue, TField> :
    ITableColumnRenderer<TValue>, IHasValueField<TValue, TField>
{
    private Func<TValue, TField>? _valueField;
    private int _columnWidth = 50;

    [Parameter]
    public string? Caption { get; set; }

    [Parameter]
    public int MaxWidth { get; set; }

    [Parameter]
    public int MinWidth { get; set; } = 50;

    [Parameter]
    public int Width { get; set; }

    [Parameter]
    public string? Format { get; set; }

    [Parameter]
    public Expression<Func<TValue, TField>>? ValueField { get; set; }

    [Parameter]
    public bool EnableFilter { get; set; }

    [Parameter]
    public bool EnableSorter { get; set; }

    public string GetColClass()
    {
        var cls = "grid-col";
        if (Master.TableColumns.FirstOrDefault() == this)
        {
            if (Master.FixFirstColumn) cls += " fix-left";
        }
        else if (Master.TableColumns.LastOrDefault() == this)
        {
            if (Master.FixLastColumn) cls += " fix-right";
        }

        return cls;
    }

    public int RealWidth
    {
        get => _columnWidth;
        set => _columnWidth = Math.Clamp(value, MinWidth,
            MaxWidth > MinWidth ? MaxWidth : int.MaxValue);
    }

    #region LifeCycle

    protected override void PreParametersSet(ParameterContainer parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField), field =>
        {
            var name = field?.GetBodyName();
            _valueField = field?.Compile();
            Filter.Field = name;
            Sorter.Field = name;
        });

        parameters.UseParameter(Width, nameof(Width), width => RealWidth = width);
    }

    protected override void ApplySetting()
    {
        Master.TableColumns.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.TableColumns.Forgo(this);
    }

    #endregion

    protected async Task SetFilterValueAsync(object? filterValue)
    {
        Filter.FilterValue = filterValue;
        await Master.RefreshAsync();
    }

    [return: NotNullIfNotNull(nameof(item))]
    protected TField? GetField(TValue? item)
    {
        return _valueField is null || item is null ? default : _valueField(item);
    }

    public DataFilter Filter { get; } = new();
    public DataSorter Sorter { get; } = new();
}