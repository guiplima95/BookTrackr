using Book.API.Domain.AuthorAggregate;
using Book.API.Domain.GenreAggregate;
using Book.API.Domain.SeedWork;
using Book.API.Domain.UserAggregate;

namespace Book.API.Domain.BookAggregate;

public class Book : Entity
{
    private Book(
        string title,
        string description,
        Edition edition,
        string iSBN,
        int idAuthor,
        string publisher,
        int idGenre,
        short publishedYear,
        short pageAmount,
        decimal averageRating,
        List<User> users)
    {
        Title = title;
        Description = description;
        Edition = edition;
        ISBN = iSBN;
        IdAuthor = idAuthor;
        Publisher = publisher;
        IdGenre = idGenre;
        PublishedYear = publishedYear;
        PageAmount = pageAmount;
        AverageRating = averageRating;
        Users = users;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Edition Edition { get; private set; }
    public string ISBN { get; private set; }
    public int IdAuthor { get; private set; }
    public Author? Author { get; private set; }
    public string Publisher { get; private set; }
    public int IdGenre { get; private set; }
    public Genre? Genre { get; private set; }
    public short PublishedYear { get; private set; }
    public short PageAmount { get; private set; }
    public decimal AverageRating { get; private set; }
    public IReadOnlyCollection<User> Users { get; private set; }

    public static Book Create(
        string title,
        string description,
        Edition edition,
        string iSBN,
        int idAuthor,
        string publisher,
        int idGenre,
        short publishedYear,
        short pageAmount,
        decimal averageRating,
        List<User> users)
    {
        return new Book(
            title,
            description,
            edition,
            iSBN,
            idAuthor,
            publisher,
            idGenre,
            publishedYear,
            pageAmount,
            averageRating,
            users);
    }
}