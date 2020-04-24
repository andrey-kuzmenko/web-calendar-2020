using WebCalendar.Common;
using WebCalendar.Services.Models.User;
using WebCalendar.Web.Models;

namespace WebCalendar.Web.Mapper
{
    public class UserViewModelProfile : AutoMapperProfile
    {
        public UserViewModelProfile()
        {
            CreateMap<RegistrationModel, UserRegisterServiceModel>();
            CreateMap<LoginModel, UserLoginServiceModel>();
        }
    }
}