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
    [Route("api/orders")]
    
    public class OrdersController : ControllerBase
    {
        private DataContext _dataContext;

        public OrdersController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var ordersToReturn = _dataContext
                .Orders
                .Select(x => new OrderGetDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    PreparationStepId = x.PreparationStepId,
                    CreatedDate = x.CreatedDate
                })
                .ToList();

            response.Data = ordersToReturn;

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
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

            var orderFromDatabase = _dataContext
                .Orders
                .FirstOrDefault(x => id == x.Id);

            if (orderFromDatabase == null)
            {
                response.AddError("id", "Order not found.");
                return NotFound(response);
            }

            var orderToReturn = new OrderGetDto
            {
                Id = orderFromDatabase.Id,
                CustomerId = orderFromDatabase.CustomerId,
                PreparationStepId = orderFromDatabase.PreparationStepId,
                CreatedDate = orderFromDatabase.CreatedDate
            };

            response.Data = orderToReturn;

            return Ok(response);
        }
        
    }
}