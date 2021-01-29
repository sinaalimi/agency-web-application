using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.MainHotel
{
    public class MainHotel
    {
        #region Property
        public  Guid Id { get; set;  }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int level { get; set; }

        public string Adress { get; set;  }

        public string Tel { get; set;  }

        public  string Image { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }


        #endregion

        #region Ctor

        public MainHotel()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Navigation

        public  virtual User.User User { get; set;  }

        public  virtual State.State State { get; set;  }

        public  virtual City.City City { get; set; }

        public virtual ICollection<MainHotelOption.MainHotelOption> MainHotelOptions { get; set; }

        public virtual ICollection<RoomMainHotel.RoomMainHotel> RoomMainHotels { get; set; }

        #endregion

    }
}
