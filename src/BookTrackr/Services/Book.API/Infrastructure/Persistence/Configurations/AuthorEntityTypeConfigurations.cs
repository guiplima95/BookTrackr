using Book.API.Domain.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class AuthorEntityTypeConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .HasKey(t => t.Id)
            .IsClustered(false)
            .HasName("PK_Author");

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasDefaultValueSql("newsequentialid()")
            .IsRequired();

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
            .WithOne()
            .HasForeignKey(b => b.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(n => n.Name)
            .HasMaxLength(150)
            .HasConversion(name => name.FullName, fullName => new Name(fullName));

        IMutableNavigation? navigation = builder.Metadata.FindNavigation(nameof(Author.Books));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}