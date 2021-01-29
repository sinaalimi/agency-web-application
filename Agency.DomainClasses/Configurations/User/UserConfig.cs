using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Agency.DomainClasses.Configurations.User
{
    public class UserConfig : EntityTypeConfiguration<Agency.DomainClasses.Entities.User.User>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public UserConfig()
        {
            ToTable(nameof(Agency.DomainClasses.Configurations.User));
            HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.LastName).IsRequired().HasMaxLength(100);
            Property(u => u.RowVersion).IsRowVersion();
            Property(u => u.PhoneNumber).IsOptional().HasMaxLength(11);
            Property(u => u.AgencyName).IsRequired().HasMaxLength(100);
            Property(u => u.UserName)
                 .IsRequired()
                 .HasMaxLength(50)
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserName") { IsUnique = true }));

            Property(u => u.Email)
                .IsOptional()
                .HasMaxLength(100);
        }
    }
}
