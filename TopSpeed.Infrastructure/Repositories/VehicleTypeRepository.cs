using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Data;

namespace TopSpeed.Infrastructure.Repositories
{
    public class VehicleTypeRepository : GenericRepository<VehicleType>, IVehicleTypeRepository
    {
       
        public VehicleTypeRepository(ApplicationDbContext  dbContext) :base(dbContext) 
        {
           
        }

        public async Task update(VehicleType vehicleType)
        {
            var ObjFromDb  =  await _dbContext.VehicleTypes.FirstOrDefaultAsync(x=>x.Id == vehicleType.Id);
            if(ObjFromDb != null)
            {
                ObjFromDb.Name =  vehicleType.Name;

            }
            _dbContext.VehicleTypes.Update(vehicleType);
        }
    }
}
