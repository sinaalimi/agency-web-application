using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.ViewModel.Option;

namespace Agency.ViewModel.Tour
{
    public class TourDetailViewModel
    {
        #region Properties

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        public Guid SourceCityId { get; set; }
        public string SourceCity { get; set; }

        public Guid SourceStateId { get; set; }
        public string SourceSate { get; set; }

        public Guid DestinationCityId { get; set; }
        public string DesCity { get; set; }

        public Guid DestinationStateId { get; set; }
        public string DesState { get; set; }

        public string GoRoad { get; set; }

        public string ComeRoad { get; set; }

        public int AdultPrice { get; set; }

        public int ChildPrice { get; set; }

        public string StartHour { get; set; }

        public int AgencyShare { get; set; }

        public string SupervisorName { get; set; }

        public int IsurancePrice { get; set; }

        public DateTime FinishRegister { get; set; }

        public string Description { get; set; }

        public bool IsCanceled { get; set; }

        public int OfficeCost { get; set; }

        public List<string> Vehicles { get; set; }

        public List<string> Hotels { get; set; }

        public List<string> HotelDesciption { get; set; }
        public List<OptionViewModel> OptionsList { get; set; }

        #endregion
    }
}
