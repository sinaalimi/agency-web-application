using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.TourHotel
{
    public class TourHotel
    {
        #region properties
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public Guid HotelId { get; set; }

        #endregion

        #region ctor

        public TourHotel()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();

        }
        #endregion

        #region navigation
        public virtual Hotel.Hotel Hotel { get; set; }
        public virtual tour.Tour Tour { get; set; }

        #endregion
    }
}
