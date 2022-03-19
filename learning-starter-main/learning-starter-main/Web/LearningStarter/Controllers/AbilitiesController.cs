using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController] 
    [Route("api/abilities")]
    public class AbilitiesController : ControllerBase
    {
        private DataContext _dataContext;
        
        public AbilitiesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var abilitiesToReturn = _dataContext
                .Abilities
                .Select(x => new AbilityGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = abilitiesToReturn;
            
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

            var abilityFromDatabase = _dataContext
                .Abilities
                .FirstOrDefault(x => x.Id == id);

            if (abilityFromDatabase == null)
            {
                response.AddError("Id", "Ability not found");
                return NotFound(response);
            }

            var abilityToReturn = new AbilityGetDto
            {
                Id = abilityFromDatabase.Id,
                Name = abilityFromDatabase.Name
            };

            response.Data = abilityToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] AbilityCreateDto abilityCreateDto)
        {
            var response = new Response();

            if (abilityCreateDto == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }
            
            if (string.IsNullOrEmpty(abilityCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Abilities
                .Any(x => x.Name == abilityCreateDto.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var abilityToCreate = new Ability
            {
                Name = abilityCreateDto.Name
            };

            _dataContext.Add(abilityToCreate);
            _dataContext.SaveChanges();
            
            var abilityToGet = new AbilityGetDto
            {
                Id = abilityToCreate.Id,
                Name = abilityToCreate.Name
            };

            response.Data = abilityToGet;

            return Created("Ability created", response);
        }
    }
}