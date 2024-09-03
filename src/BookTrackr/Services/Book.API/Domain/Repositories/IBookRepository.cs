namespace Book.API.Domain.Repositories;

public interface IBookRepository
{
    Task<bool> IsBookingExistsAsync(
        Guid userId,
        string isbn,
        CancellationToken cancellationToken = default);

    Task AddAsync(Entities.BookAggregate.Book book, CancellationToken cancellationToken = default);
}