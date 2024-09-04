using Book.API.Application.Abstractions.Menssaging;
using Book.API.Application.Books.GetBooks;

namespace Book.API.Application.Books.GetAllBooks;

public sealed record GetAllBooksQuery(string UserEmail) : IQuery<List<BookResponse>>;