using System.Text.RegularExpressions;
using Shared.Domain.Exceptions;

namespace Shared.Domain.ValueObjects;
public sealed class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException("شماره تماس نمی‌تواند خالی باشد.");

        var cleaned = Normalize(value);

        if (!IsValidFormat(cleaned))
            throw new DomainValidationException($"فرمت شماره تماس معتبر نیست: {value}");

        Value = cleaned;
    }

    public static PhoneNumber Create(string value) => new PhoneNumber(value);
    private static string Normalize(string input)
    {
        var normalized = input.Trim()
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("۰", "0")
            .Replace("۱", "1")
            .Replace("۲", "2")
            .Replace("۳", "3")
            .Replace("۴", "4")
            .Replace("۵", "5")
            .Replace("۶", "6")
            .Replace("۷", "7")
            .Replace("۸", "8")
            .Replace("۹", "9");

        return normalized;
    }
    private static bool IsValidFormat(string value)
    {
        const string pattern = @"^0\d{10}$|^0\d{2,3}\d{7,8}$";
        return Regex.IsMatch(value, pattern);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
    public static implicit operator string(PhoneNumber phone) => phone.Value;
    public static explicit operator PhoneNumber(string value) => Create(value);
}
