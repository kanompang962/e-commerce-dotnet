using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo) 
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()    
        {
            var categorys = await _categoryRepo.GetAllAsync();
            var categoryDto = categorys.Select(c => c.ToCategoryDto()).ToList();
            return Ok(categoryDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)    
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if(category == null)
                return NotFound("Category does not exists");
            
            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDtoRequest categoryDto)
        {
            var category = categoryDto.ToProductFormCreateDTO();
            await _categoryRepo.CreateAsync(category);
            return CreatedAtAction(
                nameof(GetById),
                new {id = category.Id},
                category.ToCategoryDto()
            );
        }
    }
}