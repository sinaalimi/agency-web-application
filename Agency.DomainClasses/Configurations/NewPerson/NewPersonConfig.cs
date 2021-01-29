using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.NewPerson
{
    public class NewPersonConfig : EntityTypeConfiguration<Entities.NewPerson.NewPerson>
    {
        public NewPersonConfig()
        {
            ToTable("NewPerson");

            Property(p => p.FName).IsRequired().HasMaxLength(100);
            Property(p => p.LName).IsRequired().HasMaxLength(100);
            Property(p => p.Mobile).IsRequired().HasMaxLength(11);
            Property(p => p.Email).IsRequired();
            HasRequired(p => p.User).WithMany(p => p.NewPersons).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
        }
    }
}
