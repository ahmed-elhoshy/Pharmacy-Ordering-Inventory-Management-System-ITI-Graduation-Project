using E_Commerce.DomainLayer.Interfaces;
using PharmacySystem.ApplicationLayer.DTOs.Medicines;
using PharmacySystem.ApplicationLayer.DTOs.Orders;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
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

        public async Task<List<OrderMedicineDto>> GetOrdersForPharmacy(int pharmacyId)
        {
            var orders = await _unitOfWork.orderRepository.GetOrderByPharmacyId(pharmacyId);
            return orders.Select(o => new OrderMedicineDto
            {
                OrderId = o.Id,
                WareHouseName = o.WareHouse.Name,
                Status = o.Status.ToString(),
                Quantity = o.Quntity,
                TotalPrice = o.TotalPrice,
                CreatedAt = o.CreatedAt,
                WareHouseImage=o.WareHouse.ImageUrl
            }).ToList();
        }

        public async Task<List<OrderMedicineDto>> GetOrdersForPharmacyByStatus(int pharmacyId, OrderStatus status)
        {
            var orders = await _unitOfWork.orderRepository.GetOrderByPharmacyIdAndStatus(pharmacyId,status);
            return orders.Select(o => new OrderMedicineDto
            {
                OrderId = o.Id,
                WareHouseName = o.WareHouse.Name,
                Status = o.Status.ToString(),
                Quantity = o.Quntity,
                TotalPrice = o.TotalPrice,
                CreatedAt = o.CreatedAt,
                WareHouseImage = o.WareHouse.ImageUrl
            }).ToList();
        }

        public async Task<List<OrderDetailsDto>> GetOrdersDetails(int orderId)
        {
           var details=await _unitOfWork.orderRepository.GetOrderDetailsById(orderId);
            return details.Select(o =>
            {
                var originalTotal = o.Medicine.Price * o.Quntity;
                var discountAmount = originalTotal - o.Price;

                return new OrderDetailsDto
                {
                    MedicineName = o.Medicine.Name,
                    ArabicMedicineName = o.Medicine.ArabicName,
                    MedicineImage=o.Medicine.MedicineUrl,
                    Quantity = o.Quntity,
                    TotalPriceAfterDisccount = o.Price,
                    TotalPriceBeforeDisccount = o.Medicine.Price*o.Quntity,
                    MedicinePrice = o.Medicine.Price,
                    DiscountAmount = discountAmount,
                    
                };
            }).ToList();

        }
    }
}
