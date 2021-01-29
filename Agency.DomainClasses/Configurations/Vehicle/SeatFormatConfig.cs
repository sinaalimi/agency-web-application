using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Vehicle;

namespace Agency.DomainClasses.Configurations.Vehicle
{
   public  class SeatFormatConfig :EntityTypeConfiguration<SeatFormat>
    {
       public SeatFormatConfig()
       {
           
         ToTable("SeatFormat");
           Property(p => p.Col).IsRequired();
           Property(p => p.Row).IsRequired();
           Property(p => p.SeatNumber).IsRequired();
           HasRequired(p=>p.User).WithMany(p=>p.SeatsFormat).HasForeignKey(p=>p.UserId).WillCascadeOnDelete(false);
           HasRequired(p=>p.Vehicle).WithMany(p=>p.SeatSFormat).HasForeignKey(p=>p.VehicleId).WillCascadeOnDelete(false);
       }
    }
}
