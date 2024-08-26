namespace Book.API.Domain.UserAggregate;

public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country);