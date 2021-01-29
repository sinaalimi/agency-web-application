using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.PersonRoomHotel
{
    public class PersonRoomHotel
    {
        #region properties
     
        public Guid Id { get; set; }

        public Guid NewPersonId { get; set; }

        public Guid RoomHotelId { get; set; }

        #endregion

        #region ctor

        public PersonRoomHotel()
        {

            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        #endregion

        #region navigation

        public virtual NewPerson.NewPerson NewPerson { get; set; }

        public virtual RoomMainHotel.RoomMainHotel RoomMainHotel { get; set; }

        #endregion
    }
}
