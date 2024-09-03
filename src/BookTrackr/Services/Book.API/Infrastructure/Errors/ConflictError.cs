using Book.API.Domain.Abstractions;

namespace Book.API.Infrastructure.Errors;

public record ConflictError(string Code, string Description, ErrorType Type) : Error(Code, Description, Type)
{
    public static readonly ConflictError EmailNotUnique = new(
        "Users.EmailNotUnique",
        "There is already a user with this email.",
        ErrorType.Conflict);

    public static readonly ConflictError ISBNNotUnique = new(
        "Books.BookNotUnique",
        "There is already a book with this isbn.",
        ErrorType.Conflict);
}