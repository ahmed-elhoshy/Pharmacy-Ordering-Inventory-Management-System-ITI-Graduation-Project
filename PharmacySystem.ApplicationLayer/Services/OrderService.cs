using E_Commerce.DomainLayer.Interfaces;
using PharmacySystem.ApplicationLayer.DTOs.Medicines;
using PharmacySystem.ApplicationLayer.DTOs.Orders;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
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
    }
}
