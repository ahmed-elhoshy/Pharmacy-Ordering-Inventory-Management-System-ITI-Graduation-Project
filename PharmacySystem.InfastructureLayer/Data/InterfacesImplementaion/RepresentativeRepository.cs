using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class RepresentativeRepository : GenericRepository<Representative>, IRepresentativeRepository
    {
        #region Context
        private readonly PharmaDbContext context;
        public RepresentativeRepository(PharmaDbContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        public IQueryable<Representative> GetCountOfPharmaciesWithRepresentativeId(int RepresentativeId)
        {
            var getCount = context.Representatives.Include(P => P.pharmacies).Where(x => x.Id == RepresentativeId);
            return getCount;
        }

        public IQueryable<Representative> GetCountOfPharmaciesWithRepresentativeCode(string RepresentativeCode)
        {
            var getCount = context.Representatives.Include(P => P.pharmacies).Where(x => x.Code == RepresentativeCode);
            return getCount;
        }

        public IQueryable<Representative> GetCountOfOrders(int RepresentativeId)
        {
            var getCount = context.Representatives.Include(P => P.pharmacies)
                .ThenInclude(O => O.Orders).Where(x => x.Id == RepresentativeId);
            return getCount;
        }

        public async Task<bool> IsCodeExistsAsync(string code)
        {
            return await context.Representatives.AnyAsync(r => r.Code == code);
        }
        public async Task<Representative?> FindByEmailAsync(string email)
        {
            return await context.Set<Representative>()
                .FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
