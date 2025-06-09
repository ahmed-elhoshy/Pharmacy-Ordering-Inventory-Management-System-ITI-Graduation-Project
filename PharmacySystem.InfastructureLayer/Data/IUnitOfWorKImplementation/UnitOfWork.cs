using E_Commerce.DomainLayer.Interfaces;
using E_Commerce.InfrastructureLayer.Data.DBContext;
using E_Commerce.InfrastructureLayer.Data.GenericClass;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace E_Commerce.DomainLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        #region DBContext
        private readonly PharmaDbContext context;
        private IMedicinRepository _MedicinRepository;
        #endregion

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
