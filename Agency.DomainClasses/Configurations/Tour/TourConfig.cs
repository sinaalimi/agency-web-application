using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.Tour
{
    public class TourConfig:EntityTypeConfiguration<Entities.tour.Tour>    
    {
        public TourConfig()
        {
            ToTable("Tour");
            Property(p => p.Title).IsRequired().HasMaxLength(100);
            Property(p => p.StartTime).IsRequired();
            Property(p => p.EndTime).IsRequired();
            Property(p => p.Capacity).IsRequired();
            Property(p => p.GoRoad).IsOptional().HasMaxLength(250);
            Property(p => p.ComeRoad).IsOptional().HasMaxLength(250);
            Property(p => p.AdultPrice).IsRequired();
            Property(p => p.ChildPrice).IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.StartHour).IsRequired().HasMaxLength(5);
            Property(p => p.AgencyShare).IsRequired();
            Property(p => p.SupervisorName).IsOptional().HasMaxLength(100);
            Property(p => p.IsurancePrice).IsRequired();
            Property(p => p.FinishRegister).IsRequired();
            Property(p => p.IsCanceled).IsRequired();
            Property(p => p.OfficeCost).IsRequired();
            Property(p => p.CoupleBedPrice).IsRequired();

            HasRequired(p => p.User).WithMany(p => p.Tour).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
            HasRequired(p=>p.City).WithMany(p=>p.Tours).HasForeignKey(p=>p.SourceCityId).WillCascadeOnDelete(false);
            HasRequired(p => p.City).WithMany(p => p.Tours).HasForeignKey(p => p.DestinationCityId).WillCascadeOnDelete(false);
            HasRequired(p => p.State).WithMany(p => p.Tours).HasForeignKey(p => p.SourceStateId).WillCascadeOnDelete(false);
            HasRequired(p => p.State).WithMany(p => p.Tours).HasForeignKey(p => p.DestinationStateId).WillCascadeOnDelete(false);




        }
    }
}
