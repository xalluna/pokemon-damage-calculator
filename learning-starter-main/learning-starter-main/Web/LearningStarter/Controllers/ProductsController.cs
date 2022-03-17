using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/products")]
    
    public class ProductsController : ControllerBase
    {
        private DataContext _dataContext;

        public ProductsController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var productsToReturn = _dataContext
                .Products
                .Select(x => new ProductGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductTypeId = x.ProductTypeId,
                    Price = x.Price
                })
                .ToList();

            response.Data = productsToReturn;

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var response = new Response();

            if (id <= 0)
            {
                response.AddError("id", "Id cannot be less than or equal to 0.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var productToReturn = _dataContext
                .Products
                .FirstOrDefault(x=> id == x.Id);

            if (productToReturn == null)
            {
                response.AddError("id", "Product not found.");
                return NotFound(response);
            }

            response.Data = new ProductGetDto
            {
                Name = productToReturn.Name,
                ProductTypeId = productToReturn.ProductTypeId,
                Price = productToReturn.Price
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateDto productCreateDto)
        {
            var response = new Response();

            if(productCreateDto == null)
            {
                response.AddError("", "Entity cannot be null.");
                return BadRequest(response);
            }

            if(string.IsNullOrEmpty(productCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty.");
            }

            var databaseHasName = _dataContext
                .Products
                .Any(x => x.Name == productCreateDto.Name);

            if (databaseHasName)
            {
                response.AddError("Name", "Name already exists.");
            }

            var isValidId = _dataContext
                .ProductTypes
                .Any(x => x.Id == productCreateDto.ProductTypeId);

            if(!isValidId)
            {
                response.AddError("ProductTypeId", "Product Type Id not found.");
            }

            if (productCreateDto.Price < 0)
            {
                response.AddError("Price", "Price cannot be negative.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }
            
            var productToCreate = new Product
            {
                Name = productCreateDto.Name,
                ProductTypeId = productCreateDto.ProductTypeId,
                Price = productCreateDto.Price
            };

            response.Data = productToCreate;
            
            _dataContext.Add(productToCreate);
            _dataContext.SaveChanges();

            return Created(String.Empty, response);
        }
    }
}