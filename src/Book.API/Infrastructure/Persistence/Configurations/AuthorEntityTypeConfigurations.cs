using Book.API.Domain.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class AuthorEntityTypeConfigurations : EntityConfigurations<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        builder
            .Property(a => a.Born)
            .HasMaxLength(250);

        builder
            .Property(a => a.Died)
            .HasMaxLength(250);

        builder
            .Property(a => a.Description)
            .HasMaxLength(250);

        builder
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.IdAuthor)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(n => n.Name)
            .HasMaxLength(150)
            .HasConversion(name => name.FullName, fullName => new Name(fullName));
    }
}