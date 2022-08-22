using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Optmni.DAL.Model;
using System;

namespace Optmni.DAL.ModelConfiguration
{
    public class Lkp_RegionConfiguration : IEntityTypeConfiguration<Lkp_Region>
    {
        public void Configure(EntityTypeBuilder<Lkp_Region> builder)
        {
            builder.ToTable("Lkp_Region");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.Name).HasMaxLength(100);

             builder.HasData
                (
                  new Lkp_Region { Id = 1, Name = "Region1", CreatedAt = new DateTime(2022, 8, 06, 0, 0, 0, 0, DateTimeKind.Local) },
                  new Lkp_Region { Id = 2, Name = "Region2", CreatedAt = new DateTime(2022, 8, 06, 0, 0, 0, 0, DateTimeKind.Local) },
                  new Lkp_Region { Id = 3, Name = "Region3",  CreatedAt = new DateTime(2022, 8, 06, 0, 0, 0, 0, DateTimeKind.Local) }
                  
                );
        }
    }
}
