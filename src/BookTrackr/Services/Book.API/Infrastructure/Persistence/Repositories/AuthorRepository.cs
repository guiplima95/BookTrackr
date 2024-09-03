using Book.API.Domain.Entities.AuthorAggregate;
using Book.API.Domain.Repositories;

namespace Book.API.Infrastructure.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}