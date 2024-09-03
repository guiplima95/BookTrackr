using Book.API.Domain.Entities.GenreAggregate;
using Book.API.Domain.Repositories;

namespace Book.API.Infrastructure.Persistence.Repositories;

public class GenreRepository : IGenreRepository
{
    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}