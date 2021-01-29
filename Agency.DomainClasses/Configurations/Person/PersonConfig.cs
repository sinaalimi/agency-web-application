using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Configurations.Person
{
    public class PersonConfig:EntityTypeConfiguration<Entities.Person.Person>
    {
        public PersonConfig()
        {
            ToTable("Person");
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.LastName).IsRequired().HasMaxLength(100);
            Property(p => p.ParentName).IsOptional().HasMaxLength(100);
            Property(p => p.NationalCode).IsRequired().HasMaxLength(10);
            Property(p => p.IdentityCode).IsRequired().HasMaxLength(10);
            Property(p => p.PhoneNumber).IsOptional().HasMaxLength(11);
            Property(p => p.Mobile).IsOptional().HasMaxLength(11);
            Property(p => p.BirthDay).IsRequired();
            Property(p => p.BirthPlace).IsRequired().HasMaxLength(100);
            Property(p => p.Job).IsOptional().HasMaxLength(100);
            Property(p => p.IsGroup).IsRequired();
            Property(p => p.IsParent).IsRequired();
            Property(p => p.AgeRange).IsRequired();
            Property(p => p.ParentId).IsRequired();
            Property(p => p.Relation).IsOptional().HasMaxLength(100);
            Property(p => p.Gender).IsRequired();
            Property(p => p.Address).IsOptional().HasMaxLength(100);
            HasOptional(x => x.Cost).WithRequired(x => x.Person).WillCascadeOnDelete(false);
        }
    }
}
