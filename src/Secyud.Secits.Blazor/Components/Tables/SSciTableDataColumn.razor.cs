using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public partial class SSciTableDataColumn<TItem, TValue> :
    ISciTableColumnRenderer<TItem>, ISchValueField<TItem, TValue>
{
    [Parameter]
    public string? Caption { get; set; }

    [Parameter]
    public int MaxWidth { get; set; }

    [Parameter]
    public int MinWidth { get; set; }

    [Parameter]
    public int Width { get; set; } = 100;

    [Parameter]
    public string? Format { get; set; }

    private Func<TItem, TValue>? _valueField;

    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    [Parameter]
    public bool EnableFilter { get; set; }

    [Parameter]
    public bool EnableSorter { get; set; }

    #region LifeCycle

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField), field =>
        {
            var name = field?.GetBodyName();
            _valueField = field?.Compile();
            Filter.Field = name;
            Sorter.Field = name;
        });

        await base.SetParametersAsync(parameters);
    }

    protected override void ApplySetting()
    {
        Master?.TableColumns.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master?.TableColumns.Forgo(this);
    }

    #endregion

    protected async Task SetFilterValueAsync(object? filterValue)
    {
        Filter.FilterValue = filterValue;
        if (Master is not null)
            await Master.RefreshAsync();
    }

    [return: NotNullIfNotNull(nameof(item))]
    protected TValue? GetField(TItem? item)
    {
        return _valueField is null || item is null ? default : _valueField(item);
    }

    public DataFilter Filter { get; } = new();
    public DataSorter Sorter { get; } = new();

    public int GetColumnWidth()
    {
        var width = Width;
        if (MaxWidth > 0 && width > MaxWidth) width = MaxWidth;
        if (MinWidth > width) width = MinWidth;
        return width;
    }
}