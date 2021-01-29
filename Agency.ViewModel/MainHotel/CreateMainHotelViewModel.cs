using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.ViewModel.Option;
using Agency.ViewModel.RoomMainHotel;

namespace Agency.ViewModel.MainHotel
{
    public class CreateMainHotelViewModel
    {
        #region Properties
        [Required(ErrorMessage = "لطفا نام هتل را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام هتل نباید بیش تر از 100 حرف باشد")]
        [DisplayName("نام هتل")]
        public string Name { get; set; }

        [Required(ErrorMessage ="لطفا درجه هتل را وارد کنید" )]
        [DisplayName("درجه هتل")]
        public int Level { get; set;  }

        [Required(ErrorMessage = "لطفا شماره تلفن را وارد کنید")]
        [DisplayName("شماره تلفن")]
        public  string Tel { get; set;  }

        [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
        [DisplayName("آدرس")]
        public string Adress { get; set;  }


        [DisplayName("عکس هتل")]
        public HttpPostedFileBase PicSrFile { get; set; }
        public string Image { get; set; }

        public List<SelectListItem> Cities { get; set; }

        public List<SelectListItem> States { get; set; }

        [Required(ErrorMessage = "لطفا شهر را انتخاب کنید")]
        [DisplayName("شهر")]

        public Guid CityId { get; set; }

        [DisplayName("استان")]
        [Required(ErrorMessage = "لطفا استان مورد نظر را انتخاب کنید")]
        public Guid StateId { get; set; }

        public List<RoomItemViewModel> RoomMainHotelList { get; set; }

        public List<SelectListItem> RoomTypeList { get; set; } 

        public List<HotelOptionViewModel> OptionList { get; set;  }







        #endregion
    }
}
