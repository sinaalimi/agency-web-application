using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Hotel;

namespace Agency.ViewModel.Tour
{
    public class TourSummeryViewModel
    {
        #region Properties
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartTime { get; set; }

        [Column(TypeName = "Date")]

        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        public Guid SourceCityId { get; set; }
        public string SourceCity { get; set; }
        public Guid SourceStateId { get; set; }
        public string SourceState { get; set; }
        public Guid DestinationCityId { get; set; }
        public string DesCity { get; set; }
        public Guid DestinationStateId { get; set; }
        public string DesState { get; set; }
        public string GoRoad { get; set; }

        public string ComeRoad { get; set; }

        public int AdultPrice { get; set; }

        public int ChildPrice { get; set; }

        public string StartHour { get; set; }

        public int IsurancePrice { get; set; }

        public string ImageSrc { get; set; }

        [Column(TypeName = "Date")]

        public DateTime FinishRegister { get; set; }

        public string Description { get; set; }

        public string Summery { get; set; }

        public bool IsCanceled { get; set; }

        public bool Breakfast { get; set; }
        public bool Launch { get; set; }
        public bool Dinner { get; set; }
        #endregion
    }
}
