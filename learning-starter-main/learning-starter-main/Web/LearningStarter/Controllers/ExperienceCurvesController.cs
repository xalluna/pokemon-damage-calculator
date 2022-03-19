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
    }
}