using Book.API.Application.Abstractions;
using Book.API.Domain.Abstractions;
using Book.API.Domain.Entities.UserAggregate;
using Book.API.Domain.Repositories.Books;
using Book.API.Domain.Repositories.Users;
using Book.API.Infrastructure.Errors;

namespace Book.API.Application.Books.Create;

public sealed class CreateBookCommandHandler(
    IUserRepository userRepository,
    IBookRepository bookRepository) : ICommandHandler<CreateBookCommand>
{
    public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        // Verify User:
        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(ConflictError.EmailNotUnique);
        }

        // Get Genre:

        // Get Author:

        // Add Book:

        var book = new Domain.Entities.BookAggregate.Book(
            request.Title,
            request.Description,
            request.Edition,
            request.ISBN,
            request.AuthorId,
            request.Publisher,
            request.GenreId,
            request.PublisherYear,
            request.PageAmount,
            0,
            request.UserId);

        await bookRepository.AddAsync(book);

        return Result.Success();
    }
}