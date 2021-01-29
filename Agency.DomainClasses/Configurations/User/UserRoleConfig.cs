using System.Data.Entity.ModelConfiguration;
using Agency.DomainClasses.Entities.User;

namespace Agency.DomainClasses.Configurations.User
{
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public UserRoleConfig()
        {
            HasKey(r => new { r.UserId, r.RoleId });
            ToTable(nameof(UserRole));
        }
    }
}
