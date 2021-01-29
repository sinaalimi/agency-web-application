using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Vehicle
{
    public class SeatViewModel
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }

        public int SeatNumber { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public bool SeatGender { get; set; }

        public bool Isempty { get; set; }

        public bool IsDeactive { get; set; }

        public string Name { get; set; }

        public bool IsHamGuruh { get; set; }

        public bool IsDetailVisible { get; set; }

        public SeatViewModel()
        {
            Isempty = true;
        }
    }
}
