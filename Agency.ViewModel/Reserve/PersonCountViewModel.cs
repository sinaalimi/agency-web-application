using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Agency.ViewModel.Reserve
{
    public class PersonCountViewModel
    {
        [Required(ErrorMessage = "لطفا تعداد افراد بزرگسال را وارد کنید")]
        [DisplayName("تعداد بزرگسال")]
        [Range(1,1000,ErrorMessage = "وجود حداقل یک بزرگسال الزامی است")]
        public int AdultCount { get; set; }

        [Required(ErrorMessage = "لطفا تعداد کودکان را وارد کنید")]
        [DisplayName("تعداد کودک")]
        public int ChildCount { get; set; }

        [Required(ErrorMessage = "لطفا تعداد نوزادان را وارد کنید")]
        [DisplayName("تعداد نوزاد")]
        public int BabyCount { get; set; }

        public Guid TourId { get; set; }

        public int RemainTourCapacity { get; set; }
    }
}
