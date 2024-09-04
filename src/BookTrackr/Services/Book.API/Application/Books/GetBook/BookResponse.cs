namespace Book.API.Application.Books.GetBooks;

public sealed record BookResponse(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    int PageAmount,
    decimal AverageRating,
    string Author);