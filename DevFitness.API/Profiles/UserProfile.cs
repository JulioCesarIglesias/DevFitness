using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.inputModels;
using DevFitness.API.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<CreateUserInputModel, User>();
        }
    }
}
