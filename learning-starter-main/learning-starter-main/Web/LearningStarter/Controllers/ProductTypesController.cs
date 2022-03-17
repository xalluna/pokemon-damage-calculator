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
    [Route("api/product-types")]
    
    public class ProductTypesController : ControllerBase
    {
        private DataContext _dataContext;
        
        public ProductTypesController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var productTypesToReturn = _dataContext
                .ProductTypes
                .Select(x => new ProductTypeGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = productTypesToReturn;

            return Ok(response);
        }

        //  /api/product-type/"id"
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            if (id <= 0)
            {
                response.AddError("id", "Cannot be less than or equal to 0.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var productTypeFromDatabase = _dataContext
                .ProductTypes
                .FirstOrDefault(x => x.Id == id);

            if (productTypeFromDatabase == null)
            {
                response.AddError("id", "No Product Type found.");
                return NotFound(response);
            }

            var productTypeToReturn = new ProductTypeGetDto
            {
                Id = productTypeFromDatabase.Id,
                Name = productTypeFromDatabase.Name
            };

            response.Data = productTypeToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductTypeCreateDto productTypeCreateDto)
        {
            var response = new Response();

            if(productTypeCreateDto == null)
            {
                response.AddError("", "Entity cannot be null.");

                return BadRequest(response);
            }
            
            if(string.IsNullOrEmpty(productTypeCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty.");
            }

            var databaseHasName = _dataContext
                .ProductTypes
                .Any(x => x.Name == productTypeCreateDto.Name);

            if(databaseHasName)
            {
                response.AddError("Name", "Name already exists.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var productTypeToCreate = new ProductType
            {
                Name = productTypeCreateDto.Name
            };

            _dataContext.Add(productTypeToCreate);
            _dataContext.SaveChanges();

            return Created(String.Empty, response);
        }
    }
}