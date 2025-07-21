using System.Globalization;

namespace Shared.Utility.Number
{
    public class NumberUtility : INumberUtility
    {
        public string FormatDecimal(decimal value, string format = "#,##0.##", string culture = "fa-IR")
        {
            return value.ToString(format, new CultureInfo(culture));
        }
    }
}
