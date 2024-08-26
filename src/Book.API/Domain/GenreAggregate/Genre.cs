using Book.API.Domain.SeedWork;

namespace Book.API.Domain.GenreAggregate;

public class Genre(string description) : Entity
{
    public string Description { get; private set; } = description;
    public IReadOnlyCollection<BookAggregate.Book> Books { get; private set; } = [];

    public static Genre Create(string description)
    {
        return new Genre(description);
    }

    public void Update(string description)
    {
        Description = description;
    }
}