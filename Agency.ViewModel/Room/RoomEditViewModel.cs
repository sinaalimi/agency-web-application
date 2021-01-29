using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Room
{
    public class RoomEditViewModel
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "لطفا نوع اتاق را انتخاب کنید")]
        [DisplayName("نوع")]
        [StringLength(100, ErrorMessage = "نوع نباید بیشتر از 100 حرف باشد")]
        public string Type { get; set; }


        [Required(ErrorMessage = "لطفا ظرفیت اتاق را انتخاب کنید")]
        [DisplayName("ظرفیت")]
        public int Capacity { get; set; }

    }
}
