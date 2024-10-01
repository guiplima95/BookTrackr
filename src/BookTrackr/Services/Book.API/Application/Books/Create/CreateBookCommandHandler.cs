using Book.API.Application.Abstractions.Menssaging;
using Book.API.Domain.Abstractions;
using Book.API.Domain.Entities.AuthorAggregate;
using Book.API.Domain.Entities.GenreAggregate;
using Book.API.Domain.Entities.UserAggregate;
using Book.API.Domain.Repositories;
using Book.API.Domain.Repositories.Users;
using Book.API.Infrastructure.Errors;

using BookEntity = Book.API.Domain.Entities.BookAggregate.Book;

namespace Book.API.Application.Books.Create;

public sealed class CreateBookCommandHandler(
    IUserRepository userRepository,
    IGenreRepository genreRepository,
    IAuthorRepository authorRepository,
    IBookRepository bookRepository) : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(NotFoundError.UserNotFound);
        }

        Genre? genre = await genreRepository.GetByIdAsync(request.GenreId, cancellationToken);
        if (genre is null)
        {
            return Result.Failure<Guid>(NotFoundError.GenreNotFound);
        }

        Author? author = await authorRepository.GetByIdAsync(request.GenreId, cancellationToken);
        if (author is null)
        {
            return Result.Failure<Guid>(NotFoundError.AuthorNotFound);
        }

        bool bookAlreadyExists = await bookRepository
            .IsBookingExistsAsync(request.UserId, request.ISBN, cancellationToken);

        if (bookAlreadyExists)
        {
            return Result.Failure<Guid>(ConflictError.ISBNNotUnique);
        }

        BookEntity book = new(
            request.Title,
            request.Description,
            request.Edition,
            request.ISBN,
            request.AuthorId,
            request.Publisher,
            request.GenreId,
            request.PublisherYear,
            request.PageAmount,
            averageRating: 0,
            request.UserId);

        await bookRepository.AddAsync(book, cancellationToken);

        return Result.Success(book.Id);
    }
}