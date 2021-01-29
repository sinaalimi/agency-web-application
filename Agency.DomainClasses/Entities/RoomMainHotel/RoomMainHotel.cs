using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.RoomMainHotel
{
    public class RoomMainHotel
    {
        #region Property

        public Guid Id { get; set; }

       // public Guid UserId { get; set; }

        public Guid RoomId { get; set; }

        public Guid HotelId { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public int RemainingCount { get; set; }

        public DateTime FirstDate { get; set; }

        public DateTime LasTime { get; set; }

        public string Description { get; set; }


        #endregion

        #region Ctor

        public RoomMainHotel()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        #endregion

        #region Navigation

        //public virtual User.User User { get; set; }

        public virtual MainHotel.MainHotel MainHotel { get; set; }

        public virtual MainHotel.Room Room { get; set; }

        public virtual ICollection<PersonRoomHotel.PersonRoomHotel> PersonRoomHotels { get; set; }

        #endregion
    }
}
