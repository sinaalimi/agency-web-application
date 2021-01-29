using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.Cost
{
   public class CostConfig:EntityTypeConfiguration<Entities.Cost.Cost>
    {
       public CostConfig()
       {
           ToTable("Cost");
           Property(p => p.ReserveId).IsRequired();
           Property(p => p.PersonCost).IsRequired();
           Property(p => p.PersonPenaltyCost).IsOptional();
           Property(p => p.IsCanceled).IsOptional();
           Property(p => p.CancelDate).IsOptional();
           Property(p => p.CancelFilePath).IsOptional();
           HasRequired(p => p.Reserve).WithMany(p => p.Costs).HasForeignKey(p => p.ReserveId).WillCascadeOnDelete(false);
       

       }
         
    }
}
