using Book.API.Application.Abstractions.Data;
using Book.API.Application.Abstractions.Menssaging;
using Book.API.Application.Books.GetBooks;
using Book.API.Domain.Abstractions;
using Dapper;

namespace Book.API.Application.Books.GetAllBooks;

public sealed class GetAllBooksQueryHandler(IDbConnectionFactory connectionFactory) : IQueryHandler<GetAllBooksQuery, List<BookResponse>>
{
    public async Task<Result<List<BookResponse>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetOpenConnection();

        const string sql = """
            SELECT
                B.Id,
                B.Title,
                B.Description,
                G.Description AS Genre,
                B.PageAmount,
                B.AverageRating,
                A.Name AS Author
            FROM Books B
            INNER JOIN Genres G ON B.GenreId = G.Id
            INNER JOIN Authors A ON B.AuthorId = A.Id
            INNER JOIN Users U ON B.UserId = U.Id
            WHERE U.Email = @UserEmail
            """;

        return (await connection.QueryAsync<BookResponse>(sql, new
        {
            request.UserEmail
        })).ToList();
    }
}