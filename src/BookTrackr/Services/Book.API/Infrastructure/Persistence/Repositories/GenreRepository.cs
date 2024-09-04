using Book.API.Domain.Entities.GenreAggregate;
using Book.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Infrastructure.Persistence.Repositories;

public class GenreRepository(BookContext context) : IGenreRepository
{
    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Genres.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}