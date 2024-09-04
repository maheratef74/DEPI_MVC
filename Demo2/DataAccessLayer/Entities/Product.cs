using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        // Eager Loading  ==> Navigation properteis are loaded
        // Lazy  Loading  ==> Navigation properties are not loaded

    }
}
