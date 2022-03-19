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
            [FromBody] AbilityCreateDto ability)
        {
            var response = new Response();

            if (ability == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }

            ability.Name = ability.Name.Trim();
            if (string.IsNullOrEmpty(ability.Name))
            {
                response.AddError("Species", "Species cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Abilities
                .Any(x => x.Name == ability.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Species", "Species already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var abilityToCreate = new Ability
            {
                Name = ability.Name
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

        [HttpPut("{id:int}")]
        public IActionResult Edit([FromRoute] int id,
            [FromBody] AbilityUpdateDto ability)
        {
            var response = new Response();

            if (ability == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }

            ability.Name = ability.Name.Trim();
            if (string.IsNullOrEmpty(ability.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .Abilities
                .Any(x => x.Name.ToLower() == ability.Name.ToLower() && x.Id != id);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var abilityToUpdate = _dataContext
                .Abilities
                .FirstOrDefault(x => x.Id == id);

            abilityToUpdate.Name = ability.Name;
            _dataContext.SaveChanges();

            var abilityGet = new AbilityGetDto
            {
                Name = ability.Name
            };

            response.Data = abilityGet;

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var ability = _dataContext.Abilities.FirstOrDefault(x => x.Id == id);

            if (ability == null)
            {
                response.AddError("id", "Ability not found.");
                return NotFound(response);
            }

            _dataContext.Abilities.Remove(ability);
            _dataContext.SaveChanges();

            return Ok(response);
        }

    }
}