using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Person;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.ViewModel.Person;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Vehicle;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class PersonProfilev: Profile
    {
        protected override void Configure()
        {
            CreateMap<Person, CreatePersonViewModel>();
            CreateMap<CreatePersonViewModel, Person>();

            CreateMap<CreateParentViewModel, Person>();
            CreateMap<Person, CreateParentViewModel>();

            CreateMap<Person, ShowPersonViewModel>();

            CreateMap<Person, EditPersonViewModel>();
            CreateMap<EditPersonViewModel, Person>();
            CreateMap<EditParentViewModel, Person>();
            CreateMap<Person, EditParentViewModel>();


        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
