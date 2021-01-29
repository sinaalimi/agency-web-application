using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Hotel
{
    public class Hotel
    {
        #region Properties
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public short Degree { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Guid CityId { get; set; }

        public Guid StateId { get; set; }

        public string ManagerName { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }

        #endregion

        #region Ctor

        public Hotel()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region navigation

        public virtual City.City City { get; set; }

        public virtual ICollection<TourHotel.TourHotel> TourHotels { get; set; }

        public virtual State.State State { get; set; }

        public virtual ICollection<Reserve.Reserve> Reserves { get; set; }

        #endregion
    }
}
