using Book.API.Domain.Abstractions;

namespace Book.API.Domain.Entities.BookAggregate;

public class Book : Entity
{
    // EF constructor
    protected Book()
    {
        Title = string.Empty;
        Description = string.Empty;
        Edition = new(0, string.Empty);
        ISBN = string.Empty;
        Publisher = string.Empty;
    }

    public Book(
        string title,
        string description,
        Edition edition,
        string isbn,
        Guid authorId,
        string publisher,
        Guid genreId,
        short publishedYear,
        int pageAmount,
        decimal averageRating,
        Guid userId)
    {
        Title = title;
        Description = description;
        Edition = edition;
        ISBN = isbn;
        AuthorId = authorId;
        Publisher = publisher;
        GenreId = genreId;
        PublishedYear = publishedYear;
        PageAmount = pageAmount;
        AverageRating = averageRating;
        UserId = userId;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Edition Edition { get; private set; }
    public string ISBN { get; private set; }
    public Guid AuthorId { get; private set; }
    public Guid GenreId { get; private set; }
    public string Publisher { get; private set; }
    public short PublishedYear { get; private set; }
    public int PageAmount { get; private set; }
    public decimal AverageRating { get; private set; }
    public Guid UserId { get; private set; }
}