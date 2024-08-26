using Book.API.Domain.SeedWork;

namespace Book.API.Domain.AuthorAggregate;

public class Author : Entity
{
    private Author(
        string? born,
        string? died,
        string description,
        Name name)
    {
        Born = born;
        Died = died;
        Description = description;
        Name = name;
    }

    public string? Born { get; private set; }
    public string? Died { get; private set; }
    public string Description { get; private set; }
    public Name Name { get; private set; }
    public IReadOnlyCollection<BookAggregate.Book>? Books { get; private set; }

    public static Author Create(
        string? born,
        string? died,
        string description,
        Name name)
    {
        return new Author(
            born,
            died,
            description,
            name);
    }
}