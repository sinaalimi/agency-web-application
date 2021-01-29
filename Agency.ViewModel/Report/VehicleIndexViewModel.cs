using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class VehicleIndexViewModel
    {
        public Guid VehicleId { get; set; }

        public int Index { get; set;  }
        public Guid TourId { get; set;  }

        public string VehicleName { get; set;  }
    }
}
