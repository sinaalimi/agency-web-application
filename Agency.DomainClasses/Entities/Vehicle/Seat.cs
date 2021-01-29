using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.tour;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Vehicle
{
    public class Seat
    {
        #region Properties
        public Guid Id { get; set; }

        //public int SeatNumber { get; set; }

        public bool SeatGender { get; set;  }

        public bool Isempty { get; set; }

        public bool IsDeactive { get; set; }

        // public int Row { get; set; }

        //public int Col { get; set; }

        //public Guid VehicleId { get; set; }
        public Guid SeatId { get; set; }

        public Guid UserId { get; set; }

        public Guid? PersonId { get; set; }

        public Guid TourVehicleId { get; set; }

        public Guid ReserveId { get; set; }
       // public Guid TourId { get; set; }
        #endregion

        #region Ctor

        public Seat()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Navigation 
        public virtual User.User User { get; set; }

        public virtual SeatFormat SeatFormat { get; set; }

        public virtual TourVehicle.TourVehicle TourVehicle { get; set; }

        public virtual Person.Person Person { get; set; }
        //public virtual Tour Tour { get; set; }

        #endregion
    }
}
