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
    [Route("api/customers")]
    
    public class CustomersController : ControllerBase
    {
        private DataContext _dataContext;

        public CustomersController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var customersToReturn = _dataContext
                .Customers
                .Select(x => new CustomerGetDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .ToList();

            response.Data = customersToReturn;

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            if (id <= 0)
            {
                response.AddError("id", "Id cannot be lass than or equal to 0.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var customerFromDatabase = _dataContext
                .Customers
                .FirstOrDefault(x => id == x.Id);

            if (customerFromDatabase == null)
            {
                response.AddError("id", "Customer not found");

                return NotFound(response);
            }

            var customerToReturn = new CustomerGetDto
            {
                Id = customerFromDatabase.Id,
                FirstName = customerFromDatabase.FirstName,
                LastName = customerFromDatabase.LastName
            };

            response.Data = customerToReturn;

            return Ok(response);
        }
    }
}