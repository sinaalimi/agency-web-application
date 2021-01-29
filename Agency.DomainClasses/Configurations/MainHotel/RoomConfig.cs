using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.MainHotel
{
    public class RoomConfig : EntityTypeConfiguration<Entities.MainHotel.Room>
    {
        public RoomConfig()
        {
            ToTable("MainRoom");
            Property(p => p.Type).IsRequired().HasMaxLength(100);
            Property(p => p.Capacity).IsRequired();
            HasRequired(p => p.User).WithMany(p => p.Rooms).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
        }
    }
}
