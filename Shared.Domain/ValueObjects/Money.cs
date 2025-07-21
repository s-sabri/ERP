using Shared.Domain.Exceptions;
using System.Globalization;

namespace Shared.Domain.ValueObjects;

/// <summary>
/// یک ValueObject برای نمایش مبلغ به‌همراه واحد پول.
/// </summary>
public sealed class Money : ValueObject
{
    private const int Scale = 2;

    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new DomainValidationException("مبلغ نمی‌تواند منفی باشد.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainValidationException("واحد پول نمی‌تواند خالی باشد.");

        Amount = Math.Round(amount, Scale);
        Currency = NormalizeCurrency(currency);
    }

    public static Money Create(decimal amount, string currency) => new Money(amount, currency);
    public static Money Zero(string currency) => new Money(0, currency);
    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return Create(Amount + other.Amount, Currency);
    }
    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        if (Amount < other.Amount)
            throw new DomainValidationException("مبلغ نهایی نمی‌تواند منفی باشد.");

        return Create(Amount - other.Amount, Currency);
    }
    public Money Multiply(decimal factor) => Create(Amount * factor, Currency);
    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainValidationException("واحد پول باید یکسان باشد.");
    }
    private static string NormalizeCurrency(string currency) => currency.Trim().ToUpperInvariant();
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
    public override string ToString() => string.Format(CultureInfo.InvariantCulture, "{0:N2} {1}", Amount, Currency);

    public static implicit operator decimal(Money money) => money.Amount;
}
