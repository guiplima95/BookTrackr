using Book.API.Domain.Repositories.Books;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Infrastructure.Persistence.Repositories;

public class BookRepository(BookContext context) : IBookRepository
{
    public async Task AddAsync(Domain.Entities.BookAggregate.Book book)
    {
        await context.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public Task<bool> IsBookingExistsAsync(
        Guid userId,
        Guid bookId,
        CancellationToken cancellationToken = default) =>
        context.Books.AnyAsync(
            b => b.UserId == userId && b.Id == bookId, cancellationToken);
}