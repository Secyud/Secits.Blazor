using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class STableColumnSetting<TItem, TValue> :
    ITableColumnSetting<TItem>, IFilteredComponent, IFieldComponent<TItem, TValue>
{
    [Parameter]
    public string? Caption { get; set; }

    [Parameter]
    public Expression<Func<TItem, TValue>>? Field { get; set; }

    [Parameter]
    public bool? EnableFilter { get; set; }

    [Parameter]
    public bool? EnableSorter { get; set; }

    [Parameter]
    public bool? EnableHeader { get; set; }

    protected Type? FieldType { get; private set; }

    #region LifeCycle

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue(nameof(Field),
                out Expression<Func<TItem, object?>>? field))
        {
            if (field?.Body is MemberExpression me)
            {
                Filter.Field = me.Member.Name;
                Sorter.Field = me.Member.Name;
                FieldType = me.Member switch
                {
                    FieldInfo fi => fi.FieldType,
                    PropertyInfo pi => pi.PropertyType,
                    _ => FieldType
                };
            }
            else
            {
                Filter.Field = null;
                Sorter.Field = null;
                FieldType = null;
            }
        }

        await base.SetParametersAsync(parameters);
    }

    protected override void ApplySetting()
    {
        MasterComponent?.AddColumn(this);
    }

    protected void UnsetTable()
    {
        MasterComponent?.RemoveColumn(this);
    }

    #endregion

    protected async Task SetFilterValueAsync(object? filterValue)
    {
        Filter.FilterValue = filterValue;
        if (MasterComponent is not null)
            await MasterComponent.OnDataLoadAsync();
    }

    public virtual bool RenderHeader()
    {
        if (EnableHeader is not null)
            return EnableHeader.Value;
        return MasterComponent?.EnableHeader ?? true;
    }

    public virtual bool RenderFilter()
    {
        if (EnableFilter is not null)
            return EnableFilter.Value;
        return MasterComponent?.EnableFilter ?? false;
    }

    public virtual bool RenderSorter()
    {
        if (EnableSorter is not null)
            return EnableSorter.Value;
        return MasterComponent?.EnableSorter ?? false;
    }

    protected TValue? GetField(TItem item)
    {
        var de = Field?.Compile();
        return de is null ? default : de(item);
    }

    public DataFilter Filter { get; } = new();
    public DataSorter Sorter { get; } = new();
}