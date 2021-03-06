﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Person;

namespace Agency.ViewModel.Person
{
    public class EditParentViewModel
    {
        #region properties

        public Guid UserId { get; set; }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [DisplayName("نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "لطفا نام پدر سرپرست را وارد کنید")]
        [DisplayName("نام پدر")]
        public string ParentName { get; set; }

        [Required(ErrorMessage = "لطفا کد ملی را وارد کنید")]
        [DisplayName("کد ملی")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی را صحیح وارد کید")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
        [DisplayName("آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا شماره شناسنامه را وارد کنید")]
        [DisplayName("شماره شناسنامه")]
        public string IdentityCode { get; set; }

        [Required(ErrorMessage = " لطفا تاریخ تولد را وارد کنید")]
        [DisplayName("تاریخ تولد")]
        [UIHint("PersianDatePicker")]
        public DateTime? BirthDay { get; set; }

        [Required(ErrorMessage = " لطفا محل تولد را وارد کنید")]
        [DisplayName("محل تولد")]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = ("لطفا رده سنی را وارد کنید"))]
        [DisplayName("رده سنی")]
        public AgeRange AgeRange { get; set; }

        //[Required(ErrorMessage = "لطفا وارد کنید که فرد مورد نظر سرپرست است یا خیر")]
        //[DisplayName("سرپرست")]
        //public bool IsParent { get; set; }

        [Required(ErrorMessage = "لطفا شماره تلفن را وارد کنید")]
        [DisplayName("شماره تلفن")]
        [RegularExpression(@"\d{11}$", ErrorMessage = "شماره تلفن را صحیح وارد کنید")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "لطفا شماره موبایل را وارد کنید")]
        [DisplayName("موبایل")]
        [RegularExpression(@"\d{11}$", ErrorMessage = "شماره موبایل را صحیح وارد کنید")]
        public string Mobile { get; set; }

        [DisplayName("شغل")]
        public string Job { get; set; }

        [DisplayName("جنسیت")]
        public bool Gender { get; set; }


        #endregion

        public EditParentViewModel()
        {
            Gender = true;
        }
    }
}
