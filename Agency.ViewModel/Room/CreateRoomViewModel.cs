using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Room
{
    public class CreateRoomViewModel
    {
        [Required(ErrorMessage = "لطفا نوع اتاق را مشخص کنید")]
        [DisplayName("نوع")]
        [StringLength(100,ErrorMessage = "لطفا نوع اتاق را کمتر از 100 کاراکتر وارد کنید")]
        public string Type { get; set; }

        [Required(ErrorMessage = "لطفا ظرفیت را مشخص کنید")]
        [DisplayName("ظرفیت")]
        public int Capacity { get; set; }


    }
}
