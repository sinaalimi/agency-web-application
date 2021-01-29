using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.tour
{
    public class Tour
    {
        #region Properties
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartTime { get; set; }

        [Column(TypeName = "Date")]

        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        public  Guid SourceCityId { get; set;  }

        public  Guid SourceStateId { get; set;  }

        public  Guid DestinationCityId { get; set; }

        public  Guid DestinationStateId { get; set; }

        public string GoRoad { get; set; }

        public string ComeRoad { get; set; }

        public int AdultPrice { get; set; }

        public int ChildPrice { get; set; }

        public string StartHour { get; set; }

        public int AgencyShare { get; set; }

        public string SupervisorName { get; set; }

        public int IsurancePrice { get; set; }

        [Column(TypeName = "Date")]

        public DateTime FinishRegister { get; set; }

        public bool IsCanceled { get; set; }

        public int OfficeCost { get; set; }

        public int CoupleBedPrice { get; set; }


        #endregion

        #region Ctor
        public Tour()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region navigation
      
        public virtual User.User User { get; set; }

        public virtual ICollection<Reserve.Reserve> Reserves { get; set; }

        public virtual ICollection<TourVehicle.TourVehicle> TourVehicles { get; set; }

        public virtual ICollection<TourHotel.TourHotel> TourHotels { get; set; }

        public virtual ICollection<TourOption.TourOption> TourOptions { get; set; } 

        public virtual City.City City { get; set; }

        public virtual State.State State { get; set; }
        //public virtual ICollection<Seat> Seats { get; set; }


        #endregion



    }
}
