using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Hotel
{
   public class HotelDetailViewModel
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public short Degree { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Guid CityId { get; set; }
  
        public string CityName { get; set; }

        public Guid StateId { get; set; }

        public string StateName { get; set; }

        public string ManagerName { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }
       





    }
}
