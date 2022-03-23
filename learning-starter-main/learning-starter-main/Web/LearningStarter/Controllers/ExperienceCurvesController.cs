using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/experience-curves")]
    
    public class ExperienceCurvesController : ControllerBase
    {
        private DataContext _dataContext;

        public ExperienceCurvesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var experienceCurvesToReturn = _dataContext
                .ExperienceCurves
                .Select(x => new ExperienceCurveGetDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            response.Data = experienceCurvesToReturn;

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

            var experienceCurveFromDatabase = _dataContext
                .ExperienceCurves
                .FirstOrDefault(x => x.Id == id);

            if (experienceCurveFromDatabase == null)
            {
                response.AddError("Id", "Experience Curve not found");
                return NotFound(response);
            }

            var experienceCurveToReturn = new ExperienceCurveGetDto
            {
                Id = experienceCurveFromDatabase.Id,
                Name = experienceCurveFromDatabase.Name
            };

            response.Data = experienceCurveToReturn;

            return Ok(response);
        }
        
        [HttpPost]
        public IActionResult Create(
            [FromBody] ExperienceCurveCreateDto experienceCurveCreateDto)
        {
            var response = new Response();

            if (experienceCurveCreateDto == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }
            
            if (string.IsNullOrEmpty(experienceCurveCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .ExperienceCurves
                .Any(x => x.Name == experienceCurveCreateDto.Name);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var experienceCurveToCreate = new ExperienceCurve
            {
                Name = experienceCurveCreateDto.Name
            };

            _dataContext.Add(experienceCurveToCreate);
            _dataContext.SaveChanges();
            
            var experienceCurveToGet = new ExperienceCurveGetDto()
            {
                Id = experienceCurveToCreate.Id,
                Name = experienceCurveToCreate.Name
            };

            response.Data = experienceCurveToGet;

            return Created("Experience Curve created", response);
        }
        
        [HttpPut("{id:int}")]
        public IActionResult Edit([FromRoute] int id,
            [FromBody] ExperienceCurveUpdateDto experienceCurve)
        {
            var response = new Response();

            if (experienceCurve == null)
            {
                response.AddError("", "Cannot pass a null entity.");
                return BadRequest(response);
            }

            experienceCurve.Name = experienceCurve.Name.Trim();
            if (string.IsNullOrEmpty(experienceCurve.Name))
            {
                response.AddError("Name", "Name cannot be null or empty");
            }

            var hasNameInDatabase = _dataContext
                .ExperienceCurves
                .Any(x => x.Name.ToLower() == experienceCurve.Name.ToLower() && x.Id != id);
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var experienceCurveToUpdate = _dataContext
                .ExperienceCurves
                .FirstOrDefault(x => x.Id == id);

            experienceCurveToUpdate.Name = experienceCurve.Name;
            _dataContext.SaveChanges();

            var experienceCurveGet = new ExperienceCurveGetDto
            {
                Name = experienceCurve.Name
            };

            response.Data = experienceCurveGet;

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var experienceCurve = _dataContext.ExperienceCurves.FirstOrDefault(x => x.Id == id);

            if (experienceCurve == null)
            {
                response.AddError("id", "Experience Curve not found.");
                return NotFound(response);
            }

            _dataContext.ExperienceCurves.Remove(experienceCurve);
            _dataContext.SaveChanges();

            return Ok(response);
        }
    }
}