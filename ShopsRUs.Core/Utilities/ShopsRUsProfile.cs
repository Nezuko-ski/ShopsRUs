using AutoMapper;
using ShopsRUs.Core.DTOs;
using ShopsRUs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Utilities
{
    public class ShopsRUsProfile : Profile
    {
        public ShopsRUsProfile()
        {            
            CreateMap<Customer, UserDTO>().ReverseMap().ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email.ToLower()));
        }
    }
}
