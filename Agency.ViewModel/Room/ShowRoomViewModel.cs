using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.MainHotel;
namespace Agency.ViewModel.Room
{
    public class ShowRoomViewModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }
    }
}
