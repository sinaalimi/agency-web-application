using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.PersonRoomHotel
{
    public class PersonRoomHotelConfig : EntityTypeConfiguration<Entities.PersonRoomHotel.PersonRoomHotel>
    {
        public PersonRoomHotelConfig()
        {
            ToTable("PersonRoomHotel");
            Property(p => p.NewPersonId).IsRequired();
            Property(p => p.RoomHotelId).IsRequired();

            HasRequired(p => p.NewPerson).WithMany(p => p.PersonRoomHotels).HasForeignKey(p => p.NewPersonId).WillCascadeOnDelete(false);
            HasRequired(p => p.RoomMainHotel).WithMany(p => p.PersonRoomHotels).HasForeignKey(p => p.RoomHotelId).WillCascadeOnDelete(false);
        }
    }
}
