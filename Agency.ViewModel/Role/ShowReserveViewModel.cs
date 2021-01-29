using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;

namespace Agency.ViewModel.Reserve
{
   public class ShowReserveViewModel
    {
        public Guid Id { get; set; }

        public ShowTourViewModel Tour { get; set; }

        public  ShowHotelViewModel Hotel { get; set; }

        public ShowVehicleViewModel Vehicle { get; set; }
      
        public Guid ParentId { get; set; }

         public string ParentName { get; set; }

        public Guid UserId { get; set; }

        public DateTime ReserveTime { get; set; }  

        public string HotelName { get; set; }

        public int PersonCount { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string CodeRahgiri { get; set; }

        public int TotalCost { get; set; }

        public bool IsCanseled { get; set; }

        public string ContractFilePath { get; set; }
    }
}
