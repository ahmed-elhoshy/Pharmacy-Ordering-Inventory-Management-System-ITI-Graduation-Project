using E_Commerce.DomainLayer.Interfaces;
using E_Commerce.InfrastructureLayer.Data.DBContext;
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using E_Commerce.InfrastructureLayer.Data.GenericClass;
using PharmacySystem.DomainLayer.Entities;
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
        private IPharmacyRepository _pharmacyRepository;
        private IGenericRepository<Area> _areaRepository;
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

        public IPharmacyRepository PharmacyRepository
        {
            get
            {
                if (_pharmacyRepository == null)
                    _pharmacyRepository = new PharmacyRepository(context);
                return _pharmacyRepository;
            }
            set => _pharmacyRepository = value;
        }
        public IGenericRepository<Area> AreaRepository
        {
            get
            {
                if (_areaRepository == null)
                    _areaRepository = new GenericRepository<Area>(context);
                return _areaRepository;
            }
            set => _areaRepository = value;
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
