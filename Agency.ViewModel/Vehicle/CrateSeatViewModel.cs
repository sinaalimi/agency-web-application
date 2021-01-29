using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Vehicle
{
   
        public class CreateSaetViewModel
        {
            public int Row { get; set; }

            public int Col { get; set; }

            public Guid VehicleId { get; set; }

            public int? SeatNumber { get; set; }



        }
    
}
