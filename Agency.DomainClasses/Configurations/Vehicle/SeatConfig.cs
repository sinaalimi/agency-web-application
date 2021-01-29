using System;
using System.Collections.Generic;using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Agency.DomainClasses.Entities.common;
using Agency.DomainClasses.Entities.Vehicle;

namespace Agency.DomainClasses.Configurations.Vehicle
{
   public class SeatConfig : EntityTypeConfiguration<Seat>
    {
       public SeatConfig()
       {
            ToTable("Seat");
            Property(p => p.SeatGender).IsRequired();
            Property(p => p.Isempty).IsRequired();
            Property(p => p.IsDeactive).IsRequired();
            Property(p => p.TourVehicleId).IsRequired();
            Property(p => p.ReserveId).IsRequired();
            HasRequired(p => p.User).WithMany(p => p.Seats).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
            HasRequired(p => p.TourVehicle).WithMany(p => p.Seats).HasForeignKey(p => p.TourVehicleId).WillCascadeOnDelete(false);
            //HasRequired(p=>p.Tour).WithMany(p=>p.Seats).HasForeignKey(p=>p.TourId).WillCascadeOnDelete(false);
            HasRequired(p => p.SeatFormat).WithMany(p => p.Seats).HasForeignKey(p => p.SeatId).WillCascadeOnDelete(false);
            HasOptional(p=>p.Person).WithMany(p=>p.Seats).HasForeignKey(p=>p.PersonId).WillCascadeOnDelete(true);

        }
    }
}
