using Book.API.Domain.Entities.UserAggregate;

namespace Book.API.Domain.Repositories.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(User user);

    Task<bool> IsEmailUniqueAsync(Email email);
}