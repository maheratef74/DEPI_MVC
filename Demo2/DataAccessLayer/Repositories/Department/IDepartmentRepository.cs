using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department> FindDepartmentWithProducts(int id);
    }
}
