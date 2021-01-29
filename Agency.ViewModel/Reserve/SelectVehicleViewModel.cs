using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Agency.ViewModel.Reserve
{
   public class SelectVehicleViewModel
    {
        [DisplayName("ماشین")]
        [Required(ErrorMessage = "ماشین مورد نظر را انتخاب کنید")]
        public Guid Id { get; set; }
        
        public List<SelectListItem> VehicleList { get; set; }

        public Guid TourId { get; set; }
            

    }
}
