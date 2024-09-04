using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Cusomter")]
    public class Customer
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Column(nameof(Address))]
        public string Address { get; set; }
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; }
    }
}
