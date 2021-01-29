using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.User
{
    public class EditUserViewModel : BaseRowVersion
    {
        public Guid Id { get; set; }

        /// <summary>
        /// کلمه عبور
        /// </summary>
        [StringLength(50, ErrorMessage = "کلمه عبور نباید کمتر از ۵ حرف و بیتشر از ۵۰ حرف باشد", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }
        /// <summary>
        /// تکرار کلمه عبور
        /// </summary>
        [DataType(DataType.Password)]
        [DisplayName("تکرار کلمه عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمات عبور باهم مطابقت ندارند")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        [DisplayName("نام کاربری")]
        [StringLength(256, ErrorMessage = "کلمه عبور نباید کمتر از 3 حرف و بیتشر از ۲۵۶ حرف باشد", MinimumLength = 3)]

        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا فقط از حروف انگلیسی و اعدد استفاده کنید")]
        public string UserName { get; set; }


        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [StringLength(255, ErrorMessage = "نام نباید کمتر از 3 حرف و بیتشر از 25۵ حرف باشد", MinimumLength = 3)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی استفاده کنید")]
        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [StringLength(255, ErrorMessage = "نام خانوادگی نباید کمتر از 1 حرف و بیتشر از 25۵ حرف باشد", MinimumLength = 1)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string LastName { get; set; }
        [DisplayName("گروه کاربری")]
        public Guid? RoleId { get; set; }



        [Required(ErrorMessage = "لطفا نام آژانس را وارد کنید")]
        [DisplayName("نام آژانس")]
        [StringLength(100, ErrorMessage = "نام نباید کمتر از 3 حرف و بیشتر از 100 حرف باشد", MinimumLength = 3)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string AgencyName { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
