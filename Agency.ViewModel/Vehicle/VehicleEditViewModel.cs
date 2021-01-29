using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Agency.DomainClasses.Entities.Vehicle;

namespace Agency.ViewModel.Vehicle
{
    public class VehicleEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "لطفا نام وسیله نقلیه را وارد کنید")]
        [DisplayName("نام")]
        [StringLength(100, ErrorMessage = "نام نباید بیشتر از 100 حرف باشد")]
        public string Name { get; set; }


        [Required(ErrorMessage = "لطفا ظرفیت وسیله نقلیه را وارد کنید")]
        [DisplayName("ظرفیت")]
        public int? Capacity { get; set; }

        [DisplayName("انتخاب تصویر")]
       //[Required(ErrorMessage = "لطفا تصویر را انتخاب کنید")]
        public HttpPostedFileBase PicSrFile { get; set; }
        public string ImageSource { get; set; }
    }
}
