using Book.API.Domain.AuthorAggregate;
using Book.API.Domain.GenreAggregate;
using Book.API.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class BookEntityTypeConfigurations : IEntityTypeConfiguration<Domain.BookAggregate.Book>
{
    public void Configure(EntityTypeBuilder<Domain.BookAggregate.Book> builder)
    {
        builder
           .HasKey(t => t.Id)
           .IsClustered(false)
           .HasName("PK_Book");

        builder
           .Property(t => t.Id)
           .ValueGeneratedNever()
           .HasDefaultValueSql("newsequentialid()")
           .IsRequired();

        builder
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .Property(b => b.Description)
            .HasMaxLength(500);

        builder
            .OwnsOne(u => u.Edition, edition =>
            {
                edition.Property(a => a.Description)
                    .HasMaxLength(100)
                    .HasConversion(description => description, description => description)
                    .IsRequired();

                edition.Property(a => a.Number)
                    .HasConversion(number => number, number => number)
                    .IsRequired();
            });

        builder
            .Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(13);

        builder
            .Property(b => b.Publisher)
            .HasMaxLength(200);

        builder
            .Property(b => b.PublishedYear)
            .IsRequired();

        builder
            .Property(b => b.PageAmount)
            .IsRequired();

        builder
            .Property(b => b.AverageRating)
            .HasPrecision(3, 2);

        builder
            .HasOne<Author>()
            .WithMany(s => s.Books)
            .HasForeignKey(t => t.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Book_Author");

        builder
            .HasOne<Genre>()
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Book_Genre");

        builder
           .HasOne<User>()
           .WithMany(g => g.Books)
           .HasForeignKey(b => b.UserId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict)
           .HasConstraintName("FK_Book_User");
    }
}