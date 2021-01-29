using System;
using Microsoft.AspNet.Identity;
using Agency.DomainClasses.Entities.User;
using Agency.ServiceLayer.Contracts.Users;

namespace Agency.ServiceLayer.EFService.Users
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<Role, Guid> _roleStore;

        public CustomRoleStore(IRoleStore<Role, Guid> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}
