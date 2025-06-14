using E_Commerce.DomainLayer.Interfaces;
using E_Commerce.InfrastructureLayer.Data.DBContext;
using E_Commerce.InfrastructureLayer.Data.GenericClass;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;
using PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion;

namespace E_Commerce.DomainLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        #region DBContext
        private readonly PharmaDbContext context;
        #endregion

        private IMedicinRepository _MedicinRepository;
        private IRepresentitiveRepository _representitiveRepository ;
        public IMedicinRepository medicinRepository
        {
            get
            {
                if (_MedicinRepository == null)
                    _MedicinRepository = new MedicineRepository(context);
                return _MedicinRepository;
            }
            set => _MedicinRepository = value;
        }
        public IRepresentitiveRepository representitiveRepository 
        {
            get
            {
                if (_representitiveRepository == null)
                    _representitiveRepository = new RepresentitiveRepository(context);
                return _representitiveRepository;
            }
            set => representitiveRepository = value;
        }

        public UnitOfWork(PharmaDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0; // return true -> if at least one row was modified on Database  
        }
    }
}
