#region MyRegion
using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.Admin;
using PharmacySystem.ApplicationLayer.DTOs.Area;
using PharmacySystem.ApplicationLayer.DTOs.Governate;
using PharmacySystem.ApplicationLayer.DTOs.OrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Read;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Create;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Update;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.ApplicationLayer.DTOs.Representative.Login;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Login;
using PharmacySystem.ApplicationLayer.DTOs.Orders;
#endregion

namespace PharmacySystem.ApplicationLayer.MappingConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Admin Mappings
            CreateMap<Admin, AdminResponseDto>();
            CreateMap<CreateAdminDto, Admin>();
            CreateMap<UpdateAdminDto, Admin>();
            #endregion

            #region Warehouse Mappings
            // CREATE & UPDATE mappings
            CreateMap<CreateWarehouseDTO, WareHouse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas))
                .ForMember(dest => dest.WareHouseMedicines, opt => opt.MapFrom(src => src.WareHouseMedicines))
                .ReverseMap();

            CreateMap<UpdateWareHouseDTO, WareHouse>()
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas))
                .ForMember(dest => dest.WareHouseMedicines, opt => opt.MapFrom(src => src.WareHouseMedicines))
                .ReverseMap();

            CreateMap<CreateWarehouseMedicineDTO, WareHouseMedicien>().ReverseMap();
            CreateMap<UpdateWarehouseMedicineDTO, WareHouseMedicien>().ReverseMap();

            CreateMap<CreateWareHouseAreaDTO, WareHouseArea>();
            CreateMap<UpdateWareHouseAreaDTO, WareHouseArea>();

            // READ mappings
            CreateMap<WareHouse, ReadWareHouseDTO>()
                .ForMember(dest => dest.WareHouseAreas, opt => opt.MapFrom(src => src.WareHouseAreas));

            CreateMap<WareHouse, ReadWarehouseDetailsDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Governate, opt => opt.MapFrom(src => src.Governate))
                .ForMember(dest => dest.AreaNames, opt => opt.MapFrom(src =>
                    src.WareHouseAreas != null
                        ? src.WareHouseAreas
                            .Where(wa => wa.Area != null)
                            .Select(wa => wa.Area.Name)
                            .ToList()
                        : new List<string>()
                ))
                .ForMember(dest => dest.Medicines, opt => opt.MapFrom(src => src.WareHouseMedicines));

            CreateMap<WareHouseArea, ReadWareHouseAreaDTO>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.Name));

            CreateMap<ReadWareHouseAreaDTO, WareHouseArea>()
                .ForMember(dest => dest.Area, opt => opt.Ignore());

            CreateMap<WareHouseMedicien, WarehouseMedicineDto>()
                .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.MedicineId))
                .ForMember(dest => dest.EnglishMedicineName, opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.ArabicMedicineName, opt => opt.MapFrom(src => src.Medicine.ArabicName))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Medicine.Price))
                .ForMember(dest => dest.Drug, opt => opt.MapFrom(src => src.Medicine.Drug))
                .ForMember(dest => dest.MedicineUrl, opt => opt.MapFrom(src => src.Medicine.MedicineUrl))
                .ForMember(dest => dest.Finalprice, opt => opt.MapFrom(src => src.Medicine.Price  - (src.Medicine.Price * (src.Discount / 100))))


                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ReverseMap();

            CreateMap<WareHouse, SimpleReadWarehouseDTO>()
                .ForMember(dest => dest.MinmumPrice, opt =>
                    opt.MapFrom((src, dest, destMember, context) =>
                        src.WareHouseAreas
                            .FirstOrDefault(a => a.AreaId == (int)context.Items["areaId"])?.MinmumPrice ?? 0
                    ));

            #endregion

            #region Pharmacy Mappings
            CreateMap<Pharmacy, PharmacyLoginResponseDTO>();
            CreateMap<PharmacyRegisterDto, Pharmacy>();
            CreateMap<Pharmacy, PharmacyDto>();
            #endregion

            #region Representative Mappings
            CreateMap<Representative, GetAllRepresentatitveDto>();
            CreateMap<Representative, GetRepresentativeByIdDto>();
            CreateMap<CreateRepresentativeDto, Representative>();
            CreateMap<UpdateRepresentativeDto, Representative>();
            CreateMap<Representative, RepresentativeInfoDto>();
            CreateMap<Representative, OrderDto>();

            CreateMap<Representative, GetRepresentatitvePharmaciesCountDto>()
                .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RepresentativeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.pharmacies.FirstOrDefault().UserName))
                .ForMember(dest => dest.PharmaciesCount, opt => opt.MapFrom(src => src.pharmacies.Count))
                .ForMember(dest => dest.Pharmacies, opt => opt.MapFrom(src => src.pharmacies));
            //problem
            CreateMap<Representative, GetOrdersPharmaciesCountDto>()
                 .ForMember(dest => dest.PharmaciesName, opt => opt.MapFrom(src => src.pharmacies.FirstOrDefault().Name))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.pharmacies.FirstOrDefault().UserName))
                .ForMember(dest => dest.PharmaciesAddress, opt => opt.MapFrom(src => src.pharmacies.FirstOrDefault().Address))
                .ForMember(dest => dest.PharmaciesGovernate, opt => opt.MapFrom(src => src.pharmacies.FirstOrDefault().Governate))
                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.pharmacies.Sum(p => p.Orders.Count)));

            #endregion

            #region Pharmacy
            CreateMap<PharmacyRegisterDto, Pharmacy>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Pharmacy, PharmacyInfoDto>();
            #endregion

            #region Governate
            CreateMap<Governate, GovernateLookupDto>();
            #endregion

            #region Area
            CreateMap<Area, AreaLookupDto>();
            #endregion

            #region Warehouse
            CreateMap<WareHouse, WarehouseInfoDto>();
            #endregion

            #region Medicine Mappings 
            CreateMap<Medicine, OrderDetailDto>()
               .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Name));
            #endregion
        }
    }
}
