using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Agency.ViewModel.Tour
{
    public class VehicleItemViewModel
    {
        [Required(ErrorMessage = "لطفا وسیله نقلیه را انتخاب کنید")]
        public Guid VehicleId { get; set; }
        public string VehicleName { get; set; }
        public List<SelectListItem> VehicleListItems { get; set; }

        public short Count { get; set; }

        public int Index { get; set; }
        public  bool IsEdit { get; set; }


    }
}
