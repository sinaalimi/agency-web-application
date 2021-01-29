using System.Data.Entity.ModelConfiguration;
using Agency.DomainClasses.Entities.User;

namespace Agency.DomainClasses.Configurations.User
{
    public class UserClaimConfig : EntityTypeConfiguration<UserClaim>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public UserClaimConfig()
        {
            ToTable("UserClaims");
        }
    }
}
