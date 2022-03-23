using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/moves")]
    public class MovesController : ControllerBase
    {
        private DataContext _dataContext;

        public MovesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var movesToReturn = _dataContext
                .Moves
                .Select(x => new MoveGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    TypeId = x.TypeId,
                    MoveCategory = x.MoveCategory,
                    BasePower = x.BasePower,
                    PowerPoints = x.PowerPoints,
                    Accuracy = x.Accuracy,
                    SpeedPriority = x.SpeedPriority,
                    IsContactOnHit = x.IsContactOnHit,
                    IsSoundBased = x.IsSoundBased,
                    IsPunchBased = x.IsPunchBased,
                    IsAffectedByGravity = x.IsAffectedByGravity,
                    IsDefrostOnUse = x.IsDefrostOnUse,
                    IsBlockedByProtect = x.IsBlockedByProtect
                })
                .ToList();

            response.Data = movesToReturn;

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

            var moveFromDatabase = _dataContext
                .Moves
                .FirstOrDefault(x => x.Id == id);

            if (moveFromDatabase == null)
            {
                response.AddError("Id", "Move not found");
                return NotFound(response);
            }

            var moveToReturn = new MoveGetDto
            {
                Id = moveFromDatabase.Id,
                Name = moveFromDatabase.Name,
                TypeId = moveFromDatabase.TypeId,
                MoveCategory = moveFromDatabase.MoveCategory,
                BasePower = moveFromDatabase.BasePower,
                PowerPoints = moveFromDatabase.PowerPoints,
                Accuracy = moveFromDatabase.Accuracy,
                SpeedPriority = moveFromDatabase.SpeedPriority,
                IsContactOnHit = moveFromDatabase.IsContactOnHit,
                IsSoundBased = moveFromDatabase.IsSoundBased,
                IsPunchBased = moveFromDatabase.IsPunchBased,
                IsAffectedByGravity = moveFromDatabase.IsAffectedByGravity,
                IsDefrostOnUse = moveFromDatabase.IsDefrostOnUse,
                IsBlockedByProtect = moveFromDatabase.IsBlockedByProtect
            };

            response.Data = moveToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] MoveCreateDto moveCreateDto)
        {
            var response = new Response();

            if(moveCreateDto == null)
            {
                response.AddError("","Cannot pass a null entity.");
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(moveCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be null or empty.");
            }

            var hasNameInDatabase = _dataContext
                .Moves
                .Any(x => x.Name == moveCreateDto.Name);
            
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists.");
            }

            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == moveCreateDto.TypeId);

            if (!isValidType)
            {
                response.AddError("Type", "Type is not valid.");
            }
            
            // var isValidMoveCategory = _dataContext
            //     .MoveCategories
            //     .Any(x => x.Id == moveCreateDto.MoveCategory);
            //
            // if (!isValidMoveCategory)
            // {
            //     response.AddError("Move Category", "Move Category is not valid.");
            // }

            if (moveCreateDto.BasePower < 0)
            {
                response.AddError("Base Power", "Base Power cannot be negative");
            }

            if (moveCreateDto.Accuracy <= 0 || moveCreateDto.Accuracy > 101)
            {
                response.AddError("Accuracy", "Accuracy must be between 1 and 101");
            }

            if (moveCreateDto.PowerPoints < 5)
            {
                response.AddError("Power Points", "Power Points must be greater than 5");
            }

            if (moveCreateDto.SpeedPriority < -7 || moveCreateDto.SpeedPriority > 5)
            {
                response.AddError("Speed Priority", 
                    "Priority cannot be less than -7 or greater than+ 5");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var moveToCreate = new Move
            {
                Name = moveCreateDto.Name,
                TypeId = moveCreateDto.TypeId,
                MoveCategory = moveCreateDto.MoveCategory,
                BasePower = moveCreateDto.BasePower,
                PowerPoints = moveCreateDto.PowerPoints,
                Accuracy = moveCreateDto.Accuracy,
                SpeedPriority = moveCreateDto.SpeedPriority,
                IsContactOnHit = moveCreateDto.IsContactOnHit,
                IsSoundBased = moveCreateDto.IsSoundBased,
                IsPunchBased = moveCreateDto.IsPunchBased,
                IsAffectedByGravity = moveCreateDto.IsAffectedByGravity,
                IsDefrostOnUse = moveCreateDto.IsDefrostOnUse,
                IsBlockedByProtect = moveCreateDto.IsBlockedByProtect
            };

            _dataContext.Add(moveToCreate);
            _dataContext.SaveChanges();

            var moveToGet = new MoveGetDto
            {
                Id = moveToCreate.Id,
                Name = moveToCreate.Name,
                TypeId = moveToCreate.TypeId,
                MoveCategory = moveToCreate.MoveCategory,
                BasePower = moveToCreate.BasePower,
                PowerPoints = moveToCreate.PowerPoints,
                Accuracy = moveToCreate.Accuracy,
                SpeedPriority = moveToCreate.SpeedPriority,
                IsContactOnHit = moveToCreate.IsContactOnHit,
                IsSoundBased = moveToCreate.IsSoundBased,
                IsPunchBased = moveToCreate.IsPunchBased,
                IsAffectedByGravity = moveToCreate.IsAffectedByGravity,
                IsDefrostOnUse = moveToCreate.IsDefrostOnUse,
                IsBlockedByProtect = moveToCreate.IsBlockedByProtect
            };

            response.Data = moveToGet;

            return Created("Move Created", response);
        }
    }
}