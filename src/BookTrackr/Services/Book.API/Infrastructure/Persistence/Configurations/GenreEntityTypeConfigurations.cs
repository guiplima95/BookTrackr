using Book.API.Domain.Entities.GenreAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class GenreEntityTypeConfigurations : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .HasKey(t => t.Id)
            .IsClustered(false)
            .HasName("PK_Genre");

        builder
           .Property(t => t.Id)
           .ValueGeneratedNever()
           .HasDefaultValueSql("newsequentialid()")
           .IsRequired();

        builder
            .Property(g => g.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .HasMany(g => g.Books)
            .WithOne()
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        IMutableNavigation? navigation = builder.Metadata.FindNavigation(nameof(Genre.Books));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}