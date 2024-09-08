using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class GetAllCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public static explicit operator GetAllCustomerDto(Customer customer)
        {
            return new GetAllCustomerDto()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
            };
        }
    }
}
