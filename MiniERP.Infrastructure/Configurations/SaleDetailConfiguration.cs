using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniERP.Domain.Entities;

namespace MiniERP.Infrastructure.Configurations
{
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.ToTable("sale_details");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SaleId)
                .IsRequired();
            builder.Property(x => x.ProductId)
                .IsRequired();
            builder.Property(x => x.Quantity)
                .IsRequired();
            builder.Property(x => x.UnitPrice)
                .IsRequired();
        }
    }
}
