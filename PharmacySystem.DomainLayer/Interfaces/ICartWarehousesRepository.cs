using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface ICartWarehousesRepository : IGenericRepository<CartWarehouse>
    {
    }
}
