using Book.API.Domain.AuthorAggregate;
using Book.API.Domain.GenreAggregate;
using Book.API.Domain.UserAggregate;
using Book.API.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

public class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book.API.Domain.BookAggregate.Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfigurations())
                    .ApplyConfiguration(new AuthorEntityTypeConfigurations())
                    .ApplyConfiguration(new BookEntityTypeConfigurations())
                    .ApplyConfiguration(new GenreEntityTypeConfigurations());
    }
}