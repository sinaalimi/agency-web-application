using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.Reserve
{
    public class ReserveConfig : EntityTypeConfiguration<Entities.Reserve.Reserve>
    {
        public ReserveConfig()
        {
            ToTable("Reserve");
            Property(p => p.ParentId).IsRequired();
            Property(p => p.TourId).IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.ReserveTime).IsRequired();
            Property(p => p.CoupleBed).IsOptional();
            Property(p => p.IsTemporary).IsOptional();
            Property(p => p.TourVehicleId).IsOptional();
            Property(p => p.IsCanseled).IsRequired();
            Property(p => p.CodeRahgiri).IsRequired();
            Property(p => p.TotalCost).IsRequired();
            Property(p => p.Penalty).IsOptional();
            Property(p => p.CancelDate).IsOptional();
            Property(p => p.CancelFilePath).IsOptional();
            Property(p => p.ContractFilePath).IsOptional();

            HasRequired(p => p.Tour).WithMany(p => p.Reserves).HasForeignKey(p => p.TourId).WillCascadeOnDelete(false);
            HasRequired(p=>p.User).WithMany(p=>p.Reserves).HasForeignKey(p=>p.UserId).WillCascadeOnDelete(false);
            HasRequired(p => p.Hotel).WithMany(p => p.Reserves).HasForeignKey(p => p.HotelId).WillCascadeOnDelete(false);
            HasRequired(p => p.TourVehicle).WithMany(p => p.Reserve).HasForeignKey(p => p.TourVehicleId).WillCascadeOnDelete(false);
            HasRequired(p=>p.Person).WithMany(p=>p.Reserves).HasForeignKey(p=>p.ParentId).WillCascadeOnDelete(false);

           // HasOptional(p=>p.Person).WithMany(p=>p.Reserves).HasForeignKey(p=>p.ParentId).WillCascadeOnDelete(false);
        }
    }
}
