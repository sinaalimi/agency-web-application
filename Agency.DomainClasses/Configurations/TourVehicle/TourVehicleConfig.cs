using System.Data.Entity.ModelConfiguration;

namespace Agency.DomainClasses.Configurations.TourVehicle
{
    public class TourVehicleConfig: EntityTypeConfiguration<Entities.TourVehicle.TourVehicle>
    {
        public TourVehicleConfig()
        {
            ToTable("TourVehicle");
            Property(p => p.TourId).IsOptional();
            Property(p => p.VehicleId).IsOptional();
            Property(p => p.Index).IsRequired();

            HasRequired(p=>p.Tour).WithMany(p=>p.TourVehicles).HasForeignKey(p=>p.TourId).WillCascadeOnDelete(false);
            HasRequired(p=>p.Vehicle).WithMany(p=>p.TourVehicles).HasForeignKey(p=>p.VehicleId).WillCascadeOnDelete(false);

        }
    }
}
