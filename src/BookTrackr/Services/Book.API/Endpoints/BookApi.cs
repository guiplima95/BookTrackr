using Book.API.Application.Books.Create;
using Book.API.Application.Books.GetAllBooks;
using Book.API.Application.Books.GetBooks;
using Book.API.Domain.Abstractions;
using Book.API.Extensions;
using MediatR;
using System.Net;

namespace Book.API.Endpoints;

public sealed class BookApi : IEndpoint
{
    public void MapBookApiEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("books", CreateBook)
            .WithOpenApi()
            .WithName("CreateBook")
            .WithTags(Tags.Books)
            .Produces<Guid>((int)HttpStatusCode.Created);

        app.MapGet("books", GetBooks)
            .WithOpenApi()
            .WithName("GetBooks")
            .WithTags(Tags.Books)
            //.RequireAuthorization()
            .Produces<List<BookResponse>>((int)HttpStatusCode.OK)
            .CacheOutput(builder => builder
                .Expire(TimeSpan.FromMinutes(1))
                .Tag(Tags.Books));

        //.VaryByValue((httpContext, _) =>
        //{
        //    return ValueTask.FromResult(new KeyValuePair<string, string>(
        //        nameof(ClaimsPrincipalExtensions.UserId),
        //        httpContext.User.UserId().ToString()));
        //}), excludeDefaultPolicy: true);

        app.MapGet("books/{id:guid}", GetBook)
           .WithOpenApi()
           .WithName("GetBook")
           .WithTags(Tags.Books)
           .Produces<BookResponse>((int)HttpStatusCode.OK);
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

        Result<BookResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, MainApi.Problem);
    }

    public static async Task<IResult> CreateBook(
        CreateBookRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        CreateBookCommand command = new(
            request.Title,
            request.Description,
            request.Edition,
            request.ISBN,
            request.Publisher,
            request.PublisherYear,
            request.PageAmount,
            request.UserId,
            request.AuthorId,
            request.GenreId);

        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Created, MainApi.Problem);
    }
}