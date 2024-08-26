using Book.API.Domain.AuthorAggregate;
using Book.API.Domain.SeedWork;

namespace Book.API.Domain.UserAggregate;

public class User : Entity
{
    private User(
        Address address,
        CPF cpf,
        Email email,
        Name name,
        Password password)
    {
        Address = address;
        CPF = cpf;
        Email = email;
        Name = name;
        Password = password;
    }

    public Address Address { get; private set; }
    public CPF CPF { get; private set; }
    public Email Email { get; private set; }
    public Name Name { get; private set; }
    public Password Password { get; private set; }

    public static User Create(
        Address address,
        CPF cpf,
        Email email,
        Name name,
        Password password)
    {
        return new User(address, cpf, email, name, password);
    }
}