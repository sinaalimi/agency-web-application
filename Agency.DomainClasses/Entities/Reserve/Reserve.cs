using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Reserve
{
    public class Reserve
    {
        #region properties
        public Guid Id { get; set; }

        public Guid TourId { get; set; }
        
        public Guid TourVehicleId { get; set; }

        public Guid ParentId { get; set; }

        public Guid UserId { get; set; }

        public Guid HotelId { get; set; }

        public bool CoupleBed { get; set; }

        public DateTime ReserveTime { get; set; }

        public bool IsCanseled { get; set; }

        public DateTime? CancelDate { get; set; }

        public int Penalty { get; set; }

        public string CodeRahgiri { get; set; }

        public int TotalCost { get; set; }

        public bool IsTemporary { get; set; }

        public string ContractFilePath { get; set; }

        public string CancelFilePath { get; set; }
        #endregion
        
        #region Ctor

        public Reserve()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region navigation

        public virtual User.User User { get; set; }
        public virtual tour.Tour Tour { get; set; }
        public virtual Hotel.Hotel Hotel { get; set; }
        public virtual Person.Person Person { get; set; }
        public virtual TourVehicle.TourVehicle TourVehicle { get; set; }
        public virtual ICollection<Cost.Cost> Costs { get; set; }

        #endregion
    }
}
