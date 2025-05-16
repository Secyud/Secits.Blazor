using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISelectComponent<TValue>
{
    EventCallback<IEnumerable<TValue>> SelectedValuesChanged { get; set; }
    IEnumerable<TValue> SelectedValues { get; set; }

    EventCallback<TValue?> SelectedValueChanged { get; set; }
    TValue? SelectedValue { get; set; }

    bool MultiSelectEnabled { get; set; }

    void OnValueSelect(TValue? value) => DefaultOnValueSelect(this, value);

    bool IsSelected(TValue value) => DefaultIsSelected(this, value);

    static bool DefaultIsSelected(ISelectComponent<TValue> component, TValue value)
    {
        return component.MultiSelectEnabled
            ? component.SelectedValues.Contains(value)
            : Equals(component.SelectedValue, value);
    }

    static void DefaultOnValueSelect(ISelectComponent<TValue> component, TValue? value)
    {
        component.SelectedValue = value;
        if (component.SelectedValueChanged.HasDelegate)
            component.SelectedValueChanged.InvokeAsync(value);

        if (!component.MultiSelectEnabled || value is null) return;

        var list = component.SelectedValues.ToList();
        if (!list.Remove(value)) list.Add(value);
        component.SelectedValues = list;
        if (component.SelectedValuesChanged.HasDelegate)
            component.SelectedValuesChanged.InvokeAsync(list);
    }
}

public interface ISelectComponent<TItem, TValue> :
    ISelectComponent<TValue>,
    IDataComponent<TItem>,
    ITextFieldComponent<TItem>,
    IValueFieldComponent<TItem, TValue>
{
}