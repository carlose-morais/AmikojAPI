using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmikojApi.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Models.ApplicationUser, Models.ApplicationUserModel>();
            CreateMap<Models.ApplicationUserModel, Models.ApplicationUser>();
            CreateMap<Models.ApplicationUser, Models.AuthenticationUserModel>();
            CreateMap<Models.AuthenticationUserModel, Models.ApplicationUser>();
        }
    }
}
