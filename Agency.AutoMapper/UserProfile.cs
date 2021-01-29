using System.Web.Mvc;
using AutoMapper;
using Agency.AutoMapper.Extensions;
using Agency.DomainClasses.Entities.User;
using Agency.ViewModel.User;

namespace Agency.AutoMapper
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserViewModel>().ForMember(dest=>dest.DisplayName,option=>option.MapFrom(src=> src.Name + " " + src.LastName));//.IgnoreAllNonExisting();

            CreateMap<AddUserViewModel, User>();//.IgnoreAllNonExisting();

            CreateMap<EditUserViewModel, User>()
                .ForMember(d => d.Roles, m => m.Ignore());
                 //.IgnoreAllNonExisting();

            CreateMap<User, EditUserViewModel>().ForMember(d => d.Roles, m => m.Ignore());//.IgnoreAllNonExisting();

            CreateMap<User, SelectListItem>()
               .ForMember(d => d.Text, m => m.MapFrom(s => s.UserName))
               .ForMember(d => d.Value, m => m.MapFrom(s => s.Id));//.IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
