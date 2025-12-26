using FT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FT.Infrastructure.Configuration
{
    public class TourConfiguration :IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(t => t.Description).HasMaxLength(1000);
            builder.Property(t => t.Duration).IsRequired();
            builder.Property(t => t.Created).HasDefaultValueSql("GETDATE()").IsRequired();
        }
    }
}
