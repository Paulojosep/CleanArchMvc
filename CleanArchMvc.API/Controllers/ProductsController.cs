using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var categories = await _productService.GetProducts();
            if (categories == null)
            {
                return NotFound("Products not found");
            }
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var category = await _productService.GetById(id);
            if (category == null)
            {
                return NotFound("Productnot found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO ProductDTO)
        {
            if (ProductDTO == null)
                return BadRequest("Invalid Data");

            await _productService.Add(ProductDTO);
            return new CreatedAtRouteResult("GetProduct", new { id = ProductDTO.Id }, ProductDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO ProductDTO)
        {
            if (id != ProductDTO.Id)
                return BadRequest("Data invalid");

            if (ProductDTO == null)
                return BadRequest("Data invalid");

            await _productService.Update(ProductDTO);
            return Ok(ProductDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var category = await _productService.GetById(id);
            if (category == null)
                return NotFound("Product not found");

            await _productService.Remove(id);
            return Ok(category);

        }
    }
}
