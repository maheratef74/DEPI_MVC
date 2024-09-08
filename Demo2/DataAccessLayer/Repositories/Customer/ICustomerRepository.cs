using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
    }
}
