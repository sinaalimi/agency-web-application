using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Home;
using Agency.ViewModel.Tour;

namespace Agency.ServiceLayer.Contracts.Website
{
    public interface ISiteService
    {
        ListTourSummeryViewModel GetTours();

        CounterViewModel GetCounter();

        TourSummeryViewModel GetTour(Guid id);

        ListTourSummeryViewModel GetAllTours();

        ListTourSummeryViewModel GetExpiredTours();

        GalleryViewModel GetGallery();
    }
}
