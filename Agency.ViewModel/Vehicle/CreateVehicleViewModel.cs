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
    public class CreateVehicleViewModel
    {

        [Required(ErrorMessage = "لطفا نام وسیله نقلیه را وارد کنید")]
        [DisplayName("نام")]
        [StringLength(100, ErrorMessage = "نام نباید بیشتر از 100 حرف باشد")]
        public string Name { get; set; }


        [Required(ErrorMessage = "لطفا ظرفیت وسیله نقلیه را وارد کنید")]
        [DisplayName("ظرفیت")]
        public int? Capacity { get; set; }
    }
}
