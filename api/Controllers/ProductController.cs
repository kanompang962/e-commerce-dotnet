using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("/api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUploadService _uploadService;
        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, IUploadService uploadService)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _uploadService = uploadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()    
        {
            var products = await _productRepo.GetAllAsync();
            var productDto = products.Select(p => p.ToProducDto()).ToList();
            return Ok(productDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)    
        {
            var product = await _productRepo.GetByIdAsync(id);

            if(product == null)
                return NotFound("Product not found");

            return Ok(product.ToProducDto());
        }

        [HttpPost("{categoryId:int}")]
        public async Task<IActionResult> Create([FromBody] ProductDtoRequest productDto, [FromRoute] int categoryId)    
        {
            if(!await _categoryRepo.CategoryExists(categoryId))
                return BadRequest("Category does not exists");
                
            var product = productDto.ToProductFormCreateDTO(categoryId);
            await _productRepo.CreateAsync(product);
            return CreatedAtAction(
                nameof(GetById),
                new {id = product.Id},
                product.ToProducDto()
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)    
        {
            var product = await _productRepo.DeleteAsync(id);
            if(product == null)
                return NotFound("Product not found");

            return Ok(product.ToProducDto());
        }

    }
}