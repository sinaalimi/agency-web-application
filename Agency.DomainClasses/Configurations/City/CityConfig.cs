using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.City
{
    public class CityConfig:EntityTypeConfiguration<Entities.City.City>
    {
        public CityConfig()
        {
            ToTable("City");
            Property(p => p.Name).IsRequired().HasMaxLength(60);
            HasRequired(p => p.State).WithMany(p => p.Cities).HasForeignKey(p => p.StateId).WillCascadeOnDelete();
        }
    }
}
