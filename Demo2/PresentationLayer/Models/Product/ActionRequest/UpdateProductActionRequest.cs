namespace PresentationLayer.Models
{
    public class UpdateProductActionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }
}
