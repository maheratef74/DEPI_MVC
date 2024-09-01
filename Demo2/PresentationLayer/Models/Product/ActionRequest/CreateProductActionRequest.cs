namespace PresentationLayer.Models
{
    public class CreateProductActionRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }
}
