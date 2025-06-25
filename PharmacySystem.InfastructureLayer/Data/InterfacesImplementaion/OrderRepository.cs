using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class OrderRepository : GenericRepository<Order> ,  IOrderRepository
    {
        #region context
        private readonly PharmaDbContext context;
        public OrderRepository(PharmaDbContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        public async Task<IEnumerable<Order>> GetAllOrdersByRepresentativeIdAsync(int representativeId)
        {
            return await context.Orders.AsNoTracking()
                .Where(o => o.Pharmacy.RepresentativeId == representativeId)
                .Include(o => o.Pharmacy)
                .Include(o => o.WareHouse)
                .Include(o => o.OrderDetails).ThenInclude(od => od.Medicine)
                .ToListAsync();
        }
        public async Task<List<Order>> GetOrdersByRepresentativeIdIncludingPharmicesAsync(int representativeId)
        {
            return await context.Orders.AsNoTracking()
                .Where(o => o.Pharmacy.RepresentativeId == representativeId)
                .Include(o => o.Pharmacy)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByWarehouseIdAsync(int warehouseId)
        {
            return await context.Orders.AsNoTracking().Where(p => p.WareHouseId == warehouseId)
                .Include(o => o.OrderDetails).ToListAsync();
        }


        public async Task<IEnumerable<Order>> GetOrderByPharmacyId(int pharmacyId)
        {
            return await context.Orders.Include(o => o.WareHouse)
                .Where(o => o.PharmacyId == pharmacyId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByPharmacyIdAndStatus(int pharmacyId, OrderStatus status)
        {
            return await context.Orders.Include(o => o.WareHouse)
                .Where(o => o.PharmacyId == pharmacyId&&o.Status==status).ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsById(int orderId)
        {
            return await context.OrderDetails.Include(od=>od.Medicine)
                .Where(od=>od.OrderId==orderId).ToListAsync();
        }
    }
}
