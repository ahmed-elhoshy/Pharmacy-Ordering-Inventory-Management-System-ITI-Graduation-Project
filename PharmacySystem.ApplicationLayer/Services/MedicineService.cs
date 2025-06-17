using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.ApplicationLayer.DTOs.Medicines;
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
                        MedicineName = m.Name,
                        Price = m.Price,
                        TotalQuantity = totalQuantity,
                        DistributorsCount = distributorsCount,
                        MaximumwareHouseAreaName = maxDiscountEntry.WareHouse.Address,
                        WarehouseIdOfMaxDiscount = maxDiscountEntry?.WareHouseId ?? 0,
                        WarehouseNameOfMaxDiscount = maxDiscountEntry?.WareHouse?.Name,
                        QuantityInWarehouseWithMaxDiscount = maxDiscountEntry?.Quantity ?? 0,
                        MaximumDiscount = maxDiscountEntry?.Discount ?? 0,
                        MinmumPrice = minimumPricePerOrder

                    };
                })
                .OrderBy(dto => dto.MedicineName).ThenBy(dto => dto.Price)
                .ToList();

            return result;
        }



    }
}
