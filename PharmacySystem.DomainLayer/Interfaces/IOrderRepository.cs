using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersByRepresentativeIdAsync(int representativeId);
        Task<List<Order>> GetOrdersByRepresentativeIdIncludingPharmicesAsync(int representativeId);
        Task<IEnumerable<Order>> GetOrdersByWarehouseIdAsync (int warehouseId);
    }
}
