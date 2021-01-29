using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.MainHotel
{
    public class Room
    {
        #region Property

       
        public Guid Id { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }

        public Guid UserId { get; set; }

        #endregion

        #region Ctor 

        public Room()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        #endregion

        #region Navigation

        public virtual User.User User { get; set; }

        public virtual ICollection<RoomMainHotel.RoomMainHotel> RoomMainHotels { get; set; }

        #endregion
    }
}
