using System.Collections.Generic;
using Agency.DomainClasses.Entities.User;
using Agency.ViewModel;
using Agency.ViewModel.Home;

namespace Agency.ServiceLayer.Contracts.Users
{
    public interface IActivityLogService
    {
        void Create(ActivityLog log);
        IList<LastActivityViewModel> GetLastActivities();
    }
}
