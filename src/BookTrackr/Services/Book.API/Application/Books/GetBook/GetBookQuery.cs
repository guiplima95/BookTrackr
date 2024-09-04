using Book.API.Application.Abstractions.Menssaging;

namespace Book.API.Application.Books.GetBooks;

public record GetBookQuery(Guid Id) : IQuery<BookResponse>;