using AutoMapper;
using GMS.Data.DTOs;

namespace GMS.Api.AMProfiles
{
    public class ManagerProfiles : Profile
    {
        public ManagerProfiles()
        {
            //Source -> Destination
            CreateMap<Manager, ManagerDTO>();
            CreateMap<Gym, GymDTO>()
            .ForMember(x=>x.GymName, opt=>opt.MapFrom(x=>x.GymName))
            .ForMember(x=>x.ImageFilePath, opt=>opt.MapFrom(x=>x.Image))
            ;
        }
    }
}