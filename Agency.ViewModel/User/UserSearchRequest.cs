using System;
using System.ComponentModel;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.User
{
    public class UserSearchRequest : BaseSearchRequest
    {
        public string UserName { get; set; }
        /// <summary>
        /// گروه  کاربری
        /// </summary>
        [DisplayName("گروه کاربری")]
        public Guid? RoleId { get; set; }

        public string AgencyName { get; set; }

    }

    public static class UserSortBy
    {
        public const string UserName = "UserName";
        public const string LastLogingDate = "LastLogingDate";
        public const string AgencyName = "AgencyName";
    }
}
