using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.RoomHotel
{
    public class RoomMainHotelconfig: EntityTypeConfiguration<Entities.RoomMainHotel.RoomMainHotel>
    {
        public RoomMainHotelconfig()
        {
            ToTable("RoomMAinHotel");
            Property(p => p.Price).IsRequired();
            Property(p => p.Count).IsRequired();
            Property(p => p.RemainingCount).IsRequired();
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.FirstDate).IsRequired();
            Property(p => p.LasTime).IsRequired();
            //HasRequired(p => p.User).WithMany(p => p.RoomMainHotels).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
            HasRequired(p => p.MainHotel).WithMany(p => p.RoomMainHotels).HasForeignKey(p => p.HotelId).WillCascadeOnDelete(false);
            HasRequired(p => p.Room).WithMany(p => p.RoomMainHotels).HasForeignKey(p => p.RoomId).WillCascadeOnDelete(false);
        }
    }
}
