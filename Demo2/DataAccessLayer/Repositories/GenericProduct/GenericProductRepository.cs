using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.GenericProduct
{
    public class GenericProductRepository : GenericRepository<Product>, IGenericProductRepository
    {
        
        public GenericProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
