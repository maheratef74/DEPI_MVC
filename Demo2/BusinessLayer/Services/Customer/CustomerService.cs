using BusinessLayer.DTOs;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<GetAllCustomerDto>> GetAll()
        {
            var customersFromDB = await _customerRepository.GetAll();

            var customers = customersFromDB
                .Select(customer => (GetAllCustomerDto)customer)
                .ToList();

            return customers;
        }
    }
}
