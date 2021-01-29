using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Agency.ViewModel.RoomMainHotel
{
    public class RoomItemViewModel
    {

        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public Guid HotelId { get; set; }

        [Required(ErrorMessage = "لطفا قیمت اتاق را وارد کنید")]
        public int Price { get; set; }


        [Required(ErrorMessage = "لطفا تعداد اتاق را وارد کنید")]
        public int Count { get; set; }


        [Required(ErrorMessage = "لطفا تعداد باقیمانده اتاق را وارد کنید")]
        public int RemainingCount { get; set; }


        [Required(ErrorMessage = "لطفا تاریخ آغاز رزرو اتاق را وارد کنید")]
        [UIHint("PersianDatePicker")]
        public DateTime FirstDate { get; set; }

        [Required(ErrorMessage = "لطفا تاریخ پایان رزرو اتاق را وارد کنید")]
        [UIHint("PersianDatePicker")]
        public DateTime LasTime { get; set; }

        public string Description { get; set; }

        public List<SelectListItem> RoomListItems { get; set; }

    }
}
