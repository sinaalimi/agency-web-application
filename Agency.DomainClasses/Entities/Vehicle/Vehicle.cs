using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;
namespace Agency.DomainClasses.Entities.Vehicle
{
   public class Vehicle
    {
        #region properties
        public  Guid Id { get; set;  }

        public string Name { get; set;  }

        public int Capacity { get; set; }

        #endregion

        #region Ctor
        public Vehicle()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Navigation

        public virtual ICollection<TourVehicle.TourVehicle> TourVehicles { get; set; }

        public virtual ICollection<SeatFormat>  SeatSFormat { get; set; }
        #endregion

    }
}
