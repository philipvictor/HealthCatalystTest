using System.Linq;
using AutoMapper;
using HealthCatalystUserSearchAPI.Context;
using HealthCatalystUserSearchAPI.Models;

namespace HealthCatalystUserSearchAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserDto>()
                .ForMember(dest => dest.MyInterests, opt => opt.MapFrom(src => src.MyInterests.Select(b => b.Interest)))
                .ReverseMap();

            CreateMap<Addresses, AddressDto>().ReverseMap();

            CreateMap<Interests, InterestDto>().ReverseMap();

            CreateMap<UserToInterest, UserToInterestDto>()
                .ForAllMembers(dest => dest.Ignore());

        }
    }
}
