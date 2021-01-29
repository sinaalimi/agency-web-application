using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.NewPerson;
using Agency.DomainClasses.Entities.Person;
using Agency.ViewModel.ReserveHotel;
using Agency.ViewModel.Reserve;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class NewPersonProfile :Profile
    {
        protected override void Configure()
        {
            CreateMap<NewPerson, CreatePersonViewModel>();
            CreateMap<CreatePersonViewModel, NewPerson>();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
