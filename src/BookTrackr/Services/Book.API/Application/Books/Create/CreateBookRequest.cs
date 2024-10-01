using Book.API.Domain.Entities.BookAggregate;

namespace Book.API.Application.Books.Create;

public record CreateBookRequest(
    string Title,
    string Description,
    Edition Edition,
    string ISBN,
    string Publisher,
    short PublisherYear,
    int PageAmount,
    Guid UserId,
    Guid AuthorId,
    Guid GenreId);