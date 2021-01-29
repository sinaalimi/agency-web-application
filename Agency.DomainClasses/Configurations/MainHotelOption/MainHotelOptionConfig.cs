using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.MainHotelOption
{
    public class MainHotelOptionConfig : EntityTypeConfiguration<Entities.MainHotelOption.MainHotelOption>
    {
        public MainHotelOptionConfig()
        {
            ToTable("MainHotelOption");
            Property(p => p.Name).IsRequired().HasMaxLength(256);
            Property(p => p.Price).IsRequired();
            Property(p => p.IsFree).IsOptional();
           // HasRequired(p => p.User).WithMany(p => p.MainHotelOptions).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
            HasRequired(p => p.MainHotel).WithMany(p => p.MainHotelOptions).HasForeignKey(p => p.HotelId).WillCascadeOnDelete(false);


        }
    }
}
