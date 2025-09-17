using System.ComponentModel.DataAnnotations;
using Secyud.Secits.Blazor.Validations;

namespace Secyud.Secits.Blazor.Element;

public partial class SModal
{
    private ValidationForm? _validationForm;

    public bool Validate()
    {
        return _validationForm is null ||
               _validationForm.Fields.All(field => field.ValidationResults.Count <= 0);
    }

    public List<ValidationResult> GetValidationResults()
    {
        if (_validationForm is null)
            return [];
        List<ValidationResult> results = [];

        foreach (var validationField in _validationForm.Fields)
        {
            results.AddRange(validationField.ValidationResults);
        }

        return results;
    }
}