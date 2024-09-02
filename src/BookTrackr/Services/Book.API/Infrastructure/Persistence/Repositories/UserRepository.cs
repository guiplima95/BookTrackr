using Book.API.Domain.Entities.UserAggregate;
using Book.API.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(BookContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await context.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await context.Users.AnyAsync(u => u.Email == email);
    }
}