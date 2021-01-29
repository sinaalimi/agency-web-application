using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.City;
using Agency.DomainClasses.Entities.State;
using Agency.DomainClasses.Entities.User;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ServiceLayer.Security;
using Agency.Utilities;
using Agency.ViewModel.Common;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.StateCity;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using Newtonsoft.Json;

namespace Agency.ServiceLayer.EFService.StateCity
{
    public class StateCityService : IStateCityService
    {
        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<State> _states;
        private readonly IDbSet<City> _cities;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public StateCityService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _states = _unitOfWork.Set<State>();
            _cities = _unitOfWork.Set<City>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region  GetStatesAsync
        public async Task<List<SelectListItem>> GetStatesAsync()
        {      
             return await _states.AsNoTracking().OrderBy(p=>p.Name).Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToListAsync();   
        }

        #endregion

        #region  GetCitiesAsync
        public List<SelectListItem> GetCities(Guid id)
        {
            var temp = _cities.Where(p => p.StateId == id).OrderBy(p=>p.Name).ToList();
            return temp.Select(variable => new SelectListItem() { Text = variable.Name, Value = variable.Id.ToString() }).ToList();
        }

        #endregion

        #region SeedDatabase

        private class cities
        {
            public string name { get; set; }
        }

        private class myobject
        {
            public string name { get; set; }
            public List<cities> Cities { get; set; }

        }

        public void SeedDatabase()
        {
            if (!_states.Any())
            {
                var path = HttpContext.Current.Server.MapPath("~/Content/Province.json");
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    List<myobject> items = JsonConvert.DeserializeObject<List<myobject>>(json);
                    foreach (var item in items)
                    {
                        var s1 = _states.Add(new State() { Name = item.name });
                        foreach (var city in item.Cities)
                        {
                            _cities.Add(new City() { Name = city.name, StateId = s1.Id });
                        }
                    }
                }

                //var s1=_states.Add(new State() {Name = "اذربایجان غربی"});
                //var s2 = _states.Add(new State() {Name = "تهران"});
                //var c1 = _cities.Add(new City() {Name = "ارومیه", StateId = s1.Id});
                //var c2 = _cities.Add(new City() {Name = "تهران", StateId = s2.Id});
                _unitOfWork.SaveAllChanges();
            }
        }

        #endregion

        #region CreateState
      
        public void Create(CreateStateViewModel viewModel)
        {
            var state = _mappingEngine.Map<State>(viewModel);
            _states.Add(state);
            _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region CreateCity
        public void CreateCity(CreateCityViewModel viewModel)
        {
            var city = _mappingEngine.Map<City>(viewModel);
            _cities.Add(city);
            _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region ListState
        public async Task<ListStateViewModel> GetPagedListAsync(StateSearchRequest request)
        {

            var state = _states.AsNoTracking();
            //if (request.Type.HasValue())
            //{
            //    vehicle = vehicle.Where(p => p.VehicleType.Contains(request.Type)).AsQueryable();
            //}
            if (request.Name.HasValue())
            {
                state = _states.Where(p => p.Name.Contains(request.Name)).AsQueryable();
            }


            state = state.OrderBy($"{request.CurrentSort} {request.SortDirection}");
            //var x = await vehicle.ProjectTo<ShowVehicleViewModel>(_configuration).ToListAsync();

            var query = await state
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<ShowStateViewModel>(_configuration)
                    .ToListAsync();

            return new ListStateViewModel()
            {
                SearchRequest = request,
                states = query
            };
        }
        #endregion

        #region ListCity
        public async Task<ListCityViewModel> GetPagedListCityAsync(CitySearchRequest request)
        {

            var city = _cities.AsNoTracking().Include(p=>p.State).AsQueryable();
            //if (request.Type.HasValue())
            //{
            //    vehicle = vehicle.Where(p => p.VehicleType.Contains(request.Type)).AsQueryable();
            //}
            if (request.Name.HasValue())
            {
                city = _cities.Where(p => p.Name.Contains(request.Name)).AsQueryable();
            }
            if (request.StateId != null && request.StateId!=new Guid())
            {
                city = city.Where(p => p.StateId == request.StateId).AsQueryable();
            }
            

            city = city.OrderBy($"{request.CurrentSort} {request.SortDirection}");
            //var x = await vehicle.ProjectTo<ShowVehicleViewModel>(_configuration).ToListAsync();

            var query = await city
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<ShowCityViewModel>(_configuration)
                    .ToListAsync();
            foreach (var item in query)
            {
                item.StateName = query.SingleOrDefault(p=>p.StateId==item.StateId && p.Id==item.Id).StateName;
            }
            return new ListCityViewModel()
            {
                SearchRequest = request,
                cities = query
            };
        }
        #endregion

        #region GetEditViewAsync

        public async Task<EditStateViewModel> GetEditViewAsync(Guid id)
        {
            var model = await _states.AsNoTracking()
                        .ProjectTo<EditStateViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);
            return model;
        }
        #endregion

        #region GetEditCityViewAsync

        public async Task<EditCityViewModel> GetEditCityViewAsync(Guid id)
        {
            var model = await _cities.AsNoTracking()
                        .ProjectTo<EditCityViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);
            var states = _states.AsNoTracking().Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            var state1 = states.Find(p => p.Value == model.StateId.ToString());
            state1.Selected = true;
            model.States = states;
            return model;
        }
        #endregion

        #region EditState

        public async Task<ShowStateViewModel> Edit(EditStateViewModel viewModel)
        {
            var state = _states.Find(viewModel.Id);

            _mappingEngine.Map(viewModel, state);
            await _unitOfWork.SaveAllChangesAsync();
            return await _states.AsNoTracking().ProjectTo<ShowStateViewModel>(_configuration)
                   .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
        }

        #endregion

        #region EditCity

        public async Task<ShowCityViewModel> EditCity( EditCityViewModel viewModel)
        {
            var city = _cities.Find(viewModel.Id);
            _mappingEngine.Map(viewModel, city);
            await _unitOfWork.SaveAllChangesAsync();
            return await _cities.AsNoTracking().ProjectTo<ShowCityViewModel>(_configuration)
                   .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
        }

        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _states.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _states.Where(a => a.Id == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion

        #region DeleteCity
        public async Task<DeleteMessageViewModel> DeleteCityAsync(Guid id)
        {
            if (!await _cities.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _cities.Where(a => a.Id == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion
    }
}
