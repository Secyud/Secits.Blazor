using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract partial class SMaskableInputBase<TValue>
{
    #region Life Cycle

    protected override void OnValueParameterSet(TValue value)
    {
        base.OnValueParameterSet(value);

        var text = value?.ToString();
        ResetCurrentStringFromValue(text);
        _parsingFailed = false;
    }

    protected abstract bool TryParseValueFromString(string? value, out TValue output);

    #endregion

    #region Mask

    public IInputMask? InputMask { get; set; }

    private bool _isMasked;
    private string? _maskedString;

    private bool TryParseMask(string? text)
    {
        _originString = text;
        if (InputMask is null) return false;
        var res = InputMask.TryParseMask(text, out _maskedString);

        // ensure input mask worked for masked string to value
        if (!InputMask.TryConvertMaskToText(_maskedString, out var origin))
        {
            throw new InvalidCastException(
                $"Please check mask: {InputMask}, TryConvertMaskToText failed!" +
                $"\r\nmasked string: {_maskedString}.");
        }

        _originString = origin;

        return res;
    }

    private bool TryConvertTextToMask(string? text)
    {
        if (InputMask is null) return false;
        return InputMask.TryConvertTextToMask(text,
            out _maskedString);
    }

    #endregion

    #region ValueHandle

    private string? _currentString;
    private string? _originString;
    private bool _parsingFailed;

    protected override void OnInputValueHandle(object? obj)
    {
        // handle string
        var inputText = obj?.ToString();
        _originString = inputText;
        _isMasked = TryParseMask(inputText);
        _currentString = _isMasked ? _maskedString : _originString;

        // handle value
        string? value;
        if (_isMasked && InputMask is not null)
            InputMask.TryConvertMaskToText(_maskedString, out value);
        else
            value = _originString;

        _parsingFailed = !TryParseValueFromString(value, out var output);

        if (!_parsingFailed) CurrentValue = output;
    }

    protected void ResetCurrentStringFromValue(string? value)
    {
        _originString = value;
        _isMasked = TryConvertTextToMask(value);
        _currentString = _isMasked ? _maskedString : _originString;
    }

    #endregion

    #region Clear

    [Parameter]
    public bool ClearButtonVisible { get; set; } = true;

    protected void ClearValue()
    {
        OnValueParameterSet(default!);
        SubmitChange();
    }

    #endregion
}