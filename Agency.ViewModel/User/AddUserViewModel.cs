using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agency.ViewModel.User
{
    public class AddUserViewModel
    {

        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [DisplayName("نام")]
        [StringLength(50, ErrorMessage = "نام نباید کمتر از 3 حرف و بیشتر از 50 حرف باشد", MinimumLength = 3)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string Name { get; set; }




        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [DisplayName("نام خانوادگی")]
        [StringLength(255, ErrorMessage = "نام خانوادگی نباید کمتر از 1 حرف و بیشتر از 255 حرف باشد", MinimumLength = 1)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string LastName { get; set; }




        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        [DisplayName("نام کاربری")]
        [StringLength(256, ErrorMessage = "کلمه عبور نباید کمتر از ۵ حرف و بیتشر از ۲۵۶ حرف باشد", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا فقط از حروف انگلیسی و اعدد استفاده کنید")]
        public string UserName { get; set; }




        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [StringLength(50, ErrorMessage = "کلمه عبور نباید کمتر از ۵ حرف و بیشتر از ۵۰ حرف باشد", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا فقط از حروف انگلیسی و اعدد استفاده کنید")]
        [DataType(DataType.Password)]
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }



        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [DisplayName("تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمات عبور باهم مطابقت ندارند")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "لطفا نام آژانس را وارد کنید")]
        [DisplayName("نام آژانس")]
        [StringLength(100, ErrorMessage = "نام نباید کمتر از 3 حرف و بیشتر از 100 حرف باشد", MinimumLength = 3)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string AgencyName { get; set; }

    }
}
