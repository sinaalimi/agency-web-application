using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.TourHotel
{
    public class TourHotelConfig:EntityTypeConfiguration<Entities.TourHotel.TourHotel>
    {
        public TourHotelConfig()
        {
            ToTable("TourHotel");
            Property(p => p.TourId).IsRequired();
            Property(p => p.HotelId).IsRequired();

            HasRequired(p=>p.Hotel).WithMany(p=>p.TourHotels).HasForeignKey(p=>p.HotelId).WillCascadeOnDelete(false);
            HasRequired(p=>p.Tour).WithMany(p=>p.TourHotels).HasForeignKey(p=>p.TourId).WillCascadeOnDelete(false);
        }
    }
}
