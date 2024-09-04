using Book.API.Application.Abstractions.Data;
using Book.API.Application.Abstractions.Menssaging;
using Book.API.Application.Books.GetBooks;
using Book.API.Domain.Abstractions;
using Dapper;

namespace Book.API.Application.Books.GetBook;

public sealed class GetBookQueryHandler(IDbConnectionFactory connectionFactory) : IQueryHandler<GetBookQuery, BookResponse>
{
    public async Task<Result<BookResponse>> Handle(GetBookQuery request, CancellationToken cancellationToken)
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
            WHERE B.Id = @BookId
            """;

        return await connection.QueryFirstOrDefaultAsync<BookResponse>(sql, new
        {
            BookId = request.Id
        });
    }
}