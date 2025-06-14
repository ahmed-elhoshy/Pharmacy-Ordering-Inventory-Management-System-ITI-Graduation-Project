using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Create;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Read;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           
            CreateMap<WareHouseMedicien, WarehouseMedicineDto>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name)).ReverseMap();


            #region Representative
            CreateMap<Representative, GetAllRepresentatitveDto>();

            CreateMap<CreateRepresentatitveDto , Representative>();

            CreateMap<UpdateRepresentativeDto , Representative>();

            CreateMap<Representative, GetRepresentatitveByIdDto>()
                .ForMember(dest => dest.Representatitve_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Representatitve_Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Representative, GetRepresentatitvePharmaciesCountDto>()
                .ForMember(dest => dest.RepresentatitveId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RepresentatitveName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PharmaciesCount, opt => opt.MapFrom(src => src.pharmacies.Count));

            CreateMap<Representative, GetOrdersPharmaciesCountDto>()
                .ForMember(dest => dest.PharmaciesName, opt => opt.MapFrom(src => 
                    string.Join(", ", src.pharmacies.Select(p => p.Name))))

                .ForMember(dest => dest.PharmaciesGovernate, opt => opt.MapFrom(src => 
                    string.Join(", ", src.pharmacies.Select(p => p.Governate))))

                .ForMember(dest => dest.PharmaciesAddress, opt => opt.MapFrom(src => 
                    string.Join(", ", src.pharmacies.Select(p => p.Address))))

                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => 
                    src.pharmacies.Sum(p => p.Orders.Count)));

            #endregion
        }
    }
}
