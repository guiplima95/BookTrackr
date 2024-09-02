using Book.API.Domain.Abstractions;

namespace Book.API.Domain.Entities.AuthorAggregate;

public class Author(
    string? born,
    string? died,
    string description,
    Name name) : Entity
{
    private readonly List<BookAggregate.Book> _books = new();

    public string? Born { get; private set; } = born;
    public string? Died { get; private set; } = died;
    public string Description { get; private set; } = description;
    public Name Name { get; private set; } = name;
    public IReadOnlyCollection<BookAggregate.Book> Books => _books;

    public Author(
        string? born,
        string? died,
        string description,
        Name name,
        List<BookAggregate.Book> books) : this(born, died, description, name)
    {
        _books = books ?? [];
    }
}