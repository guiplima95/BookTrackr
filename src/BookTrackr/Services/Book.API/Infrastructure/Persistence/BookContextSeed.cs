using Book.API.Domain.AuthorAggregate;
using Book.API.Domain.BookAggregate;
using Book.API.Domain.GenreAggregate;
using Book.API.Domain.UserAggregate;
using Book.API.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace Book.API.Infrastructure.Persistence;

public class BookContextSeed
{
    public async Task SeedAsync(BookContext context, IWebHostEnvironment env, ILogger<BookContextSeed> logger)
    {
        if (!env.IsDevelopment())
        {
            return;
        }

        AsyncRetryPolicy policy = CreatePolicy(logger, nameof(BookContextSeed));

        await policy.ExecuteAsync(async () =>
        {
            if (!await context.Books.AnyAsync())
            {
                await SeedDefaultBook(context);
            }
        });
    }

    private static AsyncRetryPolicy CreatePolicy(ILogger<BookContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<SqlException>()
            .WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: _ => TimeSpan.FromSeconds(5),
                onRetry: (exception, _, retry, __) => logger.LogExceptionOnRetry(exception, prefix, retry, retries)
            );
    }

    private static async Task SeedDefaultBook(BookContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            var userCreated = Domain.UserAggregate.User.Create(
                address: new Address("Av. Professor Djalma Guimaraes, 2275", "Santa Luzia", "Minas Gerais", "33170010", "Brazil"),
                cpf: new CPF("111111996601"),
                email: new Email("guilhermelucas.contato@gmail.com"),
                name: new Name("Guilherme Lima"),
                password: new Password("12345678"));

            await context.Users.AddAsync(userCreated);
            await context.SaveChangesAsync();
        }

        if (!await context.Genres.AnyAsync())
        {
            var genreCreated = Genre.Create(description: "Technology");

            await context.Genres.AddAsync(genreCreated);
            await context.SaveChangesAsync();
        }

        if (!await context.Authors.AnyAsync())
        {
            Author authorCreated = new(
                born: "5 December 1952",
                died: null,
                description: "colloquially called Uncle Bob, is an American software engineer, instructor, and author. He is most recognized for promoting many software design principles and for being an author and signatory of the influential Agile Manifesto.",
                name: new Name("Robert C. Martin"),
                books: []);

            await context.Authors.AddAsync(authorCreated);
            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            string queryAuthor = $"SELECT * FROM dbo.Authors WHERE Name = 'Robert C. Martin'";
            string queryUser = $"SELECT * FROM dbo.Users WHERE Name = 'Guilherme Lima'";
            string queryGenrer = $"SELECT * FROM dbo.Genres WHERE Description = 'Technology'";

            var user = await context.Users.FromSqlRaw<Domain.UserAggregate.User>(queryUser).FirstOrDefaultAsync();
            var genre = await context.Genres.FromSqlRaw<Genre>(queryGenrer).FirstOrDefaultAsync();
            var author = await context.Authors.FromSqlRaw<Author>(queryAuthor).FirstOrDefaultAsync();

            Domain.BookAggregate.Book bookCreated = new(
                title: "Clean Code: A Handbook of Agile Software Craftsmanship",
                description: "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way.",
                edition: new Edition(2, "2nd edition"),
                isbn: "9780136083238",
                author?.Id ?? Guid.NewGuid(),
                publisher: "Pearson",
                genreId: genre?.Id ?? Guid.NewGuid(),
                publishedYear: 2008,
                pageAmount: 422,
                averageRating: 0,
                userId: user?.Id ?? Guid.NewGuid());

            await context.Books.AddAsync(bookCreated);
            await context.SaveChangesAsync();
        }
    }
}