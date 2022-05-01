using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/types")]
    public class TypesController : ControllerBase
    {
        private DataContext _dataContext;

        public TypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var typesToReturn = _dataContext
                .Types
                .Select(x => new TypeGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = typesToReturn;

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

            var typeFromDatabase = _dataContext
                .Types
                .FirstOrDefault(x => x.Id == id);

            if (typeFromDatabase == null)
            {
                response.AddError("Id", "PType not found");
                return NotFound(response);
            }

            var typeToReturn = new TypeGetDto
            {
                Id = typeFromDatabase.Id,
                Name = typeFromDatabase.Name
            };

            response.Data = typeToReturn;

            return Ok(response);
        }
        
        [HttpPost]
        public IActionResult Create(
            [FromBody] TypeCreateDto typeCreateDto)
        {
            var response = new Response();

            if (typeCreateDto == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }
            
            if (string.IsNullOrEmpty(typeCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Types
                .Any(x => x.Name == typeCreateDto.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var typeToCreate = new PType
            {
                Name = typeCreateDto.Name
            };

            _dataContext.Add(typeToCreate);
            _dataContext.SaveChanges();
            
            var typeToGet = new TypeGetDto
            {
                Id = typeToCreate.Id,
                Name = typeToCreate.Name
            };

            response.Data = typeToGet;

            return Created("Type created", response);
        }
        
        [HttpPut("{id:int}")]
        public IActionResult Edit([FromRoute] int id,
            [FromBody] TypeUpdateDto type)
        {
            var response = new Response();

            if (type == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }

            type.Name = type.Name.Trim();
            if (string.IsNullOrEmpty(type.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Types
                .Any(x => x.Name.ToLower() == type.Name.ToLower() && x.Id != id);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var typeToUpdate = _dataContext
                .Types
                .FirstOrDefault(x => x.Id == id);

            typeToUpdate.Name = type.Name;
            _dataContext.SaveChanges();

            var typeGet = new TypeGetDto
            {
                Name = type.Name
            };

            response.Data = typeGet;

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var type = _dataContext.Types.FirstOrDefault(x => x.Id == id);

            if (type == null)
            {
                response.AddError("id", "Ability not found.");
                return NotFound(response);
            }

            _dataContext.Types.Remove(type);
            _dataContext.SaveChanges();

            return Ok(response);
        }
    }
}