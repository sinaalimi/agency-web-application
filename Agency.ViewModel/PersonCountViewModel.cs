using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel
{
    public class PersonCountViewModel
    {
        [Required(ErrorMessage = "لطفا تعداد بزرگسال را وارد کنید")]
        [DisplayName("تعداد بزرگسال")]
        public int AdultCount { get; set; }

        [Required(ErrorMessage = "لطفا تعداد کودکان را وارد کنید")]
        [DisplayName("تعداد کودکان ")]
        public int ChildCount { get; set; }

        [Required(ErrorMessage = "لطفا تعداد خردسالان را وارد کنید")]
        [DisplayName("تعداد خردسالان ")]
        public int BabyCount { get; set; }


    }
}
