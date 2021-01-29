using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.Person;
using Agency.ServiceLayer.Contracts.Cost;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Cost;
using Agency.ViewModel.Option;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Agency.ServiceLayer.EFService.Cost
{
    public class CostService:ICostService
    {

        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<DomainClasses.Entities.tour.Tour> _tours;
        private readonly IDbSet<DomainClasses.Entities.Reserve.Reserve> _reserves;
        private readonly IDbSet<DomainClasses.Entities.TourOption.TourOption> _tourOptions;
        private readonly IDbSet<DomainClasses.Entities.Person.Person> _persons;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Seat> _seats;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public CostService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tours = _unitOfWork.Set<DomainClasses.Entities.tour.Tour>();
            _reserves = _unitOfWork.Set<DomainClasses.Entities.Reserve.Reserve>();
            _tourOptions = _unitOfWork.Set<DomainClasses.Entities.TourOption.TourOption>();
            _persons = _unitOfWork.Set<DomainClasses.Entities.Person.Person>();
            _seats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Seat>();

            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region Edit
        public ShowCostViewModel GetEditView(Guid id)
        {
            var reserve = _reserves.Find(id);

            if (_seats.Count(p => p.ReserveId == id) !=
                _persons.Count(p => p.ParentId == reserve.ParentId && p.AgeRange != AgeRange.Baby))
                return new ShowCostViewModel() {IsAllSeatsReserved = false};

            var personlist = _persons.Where(p => p.ParentId == reserve.ParentId);
            var touroption = _tourOptions.Where(p => p.TourId == reserve.TourId).ProjectTo<OptionViewModel>(_configuration).ToList();
            int AdultCount = 0;
            int ChildCount = 0;
            int BabyCount = 0;
            foreach (var item in personlist)
            {
                if (item.AgeRange == AgeRange.Adult)
                    AdultCount++;
                else if (item.AgeRange == AgeRange.Child)
                    ChildCount++;
                else if (item.AgeRange == AgeRange.Baby)
                {
                    BabyCount++;
                }
            }
            var showcost = new ShowCostViewModel()
            {
                Adult = new AdultCostViewModel(),
                Child = new ChildCostViewModel(),
                Baby = new BabyCostViewModel()
            };
            
            showcost.Adult.Count = AdultCount;
            showcost.Child.Count = ChildCount;
            showcost.Baby.Count = BabyCount;

            var tour = _tours.Find(reserve.TourId);
            showcost.Adult.BasePrice = showcost.Adult.TotalCost = tour.AdultPrice;
            showcost.Child.BasePrice = showcost.Child.TotalCost = tour.ChildPrice;
            showcost.Baby.BasePrice = 0;

            showcost.InsurancPrice = tour.IsurancePrice;
            showcost.OptionList = touroption;
            if (showcost.OptionList != null)
                foreach (var item in showcost.OptionList)
                {
                    showcost.Adult.TotalCost += item.Cost;
                    showcost.Child.TotalCost += item.Cost;
                }

            showcost.Adult.TotalCost += tour.IsurancePrice;
            showcost.Adult.TotalCost *= AdultCount;
            showcost.Child.TotalCost += tour.IsurancePrice;
            showcost.Child.TotalCost *= ChildCount;
            showcost.Baby.TotalCost += tour.IsurancePrice;
            showcost.Baby.TotalCost *= BabyCount;
            showcost.TotalCost = showcost.Adult.TotalCost + showcost.Child.TotalCost + showcost.Baby.TotalCost;
           
            showcost.CoupleBedPrice = tour.CoupleBedPrice;
            if (reserve.CoupleBed)
            {
                showcost.TotalCost += showcost.CoupleBedPrice;
                showcost.Adult.TotalCost += tour.CoupleBedPrice;
            }
            showcost.CoupleBed = reserve.CoupleBed;
            showcost.IsTemporary = reserve.IsTemporary;
            showcost.ContractFilepath = reserve.ContractFilePath;
            return showcost;
        }
        #endregion
    }
}
