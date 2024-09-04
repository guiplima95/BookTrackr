using Book.API.Domain.Entities.AuthorAggregate;
using Book.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Infrastructure.Persistence.Repositories;

public class AuthorRepository(BookContext context) : IAuthorRepository
{
    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Authors.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}