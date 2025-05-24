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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Update(Brand brand)
        {
            var ObjFromDb = await _dbContext.Brand.FirstOrDefaultAsync(x=>x.Id==brand.Id);
            if (ObjFromDb != null) { 
                ObjFromDb.Name = brand.Name;
                ObjFromDb.EstablishedYear = brand.EstablishedYear;
                if (ObjFromDb.BrandLogo != null)
                {
                    ObjFromDb.BrandLogo =  brand.BrandLogo;
                }
            }
            _dbContext.Update(brand);
            await _dbContext.SaveChangesAsync();

        }
    }
}
