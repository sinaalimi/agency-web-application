using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Agency.ServiceLayer.Security
{
    public static class AssignableToRolePermissions
    {
        #region Fields

        private static Lazy<IEnumerable<PermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<PermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lazy<IEnumerable<string>> _permissionNamesLazy = new Lazy<IEnumerable<string>>(
            GetPermisionNames, LazyThreadSafetyMode.ExecutionAndPublication);
        #endregion

        #region permissionNames

        public const string CanManageUser = "CanManageUser";
        public const string CanCreateUser = "CanCreateUser";
        public const string CanEditUser = "CanEditUser";
        public const string CanDeleteUser = "CanDeleteUser";
        public const string CanBannAndFreeUser = "CanBannAndFreeUser";

        public const string CanManageHotel = "CanManageHotel";
        public const string CanCreateHotel = "CanCreateHotel";
        public const string CanEditHotel = "CanEditHotel";
        public const string CanDeleteHotel = "CanDeleteHotel";

        public const string CanManageVehicle = "CanManageCustomerGroup";
        public const string CanCreateVehicle = "CanCreateCustomerGroup";
        public const string CanEditVehicle = "CanEditCustomerGroup";
        public const string CanDeleteVehicle = "CanDeleteCustomerGroup";
        public const string CanFormatVehicle = "CanFormatVehicle";

        public const string CanManageTour = "CanManageTour";
        public const string CanCreateTour = "CanCreateTour";
        public const string CanEditTour = "CanEditTour";
        public const string CanCancelTour = "CanCancelTour";
        public const string CanManageSeatsOfTour = "CanManageSeatsOfTour";

        public const string CanManageReserve = "CanManageReserve";
        public const string CanDoReserve = "CanDoReserve";
        public const string CanEditReserve = "CanEditReserve";
        public const string CanDeleteReserve = "CanDeleteReserve";

        public const string CanManageSlider = "CanDeleteReserve";


        public const string CanManageReport = "CanManageReport";

        public const string CanManageRole = "CanManageRole";
        public const string CanCreateRole = "CanCreateRole";
        public const string CanEditRole = "CanEditRole";
        public const string CanDeleteRole = "CanDeleteRole";



        public const string CanAccessToSystemMaintenance = nameof(CanAccessToSystemMaintenance);


        #endregion //permissions

        #region Categories

        public const string CanManageCategory = "CanManageCategory";
        public const string CanCreateCategory = "CanCreateCategory";
        public const string CanEditCategory = "CanEditCategory";
        public const string CanViewCategory = "CanViewCategory";
        public const string CanDeleteCategroy = "CanDeleteCategory";
        #endregion

        #region Permissions

        #region User
        public static readonly PermissionModel CanManageUserPermission = new PermissionModel
        {
            Name = CanManageUser,
            Category = CanManageCategory,
            Description = "میتواند به قسمت کاربران دسترسی داشته باشد"
        };
        public static readonly PermissionModel CanCreateUserPermission = new PermissionModel
        {
            Name = CanCreateUser,
            Category = CanCreateCategory,
            Description = "میتواند کاربر را ایجاد کند"
        };

        public static readonly PermissionModel CanEditUserPermission = new PermissionModel
        {
            Name = CanEditUser,
            Category = CanEditCategory,
            Description = "میتواند کاربر را ویرایش کند"
        };

        public static readonly PermissionModel CanDeleteUserPermission = new PermissionModel
        {
            Name = CanDeleteUser,
            Category = CanDeleteCategroy,
            Description = "میتواند کاربر را حذف کند"
        };
        public static readonly PermissionModel CanBannFreeUserPermission = new PermissionModel
        {
            Name = CanBannAndFreeUser,
            Category = CanEditCategory,
            Description = "میتواند کاربر را مسدود و آزاد کند"
        };
        #endregion

        #region Hotel
        public static readonly PermissionModel CanManageHotelPermission = new PermissionModel
        {
            Name = CanManageHotel,
            Category = CanManageCategory,
            Description = "میتواند به قسمت هتل دسترسی داشته باشد"
        };
        public static readonly PermissionModel CanCreateHotelPermission = new PermissionModel
        {
            Name = CanCreateHotel,
            Category = CanCreateCategory,
            Description = "میتواند هتل را ایجاد کند"
        };
        public static readonly PermissionModel CanEditHotelPermission = new PermissionModel
        {
            Name = CanEditHotel,
            Category = CanEditCategory,
            Description = "میتواند هتل را ویرایش کند"
        };
        public static readonly PermissionModel CanDeleteHotelPermission = new PermissionModel
        {
            Name = CanDeleteHotel,
            Category = CanDeleteCategroy,
            Description = "میتواند هتل را حذف کند"
        };
        #endregion

        #region Vehicle
        public static readonly PermissionModel CanManageVehiclePermission = new PermissionModel
        {
            Name = CanManageVehicle,
            Category = CanManageCategory,
            Description = "میتواند به قسمت وسیله نقلیه دسترسی داشته باشد"
        };
        public static readonly PermissionModel CanCreateVehiclePermission = new PermissionModel
        {
            Name = CanCreateVehicle,
            Category = CanCreateCategory,
            Description = "میتواند وسیله نقلیه را ایجاد کند"
        };
        public static readonly PermissionModel CanEditVehiclePermission = new PermissionModel
        {
            Name = CanEditVehicle,
            Category = CanEditCategory,
            Description = "میتواند وسیله نقلیه را ویرایش کند"
        };
        public static readonly PermissionModel CanDeleteVehiclePermission = new PermissionModel
        {
            Name = CanDeleteVehicle,
            Category = CanDeleteCategroy,
            Description = "میتواند وسیله نقلیه را حذف کند"
        };
        public static readonly PermissionModel CanFormatVehiclePermission = new PermissionModel
        {
            Name = CanFormatVehicle,
            Category = CanDeleteCategroy,
            Description = "میتواند برای وسیله نقلیه چیدمان تعریف کند"
        };
        #endregion

        #region Tour
        public static readonly PermissionModel CanManageTourPermission = new PermissionModel
        {
            Name = CanManageTour,
            Category = CanManageCategory,
            Description = "میتواند به قسمت تور دسترسی داشته باشد"
        };
        public static readonly PermissionModel CanCreateTourPermission = new PermissionModel
        {
            Name = CanCreateTour,
            Category = CanCreateCategory,
            Description = "میتواند تور را ایجاد کند"
        };
        public static readonly PermissionModel CanEditTourPermission = new PermissionModel
        {
            Name = CanEditTour,
            Category = CanEditCategory,
            Description = "میتواند تور را ویرایش کند"
        };
        public static readonly PermissionModel CanCancelTourPermission = new PermissionModel
        {
            Name = CanCancelTour,
            Category = CanDeleteCategroy,
            Description = "میتواند تور را لغو کند"
        };
        public static readonly PermissionModel CanManageSeatsOfTourPermission = new PermissionModel
        {
            Name = CanManageSeatsOfTour,
            Category = CanManageCategory,
            Description = "میتواند صندلی های تور را مدیریت کند"
        };
        #endregion

        #region Reserve
        public static readonly PermissionModel CanManageReservePermission = new PermissionModel
        {
            Name = CanManageReserve,
            Category = CanManageCategory,
            Description = "میتواند به قسمت رزرو دسترسی داشته باشد"
        };
        public static readonly PermissionModel CanDoReservePermission = new PermissionModel
        {
            Name = CanDoReserve,
            Category = CanCreateCategory,
            Description = "میتواند برای تور رزرو کند"
        };
        public static readonly PermissionModel CanEditReservePermission = new PermissionModel
        {
            Name = CanEditReserve,
            Category = CanEditCategory,
            Description = "میتواند رزرو را ویرایش کند"
        };
        public static readonly PermissionModel CanDeleteReservePermission = new PermissionModel
        {
            Name = CanDeleteReserve,
            Category = CanDeleteCategroy ,
            Description = "میتواند رزرو را حذف کند"
        };
        #endregion

        #region Slider
        public static readonly PermissionModel CanManageSliderPermission = new PermissionModel
        {
            Name = CanManageSlider,
            Category = CanManageCategory,
            Description = "میتواند اسلایدر را مدیریت کند"
        };
        #endregion

        #region Role
        public static readonly PermissionModel CanManageRolePermission = new PermissionModel
        {
            Name = CanManageRole,
            Category = CanManageCategory,
            Description = "میتواند به قسمت نقش ها دسترسی داشته باشد"
        };
        public static readonly PermissionModel CanCreateRolePermission = new PermissionModel
        {
            Name = CanCreateRole,
            Category = CanCreateCategory,
            Description = "میتواند نقش را ایجاد کند"
        };
        public static readonly PermissionModel CanEditRolePermission = new PermissionModel
        {
            Name = CanEditRole,
            Category = CanEditCategory,
            Description = "میتواند نقش را ویرایش کند"
        };
        public static readonly PermissionModel CanDeleteRolePermission = new PermissionModel
        {
            Name = CanDeleteRole,
            Category = CanEditCategory,
            Description = "میتواند نقش را حذف کند"
        };
        #endregion
        public static readonly PermissionModel CanAccessToSystemMaintenancePermission = new PermissionModel
        {
            Name = CanAccessToSystemMaintenance,
            Category = CanManageCategory,
            Description = "می تواند به بخش نگهداری سیستم دسترسی داشته باشد"
        };

        public static readonly PermissionModel CanManageReportPermission = new PermissionModel
        {
            Name = CanManageReport,
            Category = CanManageCategory,
            Description = "می تواند به قسمت گزارش گیری دسترسی داشته باشد"
        };

        #endregion permisions

        #region Properties
        public static IEnumerable<PermissionModel> Permissions => _permissionsLazy.Value;

        public static IEnumerable<string> PermissionNames => _permissionNamesLazy.Value;

        #endregion

        #region GetAllPermisions
        private static IEnumerable<PermissionModel> GetPermision()
        {
            return new List<PermissionModel>
            {
                CanManageUserPermission,
                CanCreateUserPermission,
                CanEditUserPermission,
                CanDeleteUserPermission,
                CanBannFreeUserPermission,

                CanManageHotelPermission,
                CanCreateHotelPermission,
                CanEditHotelPermission,
                CanDeleteHotelPermission,

                CanManageVehiclePermission,
                CanCreateVehiclePermission,
                CanEditVehiclePermission,
                CanDeleteVehiclePermission,
                CanFormatVehiclePermission,

                CanManageTourPermission,
                CanCreateTourPermission,
                CanEditTourPermission,
                CanCancelTourPermission,
                CanManageSeatsOfTourPermission,

                CanManageReservePermission,
                CanDoReservePermission,
                CanEditReservePermission,
                CanDeleteReservePermission,

                CanManageSliderPermission,

                CanManageReportPermission,

                CanManageRolePermission,
                CanCreateRolePermission,
                CanEditRolePermission,
                CanDeleteRolePermission,

                CanAccessToSystemMaintenancePermission
            };
        }
        
        private static IEnumerable<string> GetPermisionNames()
        {
            return new List<string>()
            {
                CanManageUser,
                CanCreateUser,
                CanEditUser,
                CanDeleteUser,
                CanBannAndFreeUser,

                CanManageHotel,
                CanCreateHotel,
                CanEditHotel,
                CanDeleteHotel,

                CanManageVehicle,
                CanCreateVehicle,
                CanEditVehicle,
                CanDeleteVehicle,
                CanFormatVehicle,

                CanManageTour,
                CanCreateTour,
                CanEditTour,
                CanCancelTour,
                CanManageSeatsOfTour,

                CanManageReserve,
                CanDoReserve,
                CanEditReserve,
                CanDeleteReserve,

                CanManageSlider,

                CanAccessToSystemMaintenance

            };
        }
        #endregion

        #region GetAsSelectedList
        public static IEnumerable<SelectListItem> GetAsSelectListItems()
        {
            return Permissions.Select(a => new SelectListItem { Text = a.Description, Value = a.Name }).ToList();
        }
        #endregion

        public static IEnumerable<string> GetBaseSettingPermissions()
        {
            return new List<string>
            {

            };
        }
    }
}
