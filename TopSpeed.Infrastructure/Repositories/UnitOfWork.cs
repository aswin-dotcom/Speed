using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Infrastructure.Data;

namespace TopSpeed.Infrastructure.Repositories
{
    public class UnitOfWork : IunitOFWork
    {
        public IBrandRepository brand { get; private set; }

        public IVehicleTypeRepository vehicleType { get; private set; }

        protected readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            brand = new BrandRepository(_dbContext);
            vehicleType = new VehicleTypeRepository(_dbContext);


        }

        public void Dispose()
        {
            _dbContext.Dispose();

        }

        public async Task SaveAsync()


        {
             await _dbContext.SaveChangesAsync();
            
        }
    }
}
