using System.Globalization;
using Shared.Domain.Exceptions;

namespace Shared.Domain.ValueObjects;

public sealed class PersianDateTime : ValueObject
{
    public string Value { get; }

    public string Date { get; }
    public TimeSpan Time { get; }

    public int Year { get; }
    public int Month { get; }
    public int Day { get; }

    public int Hour => Time.Hours;
    public int Minute => Time.Minutes;
    public int Second => Time.Seconds;

    private PersianDateTime(string datePart, TimeSpan time)
    {
        if (!IsValidPersianDateTime(datePart, time))
            throw new DomainValidationException("فرمت تاریخ و زمان شمسی نامعتبر است.");

        Date = datePart.Trim();
        Time = time;

        var parts = Date.Split('/');
        Year = int.Parse(parts[0]);
        Month = int.Parse(parts[1]);
        Day = int.Parse(parts[2]);

        Value = Time != TimeSpan.Zero
            ? $"{Year:0000}/{Month:00}/{Day:00} {Time:hh\\:mm\\:ss}"
            : $"{Year:0000}/{Month:00}/{Day:00}";
    }

    public static PersianDateTime Create(string dateTime)
    {
        if (string.IsNullOrWhiteSpace(dateTime))
            throw new DomainValidationException("تاریخ نمی‌تواند خالی باشد.");

        var parts = dateTime.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var datePart = parts[0];
        var time = parts.Length > 1 && TimeSpan.TryParse(parts[1], out var t) ? t : TimeSpan.Zero;

        return new PersianDateTime(datePart, time);
    }
    public static PersianDateTime FromParts(string date, TimeSpan time) => new PersianDateTime(date, time);
    public static PersianDateTime FromDateOnly(string date) => new PersianDateTime(date, TimeSpan.Zero);

    public DateTime ToGregorian()
    {
        var calendar = new PersianCalendar();
        return calendar.ToDateTime(Year, Month, Day, Hour, Minute, Second, 0);
    }
    public static PersianDateTime FromGregorian(DateTime dateTime)
    {
        var calendar = new PersianCalendar();

        int y = calendar.GetYear(dateTime);
        int m = calendar.GetMonth(dateTime);
        int d = calendar.GetDayOfMonth(dateTime);

        var time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        return FromParts($"{y:0000}/{m:00}/{d:00}", time);
    }
    private static bool IsValidPersianDateTime(string persianDate, TimeSpan time)
    {
        try
        {
            var parts = persianDate.Split('/');
            if (parts.Length != 3) return false;

            int y = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);
            int d = int.Parse(parts[2]);

            if (m is < 1 or > 12) return false;
            if (d is < 1 or > 31) return false;

            var calendar = new PersianCalendar();
            _ = calendar.ToDateTime(y, m, d, time.Hours, time.Minutes, time.Seconds, 0);

            return true;
        }
        catch
        {
            return false;
        }
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
    public static implicit operator string(PersianDateTime date) => date.Value;
    public static explicit operator PersianDateTime(string value) => Create(value);
}
