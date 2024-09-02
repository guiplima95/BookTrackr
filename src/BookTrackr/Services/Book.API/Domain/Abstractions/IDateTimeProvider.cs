namespace Book.API.Domain.Abstractions;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}