using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class BookEntityTypeConfigurations : EntityConfigurations<Domain.BookAggregate.Book>
{
    public override void Configure(EntityTypeBuilder<Domain.BookAggregate.Book> builder)
    {
        base.Configure(builder);
    }
}