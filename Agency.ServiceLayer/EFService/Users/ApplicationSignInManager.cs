using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Agency.DomainClasses.Entities.User;
using Agency.ServiceLayer.Contracts.Users;

namespace Agency.ServiceLayer.EFService.Users
{
    public class ApplicationSignInManager : SignInManager<User, Guid>, IApplicationSignInManager
    {

        #region Fields
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        #endregion

        #region Constructor

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }
        #endregion


    }
}
