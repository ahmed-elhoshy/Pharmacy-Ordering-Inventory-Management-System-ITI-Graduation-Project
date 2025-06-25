using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersByRepresentativeIdAsync(int representativeId);
        Task<List<Order>> GetOrdersByRepresentativeIdIncludingPharmicesAsync(int representativeId);
        Task<IEnumerable<Order>> GetOrdersByWarehouseIdAsync (int warehouseId);
        Task<IEnumerable<Order>> GetOrderByPharmacyId(int pharmacyId);
        Task<IEnumerable<Order>> GetOrderByPharmacyIdAndStatus(int pharmacyId,OrderStatus status);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsById(int orderId);

    }
}
