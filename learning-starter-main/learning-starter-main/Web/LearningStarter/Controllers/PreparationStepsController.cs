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
    [Route("api/preparation-steps")]
    
    public class PreparationStepsController : ControllerBase
    {
        private DataContext _dataContext;

        public PreparationStepsController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var preparationStepsToReturn = _dataContext
                .PreparationSteps
                .Select(x => new PreparationStepGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = preparationStepsToReturn;

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

            var preparationStepFromDatabase = _dataContext
                .PreparationSteps
                .FirstOrDefault(x => id == x.Id);

            if (preparationStepFromDatabase == null)
            {
                response.AddError("id", "Preparation Step not found.");
                return NotFound(response);
            }

            var preparationStepToReturn = new PreparationStepGetDto
            {
                Id = preparationStepFromDatabase.Id,
                Name = preparationStepFromDatabase.Name
            };

            response.Data = preparationStepToReturn;

            return Ok(response);
        }
        
    }
}