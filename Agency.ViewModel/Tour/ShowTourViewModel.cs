using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.DomainClasses.Entities.City;
using Agency.DomainClasses.Entities.State;
using Agency.DomainClasses.Entities.TourHotel;
using Agency.DomainClasses.Entities.TourVehicle;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Vehicle;

namespace Agency.ViewModel.Tour
{
    public class ShowTourViewModel
    {
        public Guid Id { get; set; }

       // public Guid UserId { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartTime { get; set; }

        [Column(TypeName = "Date")]

        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }
      
        public Guid SourceCityId { get; set; }

        public Guid DestinationCityId { get; set; }
      
        public Guid DestinationStateId { get; set; }

        public Guid SourceStateId { get; set; }

        public string SourceCity { get; set; }


        public string DesCity { get; set; }
        public string GoRoad { get; set; }

        public string ComeRoad { get; set; }

        public int AdultPrice { get; set; }

        public int ChildPrice { get; set; }

        public string StartHour { get; set; }

        public int AgencyShare { get; set; }

        public string SupervisorName { get; set; }

        public int IsurancePrice { get; set; }

        public HttpPostedFileBase PicSrFile { get; set; }
        public string ImageSrc { get; set; }

        [Column(TypeName = "Date")]

        public DateTime FinishRegister { get; set; }

        public string Description { get; set; }

        public bool IsCanceled { get; set; }

        public int OfficeCost { get; set; }

        public bool Breakfast { get; set; }
        public bool Launch { get; set; }
        public bool Dinner { get; set; }

        //  public List<DomainClasses.Entities.Hotel.Hotel> Hotels { get; set; }


        // public Guid[] HotelIds { get; set; }

        public List<VehicleItemViewModel> vehicleList { get; set; } 
        public List<SmallVehicleListViewModel> Vehicles { get; set; }
        public List<SmallHotelListViewModel> Hotels { get; set; }

        public List<SelectListItem> SourceStatesListItem { get; set; }
        public Task<SelectListItem> SourceCitiesLitsItem { get; set; }
        public Task<SelectListItem> DesStatesListItem { get; set; }
        public Task<SelectListItem> DesCitiesListItem { get; set; }

        public List<OptionViewModel> OptionsList { get; set; }

        // public Guid[] VehicleIds { get; set; }
    }




}
