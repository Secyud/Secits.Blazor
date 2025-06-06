using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Secyud.Secits.Blazor.Components;

public partial class SVisualizeItems<TItem> : ISciItemsRenderer<TItem>
{
    private Virtualize<TItem>? _virtualize;
    private bool _needRefreshData;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_needRefreshData && _virtualize is not null)
        {
            _needRefreshData = false;
            await _virtualize.RefreshDataAsync();
            StateHasChanged();
        }
    }

    public async ValueTask<ItemsProviderResult<TItem>> RefreshRowsAsync(ItemsProviderRequest request)
    {
        if (Master.Items is null) return default;
        Master.DataRequest.SkipCount = request.StartIndex;
        Master.DataRequest.PageSize = request.Count;
        Master.DataRequest.CancellationToken = request.CancellationToken;
        var result = await Master.Items(Master.DataRequest);
        return new ItemsProviderResult<TItem>(result.Items, result.TotalCount);
    }

    public Task RefreshAsync()
    {
        _needRefreshData = true;
        return InvokeAsync(StateHasChanged);
    }
}