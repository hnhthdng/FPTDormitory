using AutoMapper;
using DormModel.DTO.Account;
using DormModel.Model;

namespace DormUtility.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<AppUser, ProfileResposeDTO>();
            CreateMap<ProfileRequestDTO, AppUser>();
        }
    }
}
