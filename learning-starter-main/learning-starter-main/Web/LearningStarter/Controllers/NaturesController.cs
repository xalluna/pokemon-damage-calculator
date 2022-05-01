using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController] 
    [Route("api/natures")]
    public class NaturesController : ControllerBase
    {
        private DataContext _dataContext;
        
        public NaturesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var naturesToReturn = _dataContext
                .Natures
                .Select(x => new NatureGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = naturesToReturn;
            
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

            var natureFromDatabase = _dataContext
                .Natures
                .FirstOrDefault(x => x.Id == id);

            if (natureFromDatabase == null)
            {
                response.AddError("Id", "Nature not found");
                return NotFound(response);
            }

            var natureToReturn = new NatureGetDto
            {
                Id = natureFromDatabase.Id,
                Name = natureFromDatabase.Name
            };

            response.Data = natureToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] NatureCreateDto nature)
        {
            var response = new Response();

            if (nature == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }

            nature.Name = nature.Name.Trim();
            if (string.IsNullOrEmpty(nature.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Natures
                .Any(x => x.Name == nature.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var natureToCreate = new Nature
            {
                Name = nature.Name
            };

            _dataContext.Add(natureToCreate);
            _dataContext.SaveChanges();
            
            var natureToGet = new NatureGetDto
            {
                Id = natureToCreate.Id,
                Name = natureToCreate.Name
            };

            response.Data = natureToGet;

            return Created("Nature created", response);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit([FromRoute] int id,
            [FromBody] NatureUpdateDto nature)
        {
            var response = new Response();

            if (nature == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }

            nature.Name = nature.Name.Trim();
            if (string.IsNullOrEmpty(nature.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Natures
                .Any(x => x.Name.ToLower() == nature.Name.ToLower() && x.Id != id);
            
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var natureToUpdate = _dataContext
                .Natures
                .FirstOrDefault(x => x.Id == id);

            natureToUpdate.Name = nature.Name;
            _dataContext.SaveChanges();

            var natureGet = new NatureGetDto
            {
                Name = nature.Name
            };

            response.Data = natureGet;

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var nature = _dataContext.Natures.FirstOrDefault(x => x.Id == id);

            if (nature == null)
            {
                response.AddError("id", "Nature not found.");
                return NotFound(response);
            }

            _dataContext.Natures.Remove(nature);
            _dataContext.SaveChanges();

            return Ok(response);
        }
    }
}