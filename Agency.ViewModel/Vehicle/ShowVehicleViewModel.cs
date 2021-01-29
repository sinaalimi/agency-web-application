using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Agency.DomainClasses.Entities.Vehicle;

namespace Agency.ViewModel.Vehicle
{
    public class ShowVehicleViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public HttpPostedFileBase PicSrFile { get; set; }

        public string ImageSource { get; set; }

    }
}
