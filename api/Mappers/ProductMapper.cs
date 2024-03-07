using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Product;
using api.Models;

namespace api.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProducDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Img = productModel.Img,
                Price = productModel.Price,
                Quantity = productModel.Quantity,
                Description = productModel.Description,
                CategoryId = productModel.CategoryId,
                Category = productModel.Category?.ToCategoryDto(),
            };
        }

        public static Product ToProductFormCreateDTO(this ProductDtoRequest productDto, int categoryId)
        {
            return new Product
            {
                Name = productDto.Name,
                Img = productDto.Img,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                Description = productDto.Description,
                CategoryId = categoryId
            };
        }
    }
}