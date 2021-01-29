using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.MainHotel
{
    public class MainHotelConfig:EntityTypeConfiguration<Entities.MainHotel.MainHotel>
    {
        public MainHotelConfig()
        {
            ToTable("MainHotel");
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.level).IsRequired();
            Property(p => p.Adress).IsRequired().HasMaxLength(500);
            Property(p => p.Image).IsOptional();
            Property(p => p.Tel).IsRequired();
            HasRequired(p => p.State).WithMany(p => p.MainHotels).HasForeignKey(p => p.StateId).WillCascadeOnDelete(false);
            HasRequired(p => p.City).WithMany(p => p.MainHotels).HasForeignKey(p => p.CityId).WillCascadeOnDelete(false);
            HasRequired(p => p.User).WithMany(p => p.MainHotels).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
        }
    }
}
