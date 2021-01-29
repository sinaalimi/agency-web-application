using System;
using Agency.DataLayer.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using Agency.DomainClasses.Entities.User;
using Agency.ServiceLayer.Contracts.Users;

namespace Agency.ServiceLayer.EFService.Users
{
    public class CustomUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        public CustomUserStore(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
