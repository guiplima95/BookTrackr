using Book.API.Domain.GenreAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.API.Infrastructure.Persistence.Configurations;

public class GenreEntityTypeConfigurations : EntityConfigurations<Genre>
{
    public override void Configure(EntityTypeBuilder<Genre> builder)
    {
        base.Configure(builder);
    }
}