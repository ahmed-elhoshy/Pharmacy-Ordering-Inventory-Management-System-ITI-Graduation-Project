using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.ApplicationLayer.DTOs.Medicines;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Interfaces;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class MedicineService
    {
        private readonly IUnitOfWork unitOfWork;

        public MedicineService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<List<MedicinesbyAreaIdDto>> GetMedicineStatsByAreaAsync(int areaId)
        {
            var medicines = await unitOfWork.medicineRepository.GetMedicinesByAreaAsync(areaId);

            var result = medicines
                .Select(m =>
                {
                    var warehouseEntries = m.WareHouseMedicines
                        .Where(wm => wm.WareHouse.WareHouseAreas.Any(wa => wa.AreaId == areaId))
                        .ToList();

                    var totalQuantity = warehouseEntries.Sum(wm => wm.Quantity);
                    var distributorsCount = warehouseEntries.Select(wm => wm.WareHouseId).Distinct().Count();

                    var maxDiscountEntry = warehouseEntries
                        .OrderByDescending(wm => wm.Discount)
                        .FirstOrDefault();

                    decimal minimumPricePerOrder = maxDiscountEntry.WareHouse.WareHouseAreas.Min(wa => wa.MinmumPrice);

                    return new MedicinesbyAreaIdDto
                    {
                        MedicineId = m.Id,
                        MedicineName = m.ArabicName,
                        Price = m.Price,
                        ImageUrl = m.MedicineUrl,
                        TotalQuantity = totalQuantity,
                        DistributorsCount = distributorsCount,
                        MaximumwareHouseAreaName = maxDiscountEntry.WareHouse.Address,
                        WarehouseIdOfMaxDiscount = maxDiscountEntry?.WareHouseId ?? 0,
                        WarehouseNameOfMaxDiscount = maxDiscountEntry?.WareHouse?.Name,
                        QuantityInWarehouseWithMaxDiscount = maxDiscountEntry?.Quantity ?? 0,
                        MaximumDiscount = maxDiscountEntry?.Discount ?? 0,
                        MinmumPrice = minimumPricePerOrder,
                        finalPrice = (maxDiscountEntry?.Medicine?.Price ?? m.Price) * (1 - (maxDiscountEntry?.Discount ?? 0) / 100),
                        Drug = m.Drug
                    };
                })
                .OrderBy(dto => dto.MedicineName).ThenBy(dto => dto.Price)
                .ToList();

            return result;
        }

        public async Task<PaginatedResult<MedicinesbyAreaIdDto>> GetMedicineStatsByAreaAsync(int areaId, int page , int pageSize )
        {
            var paginatedResult = await unitOfWork.medicineRepository
                .GetMedicinesByAreaAsync(areaId, page, pageSize);

            var resultDtos = paginatedResult.Items.Select(m =>
            {
                var warehouseEntries = m.WareHouseMedicines
                    .Where(wm => wm.WareHouse.WareHouseAreas.Any(wa => wa.AreaId == areaId))
                    .ToList();

                var totalQuantity = warehouseEntries.Sum(wm => wm.Quantity);
                var distributorsCount = warehouseEntries.Select(wm => wm.WareHouseId).Distinct().Count();

                var maxDiscountEntry = warehouseEntries
                    .OrderByDescending(wm => wm.Discount)
                    .FirstOrDefault();

                decimal minPrice = maxDiscountEntry?.WareHouse?.WareHouseAreas.Min(wa => wa.MinmumPrice) ?? 0;

                return new MedicinesbyAreaIdDto
                {
                    MedicineId = m.Id,
                    MedicineName = m.Name,
                    Price = m.Price,
                    ImageUrl = m.MedicineUrl,
                    TotalQuantity = totalQuantity,
                    DistributorsCount = distributorsCount,
                    MaximumwareHouseAreaName = maxDiscountEntry?.WareHouse?.Address,
                    WarehouseIdOfMaxDiscount = maxDiscountEntry?.WareHouseId ?? 0,
                    WarehouseNameOfMaxDiscount = maxDiscountEntry?.WareHouse?.Name,
                    QuantityInWarehouseWithMaxDiscount = maxDiscountEntry?.Quantity ?? 0,
                    MaximumDiscount = maxDiscountEntry?.Discount ?? 0,
                    MinmumPrice = minPrice,
                    finalPrice = (maxDiscountEntry?.Medicine?.Price ?? m.Price) * (1 - (maxDiscountEntry?.Discount ?? 0) / 100),
                    ArabicMedicineName = m.ArabicName,
                    Drug = m.Drug
                };
            })
                .OrderBy(dto => dto.MedicineName).ThenBy(dto => dto.Price)
                .ToList();

            return new PaginatedResult<MedicinesbyAreaIdDto>
            {
                Items = resultDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = paginatedResult.TotalCount
            };
        }
         public async Task<PaginatedResult<MedicinesbyAreaIdDto>> GetMedicineStatsByAreaAsync(int areaId, int page, int pageSize, string searchTerm)
        {
            var paginatedResult = await unitOfWork.medicineRepository
                .SearchMedicinesByAreaAndNameAsync(areaId, page, pageSize,searchTerm);

            var resultDtos = paginatedResult.Items.Select(m =>
            {
                var warehouseEntries = m.WareHouseMedicines
                    .Where(wm => wm.WareHouse.WareHouseAreas.Any(wa => wa.AreaId == areaId))
                    .ToList();

                var totalQuantity = warehouseEntries.Sum(wm => wm.Quantity);
                var distributorsCount = warehouseEntries.Select(wm => wm.WareHouseId).Distinct().Count();

                var maxDiscountEntry = warehouseEntries
                    .OrderByDescending(wm => wm.Discount)
                    .FirstOrDefault();

                decimal minPrice = maxDiscountEntry?.WareHouse?.WareHouseAreas.Min(wa => wa.MinmumPrice) ?? 0;

                return new MedicinesbyAreaIdDto
                {
                    MedicineId = m.Id,
                    MedicineName = m.Name,
                    Price = m.Price,
                    ImageUrl = m.MedicineUrl,
                    TotalQuantity = totalQuantity,
                    DistributorsCount = distributorsCount,
                    MaximumwareHouseAreaName = maxDiscountEntry?.WareHouse?.Address,
                    WarehouseIdOfMaxDiscount = maxDiscountEntry?.WareHouseId ?? 0,
                    WarehouseNameOfMaxDiscount = maxDiscountEntry?.WareHouse?.Name,
                    QuantityInWarehouseWithMaxDiscount = maxDiscountEntry?.Quantity ?? 0,
                    MaximumDiscount = maxDiscountEntry?.Discount ?? 0,
                    MinmumPrice = minPrice,
                    finalPrice = (maxDiscountEntry?.Medicine?.Price ?? m.Price) * (1 - (maxDiscountEntry?.Discount ?? 0) / 100),
                    ArabicMedicineName = m.ArabicName,
                    Drug = m.Drug
                };
            })
                  .OrderBy(dto => dto.MedicineName).ThenBy(dto => dto.Price).ToList();

            return new PaginatedResult<MedicinesbyAreaIdDto>
            {
                Items = resultDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = paginatedResult.TotalCount
            };
        }


    }
}
