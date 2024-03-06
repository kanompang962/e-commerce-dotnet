using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMapper
    {
         public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
            };
        }

        public static Category ToProductFormCreateDTO(this CategoryDtoRequest categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
            };
        }
    }
}