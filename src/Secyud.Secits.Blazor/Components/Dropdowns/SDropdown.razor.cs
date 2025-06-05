namespace Secyud.Secits.Blazor.Components;

public partial class SDropdown : IScdSelect
{
    protected override string ComponentName => "dropdown";
    protected override string ElementName => "input";

    private ISciSelect? _component;

    public void BindComponent(ISciSelect component)
    {
        _component = component;
    }

    public void UnbindComponent(ISciSelect component)
    {
        if (component == _component)
            _component = null;
    }

    protected async Task ClearSelectAsync()
    {
        if (_component is not null) 
            await _component.ClearSelectAsync();
    }
}