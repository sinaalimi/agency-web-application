using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.TourOption
{
    public class TourOptionConfig : EntityTypeConfiguration<Entities.TourOption.TourOption>
    {
        public TourOptionConfig()
        {
            ToTable("TourOption");

            //Property(p => p.OptionId).IsRequired();
            Property(p => p.TourId).IsRequired();
            Property(p => p.Title).IsRequired().HasMaxLength(200);
            //Property(p => p.Cost).IsRequired();

            //HasRequired(p=>p.Option).WithMany(p=>p.TourOptions).HasForeignKey(p=>p.OptionId).WillCascadeOnDelete(false);
            HasRequired(p=>p.Tour).WithMany(p=>p.TourOptions).HasForeignKey(p=>p.TourId).WillCascadeOnDelete(false);
        }
    }
}
