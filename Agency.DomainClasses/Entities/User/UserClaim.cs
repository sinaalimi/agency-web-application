using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Agency.DomainClasses.Entities.User
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
    }
}
