using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersByRepresentativeIdAsync(int representativeId);
        Task<List<Order>> GetOrdersByRepresentativeIdIncludingPharmicesAsync(int representativeId);
        Task<IEnumerable<Order>> GetOrdersByWarehouseIdAsync (int warehouseId);
        Task<PaginatedResult<Order>> GetOrderByPharmacyIdAndStatus(int pharmacyId, int page, int pageSize, OrderStatus? status = null);

    }
}
