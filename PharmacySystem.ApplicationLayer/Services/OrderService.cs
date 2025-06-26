using E_Commerce.DomainLayer.Interfaces;
using PharmacySystem.ApplicationLayer.DTOs.Medicines;
using PharmacySystem.ApplicationLayer.DTOs.Orders;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class OrderService : IOrderService
    {
        #region
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<List<OrderToWarehouseDto>> GetOrdersForWarehouseAsync(int warehouseId)
        {
            var orders = await _unitOfWork.orderRepository.GetOrdersByWarehouseIdAsync(warehouseId);

            return orders.Select(o => new OrderToWarehouseDto
            {
                OrderId = o.Id,
                TotalPrice = o.TotalPrice,
                Quantity = o.Quntity,
                Status = o.Status.ToString(),
                Medicines = o.OrderDetails.Select(d => new MedicineDto
                {
                    MedicineId = d.MedicineId,
                    Quantity = d.Quntity,
                    Price = d.Price
                }).ToList()
            }).ToList();
        }


        public async Task<PaginatedResult<OrderMedicineDto>> GetOrdersForPharmacyByStatus(int pharmacyId, int page, int pageSize, OrderStatus? status = null)
        {
            var orders = await _unitOfWork.orderRepository.GetOrderByPharmacyIdAndStatus(pharmacyId,page,pageSize, status);
          var results=orders.Items.Select(o => new OrderMedicineDto
            {
                
                WareHouseName = o.WareHouse.Name,
                Status = o.Status.ToString(),
                Quantity = o.Quntity,
                TotalPrice = o.TotalPrice,
                CreatedAt = o.CreatedAt,
                WareHouseImage = o.WareHouse.ImageUrl,
                Details = o.OrderDetails.Select(d => {
                    var originalTotal = d.Medicine.Price * d.Quntity;
                    var discountAmount = originalTotal - (d.Price * d.Quntity);
                    var discountPercentage = originalTotal != 0 ? (discountAmount / originalTotal) * 100 : 0;
                    return new OrderDetailsDto
                    {
                        MedicineName = d.Medicine.Name,
                        ArabicMedicineName = d.Medicine.ArabicName,
                        MedicineImage = d.Medicine.MedicineUrl,
                        MedicinePrice = d.Medicine.Price,
                        Quantity = d.Quntity,
                        TotalPriceBeforeDisccount = originalTotal,
                        TotalPriceAfterDisccount = d.Price * d.Quntity,
                        DiscountAmount =discountAmount,

                        discountPercentage = discountPercentage
                    };
                }).ToList()
            }).ToList();
            return new PaginatedResult<OrderMedicineDto>
            {
                Items = results,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = orders.TotalCount
            };
        }

    }
}
