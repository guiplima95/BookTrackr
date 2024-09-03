using Book.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Infrastructure.Persistence.Repositories;

public class BookRepository(BookContext context) : IBookRepository
{
    public async Task AddAsync(Domain.Entities.BookAggregate.Book book, CancellationToken cancellationToken)
    {
        await context.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> IsBookingExistsAsync(
        Guid userId,
        string isbn,
        CancellationToken cancellationToken = default) =>
        context.Books.AnyAsync(
            b => b.UserId == userId && b.ISBN == isbn, cancellationToken);
}