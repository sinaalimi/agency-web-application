using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Person;

namespace Agency.ViewModel.Reserve
{
     public class CreateReserveViewModel
    {
        public Guid UserId { get; set;  }

        public  Guid TourId { get; set; }

        public List<CreatePersonViewModel> ListPersonViewModel { get; set; }

        public CreateParentViewModel ParentViewModel { get; set; }
        
        public List<SelectListItem> HotelList { get; set; } 
        [DisplayName("هتل")]
        [Required(ErrorMessage = "لطفا هتل را انتخاب کنید")]
        public Guid HotelId { get; set; }

        [DisplayName("تخت دو نفره")]
        public bool CoupleBed { get; set; }
        

        [Required(ErrorMessage = "لطفا تاریخ را وارد کنید")]
        [DisplayName("تاریخ رزرو")]
        [UIHint("PersianDatePicker")]
        [Column(TypeName = "Date")]
        public DateTime ReserveTime { get; set; }

        public List<SelectListItem> VehicleList { get; set; }
        [Required(ErrorMessage = "لطفا ماشین را انتخاب کنید")]
        [DisplayName("ماشین")]
        public Guid TourVehicleId { get; set; }

      
    }
}
