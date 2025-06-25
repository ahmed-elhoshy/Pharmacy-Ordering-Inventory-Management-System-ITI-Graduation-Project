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
        private IMedicinRepository _MedicineRepository;
        private ICartWarehouseRepository _cartWarehouseRepository;
        private ICartItemRepository _cartItemRepository;
        private ICartWarehousesRepository _cartWarehousesRepository;
        private IWarehouseMedicineRepository _warehouseMedicineRepository;
        private IOrderRepository _orderRepository;
        private IRepresentativeRepository _representativeRepository;
        private IPharmacyRepository _pharmacyRepository;
        private IGenericRepository<Governate> _governateRepository;
        private IAreaRepository _areaRepository;
        private ICartRepository _cartRepository;
        //private IGenericRepository<Area> _areaRepository;
        private IAdminRepository _adminRepository;
        private IGenericRepository<WareHouseArea> _wareHouseAreaRepository;
        #endregion

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
            set => _representativeRepository = value;
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

        public IAdminRepository AdminRepository
        {
            get
            {
                if (_adminRepository == null)
                    _adminRepository = new AdminRepository(context);
                return _adminRepository;
            }
            set => _adminRepository = value;
        }

        public IOrderRepository orderRepository 
        {
            get
            {
                if(_orderRepository == null)
                    _orderRepository = new OrderRepository(context);
                return _orderRepository;
            }
            set => _orderRepository = value;
        }

        public ICartRepository cartRepository
        {
            get
            {
                if (_cartRepository == null)
                    _cartRepository = new CartRepository(context);
                return _cartRepository;
            }
            set => _cartRepository = value;
        }

        public IWarehouseMedicineRepository warehouseMedicineRepository 
        {
            get
            {
                if (_warehouseMedicineRepository == null)
                    _warehouseMedicineRepository = new WarehouseMedicineRepository(context);
                return _warehouseMedicineRepository;
            }
            set => _warehouseMedicineRepository = value;
        }

        public ICartWarehousesRepository cartWarehousesRepository 
        { 
            get 
            {
                if (_cartWarehousesRepository == null)
                    _cartWarehousesRepository = new CartWarehousesRepository(context);
                return _cartWarehousesRepository;
            }
            set => _cartWarehousesRepository = value; 
        }

        public ICartItemRepository cartItemRepository 
        { 
            get 
            {
                if (_cartItemRepository == null)
                    _cartItemRepository = new CartItemRepository(context);
                return _cartItemRepository;
            }
            set => _cartItemRepository = value;
        }

        public IGenericRepository<WareHouseArea> WareHouseAreaRepository
        {
            get
            {
                if (_wareHouseAreaRepository == null)
                    _wareHouseAreaRepository = new GenericRepository<WareHouseArea>(context);
                return _wareHouseAreaRepository;
            }
        }

        public ICartWarehouseRepository cartWarehouseRepository 
        {
            get
            {
                if (_cartWarehouseRepository == null)
                    _cartWarehouseRepository = new CartWarehouseRepository(context);
                return _cartWarehouseRepository;
            }
                set => _cartWarehouseRepository = value; 
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
