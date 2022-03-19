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
                    MoveCategoryId = x.MoveCategoryId,
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
                MoveCategoryId = moveFromDatabase.MoveCategoryId,
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
        
        [HttpGet("type/{id:int}")]

        public IActionResult GetByType(int id)
        {
            var response = new Response();

            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == id);

            if (!isValidType)
            {
                response.AddError("type id","Type Id not found");
                return BadRequest(response);
            }

            var moveFromDatabase = _dataContext
                .Moves
                .Where(x => x.TypeId == id)
                .ToList();
            
            if (!moveFromDatabase.Any())
            {
                response.AddError("Move", "No moves found");
                return BadRequest(response);
            }

            var moveToGet = moveFromDatabase
                .Select(x => new MoveGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    TypeId = x.TypeId,
                    MoveCategoryId = x.MoveCategoryId,
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
            
            response.Data = moveToGet;
            
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] MoveCreateDto move)
        {
            var response = new Response();

            if(move == null)
            {
                response.AddError("","Cannot pass a null entity.");
                return BadRequest(response);
            }

            move.Name = move.Name.Trim();
            if (string.IsNullOrEmpty(move.Name))
            {
                response.AddError("Species", "Species cannot be null or empty.");
            }

            var hasNameInDatabase = _dataContext
                .Moves
                .Any(x => x.Name == move.Name);
            
            if (hasNameInDatabase)
            {
                response.AddError("Species", "Species already exists.");
            }

            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == move.TypeId);

            if (!isValidType)
            {
                response.AddError("Type", "Type is not valid.");
            }
            
            if (move.BasePower < 0)
            {
                response.AddError("Base Power", "Base Power cannot be negative");
            }

            if (move.Accuracy <= 0 || move.Accuracy > 101)
            {
                response.AddError("Accuracy", "Accuracy must be between 1 and 101");
            }

            if (move.PowerPoints < 5)
            {
                response.AddError("Power Points", "Power Points must be greater than 5");
            }

            if (move.SpeedPriority < -7 || move.SpeedPriority > 5)
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
                Name = move.Name,
                TypeId = move.TypeId,
                MoveCategoryId = move.MoveCategoryId,
                BasePower = move.BasePower,
                PowerPoints = move.PowerPoints,
                Accuracy = move.Accuracy,
                SpeedPriority = move.SpeedPriority,
                IsContactOnHit = move.IsContactOnHit,
                IsSoundBased = move.IsSoundBased,
                IsPunchBased = move.IsPunchBased,
                IsAffectedByGravity = move.IsAffectedByGravity,
                IsDefrostOnUse = move.IsDefrostOnUse,
                IsBlockedByProtect = move.IsBlockedByProtect
            };

            _dataContext.Add(moveToCreate);
            _dataContext.SaveChanges();

            var moveToGet = new MoveGetDto
            {
                Id = moveToCreate.Id,
                Name = moveToCreate.Name,
                TypeId = moveToCreate.TypeId,
                MoveCategoryId = moveToCreate.MoveCategoryId,
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

        [HttpPut("{id:int}")]
        public IActionResult Edit([FromRoute] int id,
            [FromBody] MoveUpdateDto move)
        {
            var response = new Response();

            if(move == null)
            {
                response.AddError("","Cannot pass a null entity.");
                return BadRequest(response);
            }

            move.Name = move.Name.Trim();
            if (string.IsNullOrEmpty(move.Name))
            {
                response.AddError("Name", "Name cannot be null or empty.");
            }

            var hasNameInDatabase = _dataContext
                .Moves
                .Any(x => x.Name == move.Name && x.Id != id);
            
            if (hasNameInDatabase)
            {
                response.AddError("Name", "Name already exists.");
            }

            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == move.TypeId);

            if (!isValidType)
            {
                response.AddError("Type", "Type is not valid.");
            }
            
            if (move.BasePower < 0)
            {
                response.AddError("Base Power", "Base Power cannot be negative");
            }

            if (move.Accuracy <= 0 || move.Accuracy > 101)
            {
                response.AddError("Accuracy", "Accuracy must be between 1 and 101");
            }

            if (move.PowerPoints < 5)
            {
                response.AddError("Power Points", "Power Points must be greater than 5");
            }

            if (move.SpeedPriority < -7 || move.SpeedPriority > 5)
            {
                response.AddError("Speed Priority", 
                    "Priority cannot be less than -7 or greater than+ 5");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            if (move.IsPunchBased == null)
            {
                move.IsPunchBased = false;
            }
            
            if (move.IsSoundBased == null)
            {
                move.IsSoundBased = false;
            }
            
            if (move.IsAffectedByGravity == null)
            {
                move.IsAffectedByGravity = false;
            }
            
            if (move.IsBlockedByProtect == null)
            {
                move.IsBlockedByProtect = false;
            }
            
            if (move.IsContactOnHit == null)
            {
                move.IsContactOnHit = false;
            }
            
            if (move.IsDefrostOnUse == null)
            {
                move.IsDefrostOnUse = false;
            }

            var moveToUpdate = _dataContext
                .Moves
                .FirstOrDefault(x => x.Id == id);
            
            moveToUpdate.Name = move.Name;
            moveToUpdate.TypeId = move.TypeId;
            moveToUpdate.MoveCategoryId = move.MoveCategoryId;
            moveToUpdate.BasePower = move.BasePower;
            moveToUpdate.PowerPoints = move.PowerPoints;
            moveToUpdate.Accuracy = move.Accuracy;
            moveToUpdate.SpeedPriority = move.SpeedPriority;
            moveToUpdate.IsContactOnHit = move.IsContactOnHit;
            moveToUpdate.IsSoundBased = move.IsSoundBased;
            moveToUpdate.IsPunchBased = move.IsPunchBased;
            moveToUpdate.IsAffectedByGravity = move.IsAffectedByGravity;
            moveToUpdate.IsDefrostOnUse = move.IsDefrostOnUse;
            moveToUpdate.IsBlockedByProtect = move.IsBlockedByProtect;

            _dataContext.SaveChanges();

            var moveGet = new MoveGetDto
            {
                Id = moveToUpdate.Id,
                Name = moveToUpdate.Name,
                TypeId = moveToUpdate.TypeId,
                MoveCategoryId = moveToUpdate.MoveCategoryId,
                BasePower = moveToUpdate.BasePower,
                PowerPoints = moveToUpdate.PowerPoints,
                Accuracy = moveToUpdate.Accuracy,
                SpeedPriority = moveToUpdate.SpeedPriority,
                IsContactOnHit = moveToUpdate.IsContactOnHit,
                IsSoundBased = moveToUpdate.IsSoundBased,
                IsPunchBased = moveToUpdate.IsPunchBased,
                IsAffectedByGravity = moveToUpdate.IsAffectedByGravity,
                IsDefrostOnUse = moveToUpdate.IsDefrostOnUse,
                IsBlockedByProtect = moveToUpdate.IsBlockedByProtect
            };

            response.Data = moveGet;

            return Ok(response);
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var move = _dataContext.Moves.FirstOrDefault(x => x.Id == id);

            if (move == null)
            {
                response.AddError("id", "Move not found.");
                return NotFound(response);
            }

            _dataContext.Moves.Remove(move);
            _dataContext.SaveChanges();

            return Ok(response);
        }
    }
}