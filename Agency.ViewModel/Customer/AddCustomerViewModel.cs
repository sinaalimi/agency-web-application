using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Agency.ViewModel.Customer
{
    public class AddCustomerViewModel
    {
        #region Ctor

        public AddCustomerViewModel()
        {
            RegisterDate = DateTime.Now;
        }
        #endregion

        #region properties
        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام نباید کمتر از 3 حرف و بیشتر از 100 حرف باشد", MinimumLength = 3)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام خانوادگی نباید کمتر از 3 حرف و بیشتر از 150 حرف باشد", MinimumLength = 3)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        public string LastName { get; set; }

        [DisplayName("جنسیت")]
        public bool Sexuality { get; set; }


        [DisplayName("شماره شناسنامه")]
        [Required(ErrorMessage = "لطفا شماره شناسنامه را وارد کنید")]
        [StringLength(10, ErrorMessage = "شماره شناسنامه نباید کمتر از 1 رقم و بیشتر از 10 رقم باشد", MinimumLength = 1)]
        [RegularExpression(@"^[0-9]+$",ErrorMessage = "لطفا شماره شناسنامه را وارد کنید")]
        public string SsNumber { get; set; }


        [DisplayName("کد ملی")]
        [Required(ErrorMessage = "لطفا کد ملی را وارد کنید")]
        [StringLength(10, ErrorMessage = "کد ملی باید 10 رقمی باشد", MinimumLength = 10)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "لطفا کد ملی را وارد کنید")]
        public string NationalNumber { get; set; }


        [DisplayName("تاریخ تولد")]
        [Required(ErrorMessage = "لطفا تاریخ تولد را وارد کنید")]
        [UIHint("PersianDatePicker")]
        public DateTime? BirthDate { get; set; }


        [DisplayName("تاریخ ثبت")]
        [Required(ErrorMessage = "لطفا تاریخ ثبت را وارد کنید")]

        public DateTime RegisterDate { get; set; }



        [DisplayName("تلفن ثابت")]
        [Required(ErrorMessage = "لطفا تلفن ثابت را وارد کنید")]
        [StringLength(11, ErrorMessage = "تلفن ثابت باید 11 رقمی باشد", MinimumLength = 11)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "شماره تلفن ثابت صحیح نیست")]
        public string Phone { get; set; }


        [DisplayName("موبایل")]
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد کنید")]
        [StringLength(11, ErrorMessage = "شماره موبایل باید 11 رقمی باشد", MinimumLength = 11)]
        [RegularExpression(@"^09[0-9]+$", ErrorMessage = "شماره موبایل صحیح نیست")]
        public string Mobile { get; set; }


        [DisplayName("شغل")]
        [Required(ErrorMessage = "لطفا شغل را وارد کنید")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        [StringLength(100, ErrorMessage = "آدرس منزل نباید کمتر از 3 حرف و بیشتر از 100 حرف باشد", MinimumLength = 3)]
        public string Job { get; set; }


        [DisplayName("استان")]
        [Required(ErrorMessage = "لطفا استان را وارد کنید")]
        [RegularExpression(@"^[\u0600-\u06FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        [StringLength(300, ErrorMessage = "استان نباید بیشتر از 100 حرف باشد")]
        public string State { get; set; }

        [DisplayName("شهر")]
        [Required(ErrorMessage = "لطفا شهر را وارد کنید")]
        [RegularExpression(@"^[\u0600-\u06FF\s]*$", ErrorMessage = "لطفا فقط از حروف  فارسی استفاده کنید")]
        [StringLength(300, ErrorMessage = "شهر نباید بیشتر از 100 حرف باشد")]
        public string City { get; set; }

        [DisplayName("آدرس منزل")]
        [Required(ErrorMessage = "لطفا آدرس منزل را وارد کنید")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,0-9\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی استفاده کنید")]
        [StringLength(300, ErrorMessage = "آدرس منزل نباید کمتر از 10 حرف و بیشتر از 300 حرف باشد", MinimumLength = 10)]

        public string HomeAddress { get; set; }

        [DisplayName("آدرس محل کار")]
        //[Required(ErrorMessage = "لطفا آدرس محل کار را وارد کنید")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,0-9\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی استفاده کنید")]
        //[StringLength(300, ErrorMessage = "آدرس محل کار نباید کمتر از 10 حرف و بیشتر از 300 حرف باشد", MinimumLength = 10)]

        public string BusinessAddress { get; set; }

        [Display(Name = "گروه مشتری")]
        [Required(ErrorMessage = "لطفا یکی از گروه ها را انتخاب نمایید")]
        public Guid GroupId { set; get; }

        public List<SelectListItem> GroupSelectListItems { set; get; }
        #endregion



    }
}
