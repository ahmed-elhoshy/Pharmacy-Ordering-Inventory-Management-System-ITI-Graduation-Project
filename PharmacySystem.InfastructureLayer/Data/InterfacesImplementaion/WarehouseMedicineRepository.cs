using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
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
    public class WarehouseMedicineRepository : GenericRepository<WareHouseMedicien>, IWarehouseMedicineRepository
    {
        private readonly PharmaDbContext context;
        public WarehouseMedicineRepository(PharmaDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<WareHouseMedicien?> GetWarehouseMedicineAsync(int warehouseId, int medicineId)
        {
            return await context.WareHouseMediciens.FirstOrDefaultAsync(p => p.WareHouseId == warehouseId && p.MedicineId == medicineId);
        }
    }
}
