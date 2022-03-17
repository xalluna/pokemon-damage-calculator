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
    [Route("api/order-products")]
    
    public class OrderProductsController : ControllerBase
    {
        private DataContext _dataContext;

        public OrderProductsController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var orderProductsToReturn = _dataContext
                .OrderProducts
                .Select(x => new OrderProductGetDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    OrderId = x.OrderId
                })
                .ToList();

            response.Data = orderProductsToReturn;

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

            var orderProductFromDatabase = _dataContext
                .OrderProducts
                .FirstOrDefault(x => id == x.Id);

            if (orderProductFromDatabase == null)
            {
                response.AddError("id", "Order Product not found.");
                return NotFound(response);
            }

            var orderProductToReturn = new OrderProductGetDto
            {
                Id = orderProductFromDatabase.Id,
                ProductId = orderProductFromDatabase.ProductId,
                OrderId = orderProductFromDatabase.OrderId
            };

            response.Data = orderProductToReturn;

            return Ok(response);
        }
        
    }
}