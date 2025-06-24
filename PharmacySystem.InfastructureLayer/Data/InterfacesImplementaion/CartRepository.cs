using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        #region context
        private readonly PharmaDbContext context;
        public CartRepository(PharmaDbContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        public async Task<Cart?> GetCartWithDetailsByPharmacyIdAsync(int pharmacyId)
        {
            return await context.Carts.AsNoTracking().Include(c => c.CartWarehouses)
                .ThenInclude(w => w.CartItems).FirstOrDefaultAsync(c => c.PharmacyId == pharmacyId);
        }
    }
}
