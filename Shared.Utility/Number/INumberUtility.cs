namespace Shared.Utility.Number
{
    public interface INumberUtility
    {
        string FormatDecimal(decimal value, string format = "#,##0.##", string culture = "fa-IR");
    }
}
