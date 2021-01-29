using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.DomainClasses.Entities.City;
using Agency.DomainClasses.Entities.State;

namespace Agency.ViewModel.Hotel
{
    public class ShowHotelViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public short Degree { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public City City { get; set; }

        public State State { get; set; }

        public List<SelectListItem> Cities { get; set; }

        public Guid CityId { get; set; }

        public List<SelectListItem> States { get; set; }

        public Guid StateId { get; set; }

        public string ManagerName { get; set; }

        public HttpPostedFileBase PicSrFile { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }

    }
}
