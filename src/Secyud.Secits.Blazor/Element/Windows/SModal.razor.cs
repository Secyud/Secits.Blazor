namespace Secyud.Secits.Blazor.Element;

public partial class SModal
{
    private SValidationForm? _validationForm;

    public bool CheckValidate()
    {
        return _validationForm is null ||
               _validationForm.Validations.All(u => u.IsValid);
    }
}