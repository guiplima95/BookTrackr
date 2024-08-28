using Book.API.Domain.AuthorAggregate;
using Book.API.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class UserEntityTypeConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(t => t.Id)
            .IsClustered(false)
            .HasName("PK_User");

        builder
           .Property(t => t.Id)
           .ValueGeneratedNever()
           .HasDefaultValueSql("newsequentialid()")
           .IsRequired();

        builder
            .OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasMaxLength(200)
                    .HasConversion(street => street, street => street)
                    .IsRequired();

                address.Property(a => a.City)
                    .HasMaxLength(100)
                    .HasConversion(city => city, city => city)
                    .IsRequired();

                address.Property(a => a.State)
                    .HasMaxLength(100)
                    .HasConversion(state => state, state => state)
                    .IsRequired();

                address.Property(a => a.PostalCode)
                    .HasMaxLength(20)
                    .HasConversion(postalCode => postalCode, postalCode => postalCode)
                    .IsRequired();

                address.Property(a => a.Country)
                    .HasMaxLength(100)
                    .HasConversion(country => country, country => country)
                    .IsRequired();
            });

        builder
            .Property(u => u.CPF)
            .HasMaxLength(14)
            .HasConversion(cpf => cpf.Number, number => new CPF(number))
            .IsRequired();

        builder
            .Property(u => u.Email)
            .HasMaxLength(150)
            .HasConversion(email => email.Address, address => new Email(address))
            .IsRequired();

        builder
             .Property(u => u.Password)
             .HasMaxLength(100)
             .HasConversion(password => password.Value, value => new Password(value))
             .IsRequired();

        builder
            .Property(u => u.Name)
            .HasMaxLength(150)
            .HasConversion(name => name.FullName, fullName => new Name(fullName))
            .IsRequired();

        IMutableNavigation? navigation = builder.Metadata.FindNavigation(nameof(User.Books));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}