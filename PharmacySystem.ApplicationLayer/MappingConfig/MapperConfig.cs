using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.ApplicationLayer.MappingConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CreateWarhouseDTO, WareHouse>().ReverseMap();
            CreateMap<CreateWareHouseAreaDTO, WareHouseArea>().ReverseMap();

            CreateMap<UpdateWareHouseDTO, WareHouse>().ReverseMap();
            CreateMap<UpdateWareHouseAreaDTO, WareHouseArea>().ReverseMap();

            CreateMap<ReadWareHouseDTO, WareHouse>().ReverseMap();
            CreateMap<ReadWareHouseAreaDTO, WareHouseArea>()
                .ForMember(dest => dest.Area, opt => opt.Ignore()) 
                .ReverseMap()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.Name));

            CreateMap<Area, ReadWareHouseAreaDTO>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AreaId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AreaName));
        }
    }
}
