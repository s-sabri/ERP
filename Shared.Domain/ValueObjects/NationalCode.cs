using System.Text.RegularExpressions;
using Shared.Domain.Exceptions;

namespace Shared.Domain.ValueObjects;
public sealed class NationalCode : ValueObject
{
    public string Value { get; }

    private NationalCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException("کد ملی نمی‌تواند خالی باشد.");

        value = Normalize(value);

        if (!IsValidIranianNationalCode(value))
            throw new DomainValidationException($"کد ملی معتبر نیست: {value}");

        Value = value;
    }

    public static NationalCode Create(string value) => new NationalCode(value);
    private static string Normalize(string input)
    {
        return input.Trim()
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
    }
    private static bool IsValidIranianNationalCode(string code)
    {
        if (!Regex.IsMatch(code, @"^\d{10}$")) return false;

        var digits = code.Select(c => c - '0').ToArray();

        if (digits.All(d => d == digits[0])) return false; // همه ارقام یکسان نباشد

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += digits[i] * (10 - i);

        int remainder = sum % 11;
        int checkDigit = digits[9];

        return (remainder < 2 && checkDigit == remainder) ||
               (remainder >= 2 && checkDigit == 11 - remainder);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
    public static implicit operator string(NationalCode code) => code.Value;
    public static explicit operator NationalCode(string value) => Create(value);
}
