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

        private IMedicinRepository _MedicineRepository;
        private IRepresentativeRepository _representativeRepository;
        private IPharmacyRepository _pharmacyRepository;
        private IGenericRepository<Governate> _governateRepository;
        private IAreaRepository _areaRepository;
        public IMedicinRepository medicineRepository
        {
            get
            {
                if (_MedicineRepository == null)
                    _MedicineRepository = new MedicineRepository(context);
                return _MedicineRepository;
            }
            set => _MedicineRepository = value;
        }
        public IRepresentativeRepository representativeRepository
        {
            get
            {
                if (_representativeRepository == null)
                    _representativeRepository = new RepresentativeRepository(context);
                return _representativeRepository;
            }
            set => representativeRepository = value;
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

        public IAreaRepository AreaRepository
        {
            get
            {
                if (_areaRepository == null)
                    _areaRepository = new AreaRepository(context);
                return _areaRepository;
            }
            set => _areaRepository = value;
        }



        public IGenericRepository<Governate> GovernateRepository
        {
            get
            {
                if (_governateRepository == null)
                    _governateRepository = new GenericRepository<Governate>(context);
                return _governateRepository;
            }
            set => _governateRepository = value;
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
