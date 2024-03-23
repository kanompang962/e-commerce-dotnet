using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal discountPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal rating { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}