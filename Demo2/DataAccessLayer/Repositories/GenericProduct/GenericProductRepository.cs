using Dapper;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.SP_Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.GenericProduct
{
    public class GenericProductRepository : GenericRepository<Product>, IGenericProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #region EF Core Version
        //public async Task<PagedList<SP_GetProducts_Model>> Get_StoredProc(int pageNumber, int pageSize, string sortBy, string searchTerm)
        //{
        //    var totalCountParam = new SqlParameter
        //    {
        //        ParameterName = "@TotalCount",
        //        SqlDbType = System.Data.SqlDbType.Int,
        //        Direction = System.Data.ParameterDirection.Output
        //    };

        //    var products = await _dbContext.Products.FromSqlRaw("EXEC GetProducts @PageNumber, @PageSize,@SortBy,@SearchTerm,@TotalCount OUTPUT",
        //            new SqlParameter("@PageNumber", pageNumber),
        //            new SqlParameter("@PageNumber", pageNumber),
        //            new SqlParameter("@PageSize", pageSize),
        //            new SqlParameter("@SortBy", sortBy),
        //            new SqlParameter("@SearchTerm", searchTerm),
        //            totalCountParam).ToListAsync();

        //    return new PagedList<SP_GetProducts_Model>
        //    {
        //        Data = products.Select(p => new SP_GetProducts_Model
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Price = p.Price,
        //        }).ToList(),
        //        TotalCount = (int)totalCountParam.Value
        //    };


        //}
        #endregion

        #region Dapper Version
        public async Task<PagedList<SP_GetProducts_Model>> Get_StoredProc(int pageNumber, int pageSize, string sortBy, string searchTerm)
        {
            var totalCount = 0;

            using (var connection = new SqlConnection("Server=localhost\\MSSQLSERVER01;Database=Ecommerce_System;Trusted_Connection=True; Integrated Security = True; TrustServerCertificate = True;"))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PageNumber", pageNumber);
                dynamicParameters.Add("@PageSize", pageSize);
                dynamicParameters.Add("@SortBy", sortBy);
                dynamicParameters.Add("@SearchTerm", searchTerm);
                dynamicParameters.Add("@TotalCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var products = await connection.QueryAsync<SP_GetProducts_Model>(
                    "GetProducts",
                    dynamicParameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new PagedList<SP_GetProducts_Model>
                {
                    TotalCount = dynamicParameters.Get<int>("@TotalCount"),
                    Data = products.ToList()
                };
            }

            //var products = await SocketsHttpConnectionContext.

            //return new PagedList<SP_GetProducts_Model>
            //{
            //    Data = products.Select(p => new SP_GetProducts_Model
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Price = p.Price,
            //    }).ToList(),
            //    TotalCount = (int)totalCountParam.Value
            //};


        }
        #endregion
    }

}
