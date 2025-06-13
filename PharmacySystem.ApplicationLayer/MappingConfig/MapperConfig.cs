using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Create;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Update;
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
            // CREATE & UPDATE mappings (one-way only)
            CreateMap<CreateWarhouseDTO, WareHouse>();
            CreateMap<CreateWareHouseAreaDTO, WareHouseArea>();
            CreateMap<UpdateWareHouseAreaDTO, WareHouseArea>();
            CreateMap<UpdateWarehouseMedicineDTO, WareHouseMedicien>().ReverseMap();
            CreateMap<CreateWarehouseMedicineDTO, WareHouseMedicien>().ReverseMap();
            CreateMap<CreateWarhouseDTO, WareHouse>()
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas))
                .ForMember(dest => dest.WareHouseMedicines, opt => opt.MapFrom(src => src.WareHouseMedicines))
                .ReverseMap();
            CreateMap<UpdateWareHouseDTO, WareHouse>()
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas))
                .ForMember(dest => dest.WareHouseMedicines, opt => opt.MapFrom(src => src.WareHouseMedicines))
                .ReverseMap();

            // READ mappings
            CreateMap<WareHouseArea, ReadWareHouseAreaDTO>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.Name));

            CreateMap<ReadWareHouseAreaDTO, WareHouseArea>()
                .ForMember(dest => dest.Area, opt => opt.Ignore()); 
            
            CreateMap<WareHouse, ReadWareHouseDTO>()
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas));
            
            CreateMap<WareHouse, ReadWarehouseDetailsDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ApprovedByAdminName, opt => opt.MapFrom(src => src.ApprovedByAdmin != null ? src.ApprovedByAdmin.UserName : null))
                .ForMember(dest => dest.AreaNames, opt => opt.MapFrom(src =>
                    src.WareHouseAreas != null ?
                        src.WareHouseAreas
                            .Where(wa => wa.Area != null)
                            .Select(wa => wa.Area.Name)
                            .ToList()
                        : new List<string>()))
                .ForMember(dest => dest.Medicines, opt => opt.MapFrom(src => src.WareHouseMedicines));

            CreateMap<WareHouseMedicien, WarehouseMedicineDto>()
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.MedicineId));

        }
    }
}
