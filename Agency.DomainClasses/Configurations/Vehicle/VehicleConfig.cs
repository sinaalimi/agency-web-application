using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.Vehicle
{
   public class VehicleConfig : EntityTypeConfiguration<Entities.Vehicle.Vehicle>
    {
        public VehicleConfig()
        {
            ToTable("Vehicle");
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.Capacity).IsRequired();
        }
    }
}
