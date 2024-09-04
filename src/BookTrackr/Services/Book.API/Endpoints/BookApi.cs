using Book.API.Application.Books.GetAllBooks;
using Book.API.Application.Books.GetBooks;
using Book.API.Domain.Abstractions;
using Book.API.Extensions;
using MediatR;

namespace Book.API.Endpoints;

public sealed class BookApi : IEndpoint
{
    public void MapBookApiEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("books", GetBooks)
            .WithOpenApi()
            .WithName("GetBooks")
            .WithTags(Tags.Books);

        app.MapGet("books/{id:guid}", GetBook)
           .WithOpenApi()
           .WithName("GetBook")
           .WithTags(Tags.Books);
    }

    public static async Task<IResult> GetBooks(
        ISender sender,
        CancellationToken cancellationToken)
    {
        string userEmail = "guilhermelucas.contato@gmail.com"; // TODO: I will get user by user logged

        GetAllBooksQuery query = new(userEmail);

        Result<List<BookResponse>> result = await sender.Send(
            query, cancellationToken);

        return result.Match(Results.Ok, MainApi.Problem);
    }

    public static async Task<IResult> GetBook(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetBookQuery query = new(id);

        Result<BookResponse> result = await sender.Send(
            query, cancellationToken);

        return result.Match(Results.Ok, MainApi.Problem);
    }
}