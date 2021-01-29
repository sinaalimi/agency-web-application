using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Vehicle;

namespace Agency.ViewModel.Vehicle
{
    public class VehicleFormatViewModel
    {
        public Guid VehicleId { get; set; }

        public List<CreateSaetViewModel> Seats { get; set; }

        public int SeatCount { get; set; }
        /// <summary>
        /// قبلا چیدمان تعریف شده براش یا نه
        /// </summary>
        public bool IsFormated { get; set; }

        public bool IsRemovable { get; set; }
    }
}
