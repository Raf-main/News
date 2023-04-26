namespace Raf.Utils.Shared.Time;

public interface IDateTimeProvider
{
    DateTimeOffset OffsetNow();
    DateTimeOffset OffsetUtcNow();
    DateTime Now();
    DateTime UtcNow();
}