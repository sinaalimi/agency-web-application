using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.Common.Controller;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.Reserve;

namespace Agency.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelService _hotelService;
        private readonly IReserveService _reserveService;
       //private readonly IApplicationUserManager _userManager;
        #endregion
        
        #region Ctor

        public ScheduleController(IUnitOfWork unitOfWork,
            IHotelService hotelService, IReserveService reserveService)
        {
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
            _hotelService = hotelService;
            _reserveService = reserveService;
        }
        #endregion

        public async Task<ActionResult> Index()
        {
            await _reserveService.Schedule();
            return null;
        }
    }
}