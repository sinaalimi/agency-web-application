using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.MainHotelOption
{
    public class MainHotelOption
    {
        #region Property

        public Guid Id { get; set; }


        public string Name { get; set; }

        public int Price { get; set; }

        public Guid HotelId { get; set; }

        public bool IsFree { get; set; }
        #endregion

        #region Ctor

        public MainHotelOption()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        #endregion

        #region Navigation

        public virtual MainHotel.MainHotel MainHotel { get; set; }

        #endregion
    }
}
