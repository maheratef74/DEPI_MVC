using DataAccessLayer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Validators
{
    public class UniqueNameValidatorAttribute : ValidationAttribute
    {

        // No Injection ❌❌

        //private readonly IProductRepository _productRepository;

        //public UniqueNameValidatorAttribute(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService<IServiceProvider>();
            var _productRepository = serviceProvider.GetService<IProductRepository>();

            var product = _productRepository.FindProductByName(value.ToString());

            if (product != null)
            {
                return new ValidationResult("Product Name already exists");
            }

            return ValidationResult.Success;
            
        }
    }
}
