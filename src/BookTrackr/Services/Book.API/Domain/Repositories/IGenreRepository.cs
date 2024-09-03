using Book.API.Domain.Entities.GenreAggregate;

namespace Book.API.Domain.Repositories;

public interface IGenreRepository
{
    Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}