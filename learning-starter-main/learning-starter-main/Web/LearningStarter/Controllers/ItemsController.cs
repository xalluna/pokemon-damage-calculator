using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private DataContext _dataContext;

        public ItemsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var itemsToReturn = _dataContext
                .Items
                .Select(x => new ItemGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = itemsToReturn;

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var response = new Response();
            
            if (id <= 0)
            {
                response.AddError("Id", "Id cannot be less than oe equal to zero.");
                return BadRequest(response);
            }

            var itemFromDatabase = _dataContext
                .Items
                .FirstOrDefault(x => x.Id == id);

            if (itemFromDatabase == null)
            {
                response.AddError("Id", "Item not found");
                return NotFound(response);
            }

            var itemToReturn = new ItemGetDto
            {
                Id = itemFromDatabase.Id,
                Name = itemFromDatabase.Name
            };

            response.Data = itemToReturn;

            return Ok(response);
        }
        
        [HttpPost]
        public IActionResult Create(
            [FromBody] ItemCreateDto itemCreateDto)
        {
            var response = new Response();

            if (itemCreateDto == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }
            
            if (string.IsNullOrEmpty(itemCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Items
                .Any(x => x.Name == itemCreateDto.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var itemToCreate = new Item
            {
                Name = itemCreateDto.Name
            };

            _dataContext.Add(itemToCreate);
            _dataContext.SaveChanges();
            
            var itemToGet = new ItemGetDto
            {
                Id = itemToCreate.Id,
                Name = itemToCreate.Name
            };

            response.Data = itemToGet;

            return Created("Item created", response);
        }
    }
}