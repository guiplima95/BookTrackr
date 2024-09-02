using Book.API.Domain.Entities.AuthorAggregate;
using Book.API.Domain.Entities.GenreAggregate;
using Book.API.Domain.Entities.UserAggregate;
using Book.API.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

public class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book.API.Domain.Entities.BookAggregate.Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfigurations())
                    .ApplyConfiguration(new AuthorEntityTypeConfigurations())
                    .ApplyConfiguration(new BookEntityTypeConfigurations())
                    .ApplyConfiguration(new GenreEntityTypeConfigurations());
    }
}