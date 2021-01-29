using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ServiceLayer.Contracts.Reserve;
using Agency.ViewModel.ReserveHotel;

namespace Agency.ServiceLayer.Contracts.ReserveHotel
{
    public interface IReserveHotelService
    {
        ListReserveHotelViewmodel GetPagedListHotelReserve(ReserveHotelSearchRequest request);

        ListReserveHotelViewmodel GetPagedListRooms(Guid hotelid, string start,int night);

        List<SelectListItem> StateSelectListIetm();

        List<SelectListItem> CitySelectListIetm(Guid stateid);

        void Create(CreatePersonViewModel viewModel);
    }
}
