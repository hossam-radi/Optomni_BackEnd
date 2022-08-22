using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Optmni.DAL.Model;

namespace Optmni.DAL.ModelConfiguration
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.Order)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(m => m.OrderId);

        }
    }
}
