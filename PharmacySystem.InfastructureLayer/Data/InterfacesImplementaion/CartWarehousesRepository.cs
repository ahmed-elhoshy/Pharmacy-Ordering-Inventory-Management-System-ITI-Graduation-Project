using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;


namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class CartWarehousesRepository : GenericRepository<CartWarehouse>, ICartWarehousesRepository
    {
        public CartWarehousesRepository(PharmaDbContext context) : base(context)
        {
        }
    }
}
