using System.Data.Entity.ModelConfiguration;
using Agency.DomainClasses.Entities.User;

namespace Agency.DomainClasses.Configurations.User
{
    public class UserLoginConfig : EntityTypeConfiguration<UserLogin>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserLoginConfig()
        {
            HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            ToTable("UserLogins");

        }
    }
}
