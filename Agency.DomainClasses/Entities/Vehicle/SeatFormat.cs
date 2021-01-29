using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Vehicle
{
    public class SeatFormat
    {
        #region Properties
        public Guid Id { get; set; }

        public int SeatNumber { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public Guid VehicleId { get; set; }

        public Guid UserId { get; set; }

        //public Guid TourVehicleId { get; set; }
        #endregion

        #region Ctor

        public SeatFormat()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Navigation 
        public virtual User.User User { get; set; }

        public ICollection<Seat> Seats { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        #endregion
    }
}
