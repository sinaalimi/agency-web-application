using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.State
{
    public class StateConfig : EntityTypeConfiguration<Entities.State.State>
    {
        public StateConfig()
        {
            ToTable("State");
            Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
