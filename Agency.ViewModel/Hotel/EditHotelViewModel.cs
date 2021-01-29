using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.DomainClasses.Entities.City;
using Agency.DomainClasses.Entities.State;
using Agency.ViewModel.Tour;

namespace Agency.ViewModel.Hotel
{
   public  class EditHotelViewModel
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "لطفا نام هتل را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام هتل نباید بیش تر از 100 حرف باشد")]
        [DisplayName("نام هتل")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا درجه هتل را وارد کنید")]
        [DisplayName("درجه هتل")]
        public short Degree { get; set; }

        [Required(ErrorMessage = "لطفا آدرس هتل را وارد کنید")]
        [StringLength(250, ErrorMessage = "آدرس هتل نباید بیش تر از 250 حرف باشد")]
        [DisplayName("آدرس هتل")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا شماره تلفن هتل را وارد کنید")]
        [StringLength(11, ErrorMessage = "شماره تلفن نباید بیش تر از 100 حرف باشد")]
        [DisplayName("شماره تلفن ")]
        public string PhoneNumber { get; set; }

        public City City { get; set; }

        public State State { get; set; }

        public List<SelectListItem> Cities { get; set; }


        [Required(ErrorMessage = "لطفا شهر را انتخاب کنید")]
        [DisplayName("شهر")]
        public Guid CityId { get; set; }

       
        public List<SelectListItem> States { get; set; }

        [DisplayName("استان")]
        [Required(ErrorMessage = "لطفا استان مورد نظر را انتخاب کنید")]
        public Guid StateId { get; set; }

        [Required(ErrorMessage = "لطفا نام مدیر هتل را وارد کنید")]
        [StringLength(150, ErrorMessage = "نام نباید بیش تر از 150 حرف باشد")]
        [DisplayName("نام مدیر")]
        public string ManagerName { get; set; }
        [DisplayName("عکس هتل")]
        public HttpPostedFileBase PicSrFile { get; set; }
        public string ImageSource { get; set; }

        [DisplayName(" توضیحات")]
        [StringLength(1000, ErrorMessage = "توضیحات نباید بیش تر از 1000 حرف باشد")]
        public string Description { get; set; }

      
    }
    }






