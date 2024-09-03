using Book.API.Domain.Entities.AuthorAggregate;

namespace Book.API.Domain.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}