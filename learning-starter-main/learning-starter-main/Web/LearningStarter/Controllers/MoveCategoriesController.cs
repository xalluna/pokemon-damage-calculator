using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/move-categories")]
    public class MoveCategoriesController : ControllerBase
    {
        private DataContext _dataContext;

        public MoveCategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var moveCategoriesToReturn = _dataContext
                .MoveCategories
                .Select(x => new MoveCategoryGetDto()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = moveCategoriesToReturn;

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

            var moveCategoryFromDatabase = _dataContext
                .MoveCategories
                .FirstOrDefault(x => x.Id == id);

            if (moveCategoryFromDatabase == null)
            {
                response.AddError("Id", "Move Category not found");
                return NotFound(response);
            }

            var moveCategoryToReturn = new MoveCategoryGetDto()
            {
                Id = moveCategoryFromDatabase.Id,
                Name = moveCategoryFromDatabase.Name
            };

            response.Data = moveCategoryToReturn;

            return Ok(response);
        }
        
        [HttpPost]
        public IActionResult Create(
            [FromBody] MoveCategoryCreateDto moveCategoryCreateDto)
        {
            var response = new Response();

            if (moveCategoryCreateDto == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }
            
            if (string.IsNullOrEmpty(moveCategoryCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .MoveCategories
                .Any(x => x.Name == moveCategoryCreateDto.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var moveCategoryToCreate = new MoveCategory
            {
                Name = moveCategoryCreateDto.Name
            };

            _dataContext.Add(moveCategoryToCreate);
            _dataContext.SaveChanges();
            
            var moveCategoryToGet = new MoveCategoryGetDto()
            {
                Id = moveCategoryToCreate.Id,
                Name = moveCategoryToCreate.Name
            };

            response.Data = moveCategoryToGet;

            return Created("Move Category created", response);
        }
    }
}