using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Basic;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Components;

public class STableColumnSetting<TItem> : SSettingComp,
    ITableColumnSetting<TItem>, IFilteredComponent, IAsyncDisposable
{
    [CascadingParameter]
    public STable<TItem>? Table { get; set; }

    [Parameter]
    public string? Caption { get; set; }

    [Parameter]
    public Expression<Func<TItem, object?>>? Field { get; set; }

    [Parameter]
    public bool? EnableFilter { get; set; }

    [Parameter]
    public bool? EnableSorter { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        foreach (var parameter in parameters)
        {
            if (parameter.Name == nameof(Field) &&
                Field?.Body is MemberExpression me)
            {
                Filter.Field = me.Member.Name;
                Sorter.Field = me.Member.Name;
            }
        }
    }

    protected override void OnInitialized()
    {
        Table?.AddColumn(this);
        base.OnInitialized();
    }

    public virtual bool RenderHeader()
    {
        return true;
    }

    public RenderFragment GenerateHeader() => builder =>
    {
        if (Caption is not null)
        {
            builder.AddContent(0, Caption);
        }
    };

    public virtual bool RenderFilter()
    {
        if (EnableFilter is not null)
            return EnableFilter.Value;
        return Table?.EnableFilter ?? false;
    }

    public RenderFragment GenerateFilter()
    {
        throw new NotImplementedException();
    }

    public bool RenderSorter()
    {
        if (EnableSorter is not null)
            return EnableSorter.Value;
        return Table?.EnableSorter ?? false;
    }

    public RenderFragment GenerateBody(TItem item) => builder =>
    {
        if (Field is not null)
        {
            builder.AddContent(0, Field.Compile()(item));
        }
    };

    public DataFilter Filter { get; } = new();
    public DataSorter Sorter { get; } = new();


    public ValueTask DisposeAsync()
    {
        if (Table is not null && !Table.IsDisposed)
        {
            Table.RemoveColumn(this);
        }

        return ValueTask.CompletedTask;
    }
}