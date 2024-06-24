using AutoMapper;
using DormModel.DTO.Account;
using DormModel.DTO.Dorm;
using DormModel.DTO.Floor;
using DormModel.Model;

namespace DormUtility.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<AppUser, ProfileResposeDTO>();
            CreateMap<ProfileRequestDTO, AppUser>();
            CreateMap<DormRequestDTO, Dorm>();
            CreateMap<Floor, FloorResponseDTO>();

        }
    }
}
