using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.DomainClasses.Entities.City;
using Agency.DomainClasses.Entities.State;
using Agency.ViewModel.Option;

namespace Agency.ViewModel.Tour
{
     public class EditTourViewModel
    {
        public Guid Id { get; set;  }
        #region Properties
        [Required(ErrorMessage = "لطفا نام تور را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام تور نباید بیش تر از 100 حرف باشد")]
        [DisplayName("عنوان تور")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لطفا تاریخ را وارد کنید")]
        [DisplayName("تاریخ اعزام")]
        [UIHint("PersianDatePicker")]
        [Column(TypeName = "Date")]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "لطفا تاریخ را وارد کنید")]
        [DisplayName(" تاریخ بازگشت")]
        [UIHint("PersianDatePicker")]
        [Column(TypeName = "Date")]
        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "لطفا ظرفیت را وارد کنید")]
        [DisplayName("ظرفیت تور")]
        public int? Capacity { get; set; }

        [Required(ErrorMessage = "لطفا شهر مبدا را وارد کنید")]
        [DisplayName("شهر مبدا")]
        public Guid SourceCityId { get; set; }

        [Required(ErrorMessage = "لطفا شهر مقصد را وارد کنید")]
        [DisplayName("شهر مقصد")]
        public Guid DestinationCityId { get; set; }

        [Required(ErrorMessage = "لطفا استان مقصد را وارد کنید")]
        [DisplayName("استان مقصد")]
        public Guid DestinationStateId { get; set; }

        [Required(ErrorMessage = "لطفا استان مبدا را وارد کنید")]
        [DisplayName("استان مبدا")]
        public Guid SourceStateId { get; set; }

        [StringLength(250, ErrorMessage = "مسیر رفت نباید بیش تر از 250 حرف باشد")]
        [DisplayName("مسیر رفت")]
        public string GoRoad { get; set; }

        [StringLength(250, ErrorMessage = "مسیر برگشت نباید بیش تر از 250 حرف باشد")]
        [DisplayName("مسیر برگشت")]
        public string ComeRoad { get; set; }

        [Required(ErrorMessage = "لطفا هزینه بزرگسال را وارد کنید")]
        [DisplayName("هزینه بزرگسال")]
        public int? AdultPrice { get; set; }

        [Required(ErrorMessage = "لطفا هزینه کودک را وارد کنید")]
        [DisplayName("هزینه کودک")]
        public int? ChildPrice { get; set; }

        [DisplayName("هزینه نوزاد")]
        public int BabyPrice { get; set; }

        [Required(ErrorMessage = "لطفا ساعت شروع را وارد کنید")]
        [DisplayName("ساعت حرکت")]
        public string StartHour { get; set; }

        [Required(ErrorMessage = "لطفا سهم آژانس را وارد کنید")]
        [DisplayName("سهم آژانس")]
        public int? AgencyShare { get; set; }

        [StringLength(100, ErrorMessage = "نام سرپرست تور نباید بیش تر از 100 حرف باشد")]
        [DisplayName("سرپرست تور")]
        public string SupervisorName { get; set; }


        [Required(ErrorMessage = "لطفا هزینه بیمه را وارد کنید")]
        [DisplayName("هزینه بیمه")]
        public int? IsurancePrice { get; set; }


        [Required(ErrorMessage = "لطفا آخرین تاریخ ثبت نام را قید کنید")]
        [DisplayName("پایان مهلت ثبت نام")]
        [UIHint("PersianDatePicker")]
        [Column(TypeName = "Date")]
        public DateTime? FinishRegister { get; set; }


        [Required(ErrorMessage = "لطفا سهم دفتر را وارد کنید")]
        [DisplayName("سهم دفتر")]
        public int? OfficeCost { get; set; }


        [Required(ErrorMessage = "لطفا هزینه دو تخته را وارد کنید")]
        [DisplayName("قیمت دو تخته")]
        public int? CoupleBedPrice { get; set; }


        public List<SelectListItem> Hotels { get; set; }

        [Display(Name = "انتخاب هتل")]
        [Required(ErrorMessage = "لطفا هتل مورد نظر خود را انتخاب کنید")]
        public List<Guid> HotelIds { get; set; }


        public List<SelectListItem> SourceCitiesLitsItem { get; set; }


        public List<SelectListItem> SourceStatesListItem { get; set; }


        public List<SelectListItem> DesStatesListItem { get; set; }


        public List<SelectListItem> DesCitiesListItem { get; set; }

        public City SouCity { get; set; }

        public State SourceState { get; set; }

        public City DesCity { get; set; }

        public State DesState { get; set; }

        public List<VehicleItemViewModel> VehicleList { get; set; }

        public List<OptionViewModel> OptionList { get; set; } 


    }
    #endregion
}
