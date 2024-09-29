using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.SP_Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.GenericProduct
{
    public interface IGenericProductRepository : IGenericRepository<Product>
    {
        Task<PagedList<SP_GetProducts_Model>> Get_StoredProc(int pageNumber,
            int pageSize,
            string sortBy,
            string searchTerm);
    }
}
