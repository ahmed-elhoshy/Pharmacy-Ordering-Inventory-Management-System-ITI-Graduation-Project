using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class CartWarehouseRepository : GenericRepository<CartWarehouse>, ICartWarehouseRepository
    {
        public CartWarehouseRepository(PharmaDbContext context) : base(context)
        {
        }
    }
}
