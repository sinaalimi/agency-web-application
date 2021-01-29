using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Tour;

namespace Agency.ViewModel.Home
{
    public class GalleryViewModel
    {
        public List<ShowHotelViewModel> Hotels { get; set; }

        public List<TourSummeryViewModel> Tours { get; set; }
    }
}
