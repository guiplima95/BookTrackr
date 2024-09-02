namespace Book.API.Domain.Repositories.Books;

public interface IBookRepository
{
    public Task<bool> IsBookingExistsAsync(
       Guid userId,
       Guid bookId,
       CancellationToken cancellationToken = default);

    Task AddAsync(Entities.BookAggregate.Book book);
}