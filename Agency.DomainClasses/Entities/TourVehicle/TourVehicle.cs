using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.TourVehicle
{
     public class TourVehicle
    {


        #region properties
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid TourId { get; set; }
        public int Index { get; set; }

        #endregion

        #region ctor

        public TourVehicle()
        {
            Id=SequentialGuidGenerator.NewSequentialGuid();

        }
        #endregion

        #region navigation
        public virtual Vehicle.Vehicle Vehicle { get; set; }

        public virtual tour.Tour Tour { get; set; }

        public virtual ICollection<Vehicle.Seat> Seats { get; set; }

        public virtual ICollection<Reserve.Reserve> Reserve { get; set; }
        #endregion
    }
}
