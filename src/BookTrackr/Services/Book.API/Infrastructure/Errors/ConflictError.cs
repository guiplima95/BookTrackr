using Book.API.Domain.Abstractions;

namespace Book.API.Infrastructure.Errors;

public record ConflictError(string Code, string Description, ErrorType Type) : Error(Code, Description, Type)
{
    public static readonly ConflictError EmailNotUnique = new(
        "Users.EmailNotUnique",
        "There is already a user with this email.",
        ErrorType.Conflict);
}