using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.TourOption;
using Agency.ViewModel.Option;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class Cost : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourOption, OptionViewModel>();
            CreateMap< OptionViewModel,TourOption>();
        }
    }
}