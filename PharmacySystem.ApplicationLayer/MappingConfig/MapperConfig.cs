using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Create;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Read;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines;
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
            // CREATE & UPDATE mappings
            CreateMap<CreateWarhouseDTO, WareHouse>()
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas))
                .ForMember(dest => dest.WareHouseMedicines, opt => opt.MapFrom(src => src.WareHouseMedicines))
                .ReverseMap();

            CreateMap<CreateWareHouseAreaDTO, WareHouseArea>();
            CreateMap<UpdateWareHouseAreaDTO, WareHouseArea>();

            CreateMap<UpdateWarehouseMedicineDTO, WareHouseMedicien>().ReverseMap();
            CreateMap<CreateWarehouseMedicineDTO, WareHouseMedicien>().ReverseMap();

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
                .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.MedicineId))
                .ReverseMap();

            CreateMap<WareHouse, SimpleReadWarehouseDTO>()
                .ForMember(dest => dest.MinmumPrice, opt =>
                    opt.MapFrom((src, dest, destMember, context) =>
                        src.WareHouseAreas
                            .FirstOrDefault(a => a.AreaId == (int)context.Items["areaId"])?.MinmumPrice ?? 0
                    ));

            // Representative Mappings
            #region Representative
            CreateMap<Representative, GetAllRepresentatitveDto>();
            CreateMap<CreateRepresentatitveDto, Representative>();
            CreateMap<UpdateRepresentativeDto, Representative>();

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
