using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.File;
using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IFileService _fileService;
        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, IFileService fileService)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _fileService = fileService;
        }

        // [Route("upload")]
        // [HttpPost]
        // public IActionResult UploadFile([FromForm] FileDto fileDto)
        // {
        //     var dbPath = _fileService.UploadImage(fileDto, "products");
        //     return Ok(new {dbPath});
        // } 

        [HttpGet]
        public async Task<IActionResult> GetAll()    
        {
            var products = await _productRepo.GetAllAsync();
            var productDto = products.Select(p => p.ToProducDto()).ToList();
            return Ok(productDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)    
        {
            var product = await _productRepo.GetByIdAsync(id);
            if(product == null)
                return NotFound("Product not found");

            return Ok(product.ToProducDto());
        }

        [HttpPost("{categoryId:int}")]
        public async Task<IActionResult> Create([FromForm] ProductDtoRequestFormData productDto, [FromRoute] int categoryId)    
        {
            if(!await _categoryRepo.CategoryExists(categoryId))
                return BadRequest("Category does not exists");
            
            if (productDto.Img == null)
                return BadRequest("Img does not exists");

            var dbPath = _fileService.UploadImage(productDto.Img!, "products");

            var productForm = new ProductDtoRequest{
                Name = productDto.Name,
                Img = dbPath!,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                Description = productDto.Description,
            };
            var product = productForm.ToProductFormCreateDTO(categoryId);
            
            await _productRepo.CreateAsync(product);
            return CreatedAtAction(
                nameof(GetById),
                new {id = product.Id},
                product.ToProducDto()
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] ProductDtoUpdateFormData productDto)    
        {
            if (productDto.Img == null)
                return BadRequest("Img does not exists");

            var product = await _productRepo.UpdateAsync(id, productDto);
            
            if(product == null)
                return NotFound("Product or Category does not exists");
            
            return Ok(product.ToProducDto());
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