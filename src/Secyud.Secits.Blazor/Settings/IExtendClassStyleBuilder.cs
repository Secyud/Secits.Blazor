namespace Secyud.Secits.Blazor.Settings;

public interface IExtendClassStyleBuilder : IIsSetting
{
    void BuildExtendClassStyle(ClassStyleContext context);
}