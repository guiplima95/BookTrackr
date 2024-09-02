using Book.API.Domain.Abstractions;

namespace Book.API.Infrastructure.Errors;

public record NotFoundError(
    string Code,
    string Description,
    ErrorType Type) : Error(Code, Description, Type)
{
    public static readonly NotFoundError UserNotFound = new(
        "Users.NotFound",
        "No user was found with this identifier.",
        ErrorType.NotFound);

    public static readonly NotFoundError BookNotFound = new(
        "Books.NotFound",
        "No book was found with this identifier.",
        ErrorType.NotFound);

    public static readonly NotFoundError GenreNotFound = new(
        "Genres.NotFound",
        "No genre was found with this identifier.",
        ErrorType.NotFound);

    public static readonly NotFoundError AuthorNotFound = new(
       "Authors.NotFound",
       "No author was found with this identifier.",
       ErrorType.NotFound);
}