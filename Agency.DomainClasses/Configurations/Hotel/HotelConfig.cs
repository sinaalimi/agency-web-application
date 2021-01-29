using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.Hotel
{
   public class HotelConfig:EntityTypeConfiguration<Entities.Hotel.Hotel>
    {

       public HotelConfig()
       {
           ToTable("Hotel");
           Property(p => p.Name).IsRequired().HasMaxLength(100);
           Property(p => p.Degree).IsRequired();
           Property(p => p.Address).IsRequired().HasMaxLength(250);
           Property(p => p.PhoneNumber).IsRequired().HasMaxLength(100);
           Property(p => p.ManagerName).IsRequired().HasMaxLength(100);
           Property(p => p.ImageSource).IsOptional().HasMaxLength(250);
           Property(p => p.Description).IsOptional().HasMaxLength(2000);
           HasRequired(p=>p.City).WithMany(p=>p.Hotels).HasForeignKey(p=>p.CityId).WillCascadeOnDelete(false);
           HasRequired(p => p.State).WithMany(p => p.Hotels).HasForeignKey(p => p.StateId).WillCascadeOnDelete(false);
        }

    }
}
