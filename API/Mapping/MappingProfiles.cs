using API.DTOs;
using AutoMapper;
using Data.Models;

namespace API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Models to DTOs
            CreateMap<Ad, GetAdDTO>();
            CreateMap<Ad, BasketAdDTO>();
            CreateMap<Basket, BasketDTO>();
            CreateMap<BasketItem, BasketItemDTO>();
            #endregion

            #region DTOs to Models
            CreateMap<SaveAdResource, Ad>()
                .ForMember(x => x.Images, y => y.Ignore());
            #endregion
        }


    }
}
