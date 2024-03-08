using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class ProductDtoUpdateFormData
    {
        [Required]
        [MaxLength(280, ErrorMessage = "Description cannot be over 280 characters")]
        public string Name { get; set; } = string.Empty;
        public IFormFile? Img { get; set; }
        [Required]
        [Range(0.001, 1000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 1000000000)]
        public int Quantity { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Description mut be 5 characters")]
        [MaxLength(280, ErrorMessage = "Description cannot be over 280 characters")]
        public string Description { get; set; } = string.Empty;
        public int categoryId { get; set; } 
    }
}