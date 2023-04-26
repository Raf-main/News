namespace Raf.Utils.Shared.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset OffsetNow()
    {
        return DateTimeOffset.UtcNow;
    }

    public DateTimeOffset OffsetUtcNow()
    {
        return DateTimeOffset.Now;
    }

    public DateTime Now()
    {
        return DateTime.Now;
    }

    public DateTime UtcNow()
    {
        return DateTime.UtcNow;
    }
}