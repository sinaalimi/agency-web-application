using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ServiceLayer.Security
{
    public static class StandardRoles
    {
        #region Fields

        private static Lazy<IEnumerable<PermissionRecord>> _rolesWithPermissionsLazy =
            new Lazy<IEnumerable<PermissionRecord>>();
        private static Lazy<IEnumerable<string>> _rolesLazy = new Lazy<IEnumerable<string>>();
        #endregion

        #region Properties
        public static IEnumerable<string> SystemRoles
        {
            get
            {
                if (_rolesLazy.IsValueCreated)
                    return _rolesLazy.Value;
                _rolesLazy = new Lazy<IEnumerable<string>>(GetSysmteRoles);
                return _rolesLazy.Value;
            }
        }

        public static IEnumerable<PermissionRecord> SystemRolesWithPermissions
        {
            get
            {
                if (_rolesWithPermissionsLazy.IsValueCreated)
                    return _rolesWithPermissionsLazy.Value;
                _rolesWithPermissionsLazy = new Lazy<IEnumerable<PermissionRecord>>(GetDefaultRolesWithPermissions);
                return _rolesWithPermissionsLazy.Value;
            }
        }
        #endregion

        #region DefaultRoles
        public const string Administrators = "مدیر";
        public const string Operators = "اپراتور";
        #endregion

        #region GetSysmteRoles
        private static IEnumerable<string> GetSysmteRoles()
        {
            return new List<string>
            {
              Administrators,
              Operators
            };
        }
        #endregion

        #region GetDefaultRolesWithPermissions
        private static IEnumerable<PermissionRecord> GetDefaultRolesWithPermissions()
        {
            return new List<PermissionRecord>
            {
                new PermissionRecord
                {
                    RoleName = Administrators,
                    Permissions = AssignableToRolePermissions.Permissions
                },

                new PermissionRecord
                {
                    RoleName = Operators,
                    Permissions = new List<PermissionModel>
                    {
                        //AssignableToRolePermissions.CanManageUserPermission,
                        //AssignableToRolePermissions.CanCreateUserPermission,
                        //AssignableToRolePermissions.CanEditUserPermission,
                        //AssignableToRolePermissions.CanDeleteUserPermission,
                        //AssignableToRolePermissions.CanBannFreeUserPermission,

                        AssignableToRolePermissions.CanAccessToSystemMaintenancePermission,

                        AssignableToRolePermissions.CanManageHotelPermission,
                        AssignableToRolePermissions.CanCreateHotelPermission,
                        AssignableToRolePermissions.CanEditHotelPermission,
                        AssignableToRolePermissions.CanDeleteHotelPermission,

                        AssignableToRolePermissions.CanCreateVehiclePermission,
                        AssignableToRolePermissions.CanDeleteVehiclePermission,
                        AssignableToRolePermissions.CanEditVehiclePermission,
                        AssignableToRolePermissions.CanFormatVehiclePermission,
                        AssignableToRolePermissions.CanManageVehiclePermission,

                        AssignableToRolePermissions.CanCreateTourPermission,
                        AssignableToRolePermissions.CanEditTourPermission,
                        AssignableToRolePermissions.CanManageSeatsOfTourPermission,
                        AssignableToRolePermissions.CanManageTourPermission,

                        AssignableToRolePermissions.CanDoReservePermission,
                        AssignableToRolePermissions.CanEditReservePermission,
                        AssignableToRolePermissions.CanManageReservePermission,
                    }
                }
            };
        }
        #endregion

    }
}
