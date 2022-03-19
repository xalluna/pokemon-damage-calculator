using System.Collections.Generic;
using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/pokemon-species")]
    public class PokemonSpeciesController : ControllerBase
    {
        private DataContext _dataContext;

        public PokemonSpeciesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var pokemonSpeciesToReturn = _dataContext
                .PokemonSpecies
                .Select(x => new PokemonSpeciesGetDto()
                {
                    Id = x.Id,
                    Species = x.Species,
                    BaseHealth = x.BaseHealth,
                    BaseAttack = x.BaseAttack,
                    BaseDefense = x.BaseDefense,
                    BaseSpecialAttack = x.BaseSpecialAttack,
                    BaseSpecialDefense = x.BaseSpecialDefense,
                    BaseSpeed = x.BaseSpeed,
                    PrimaryTypeId = x.PrimaryTypeId,
                    SecondaryTypeId = x.SecondaryTypeId,
                    PrimaryAbilityId = x.PrimaryAbilityId,
                    SecondaryAbilityId = x.SecondaryAbilityId,
                    HiddenAbilityId = x.HiddenAbilityId,
                    ExperienceCurveId = x.ExperienceCurveId,
                    // MoveLearnSet = moveToDto(x.MoveLearnSet)
                })
                .ToList();

            response.Data = pokemonSpeciesToReturn;

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

            var pokemonSpeciesFromDatabase = _dataContext
                .PokemonSpecies
                .FirstOrDefault(x => x.Id == id);

            if (pokemonSpeciesFromDatabase == null)
            {
                response.AddError("Id", "Pokemon Species not found");
                return NotFound(response);
            }

            var pokemonSpeciesToReturn = new PokemonSpeciesGetDto
            {
                Id = pokemonSpeciesFromDatabase.Id,
                Species = pokemonSpeciesFromDatabase.Species,
                BaseHealth = pokemonSpeciesFromDatabase.BaseHealth,
                BaseAttack = pokemonSpeciesFromDatabase.BaseAttack,
                BaseDefense = pokemonSpeciesFromDatabase.BaseDefense,
                BaseSpecialAttack = pokemonSpeciesFromDatabase.BaseSpecialAttack,
                BaseSpecialDefense = pokemonSpeciesFromDatabase.BaseSpecialDefense,
                BaseSpeed = pokemonSpeciesFromDatabase.BaseSpeed,
                PrimaryTypeId = pokemonSpeciesFromDatabase.PrimaryTypeId,
                SecondaryTypeId = pokemonSpeciesFromDatabase.SecondaryTypeId,
                PrimaryAbilityId = pokemonSpeciesFromDatabase.PrimaryAbilityId,
                SecondaryAbilityId = pokemonSpeciesFromDatabase.SecondaryAbilityId,
                HiddenAbilityId = pokemonSpeciesFromDatabase.HiddenAbilityId,
                ExperienceCurveId = pokemonSpeciesFromDatabase.ExperienceCurveId,
                //MoveLearnSet = moveToDto(pokemonSpeciesFromDatabase.MoveLearnSet)
            };

            response.Data = pokemonSpeciesToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] PokemonSpeciesCreateDto pokemonSpeciesCreateDto)
        {
            var response = new Response();
            
            if(pokemonSpeciesCreateDto == null)
            {
                response.AddError("","Cannot pass a null entity.");
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(pokemonSpeciesCreateDto.Species))
            {
                response.AddError("Species", "Species cannot be null or empty.");
            }

            var hasNameInDatabase = _dataContext
                .PokemonSpecies
                .Any(x => x.Species == pokemonSpeciesCreateDto.Species);
            
            if (hasNameInDatabase)
            {
                response.AddError("Species", "Species already exists.");
            }

            if (pokemonSpeciesCreateDto.BaseHealth < 0 || pokemonSpeciesCreateDto.BaseHealth > 255)
            {
                response.AddError("Base Health", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemonSpeciesCreateDto.BaseAttack < 0 || pokemonSpeciesCreateDto.BaseAttack > 255)
            {
                response.AddError("BaseAttack", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemonSpeciesCreateDto.BaseDefense < 0 || pokemonSpeciesCreateDto.BaseDefense > 255)
            {
                response.AddError("Base Defense", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemonSpeciesCreateDto.BaseSpecialAttack < 0 || pokemonSpeciesCreateDto.BaseSpecialAttack > 255)
            {
                response.AddError("Base Special Attack", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemonSpeciesCreateDto.BaseSpecialDefense < 0 || pokemonSpeciesCreateDto.BaseSpecialDefense > 255)
            {
                response.AddError("Base Special Defense", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemonSpeciesCreateDto.BaseSpeed < 0 || pokemonSpeciesCreateDto.BaseSpeed > 255)
            {
                response.AddError("Base Speed", "Base Stats cannot be negative or greater than 255");
            }
            
            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == pokemonSpeciesCreateDto.PrimaryTypeId);
            
            if (!isValidType)
            {
                response.AddError("Primary Type", "Primary Type is not valid.");
            }
            
            isValidType = _dataContext
                .Types
                .Any(x => x.Id == pokemonSpeciesCreateDto.SecondaryTypeId);
            
            if (!isValidType && pokemonSpeciesCreateDto.SecondaryTypeId != null)
            {
                response.AddError("Secondary Type", "Secondary Type is not valid.");
            }
            
            var isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemonSpeciesCreateDto.PrimaryAbilityId);

            if (!isValidAbility)
            {
               response.AddError("Primary Ability", "Primary Ability is not valid."); 
            }
            
            isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemonSpeciesCreateDto.SecondaryAbilityId);
            
            if (!isValidAbility && pokemonSpeciesCreateDto.SecondaryAbilityId != null)
            {
                response.AddError("Secondary Ability", "Secondary Ability is not valid."); 
            }
            
            isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemonSpeciesCreateDto.HiddenAbilityId);
            
            if (!isValidAbility && pokemonSpeciesCreateDto.HiddenAbilityId != null)
            {
                response.AddError("Hidden Ability", "Hidden Ability is not valid."); 
            }

            var isValidExperienceCurve = _dataContext
                .ExperienceCurves
                .Any(x => x.Id == pokemonSpeciesCreateDto.ExperienceCurveId);

            if (!isValidExperienceCurve)
            {
                response.AddError("Experience Curve", "Experience Curve is not valid.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }
            
            var pokemonSpeciesToCreate = new PokemonSpecies
            {
                Species = pokemonSpeciesCreateDto.Species,
                BaseHealth = pokemonSpeciesCreateDto.BaseHealth,
                BaseAttack = pokemonSpeciesCreateDto.BaseAttack,
                BaseDefense = pokemonSpeciesCreateDto.BaseDefense,
                BaseSpecialAttack = pokemonSpeciesCreateDto.BaseSpecialAttack,
                BaseSpecialDefense = pokemonSpeciesCreateDto.BaseSpecialDefense,
                BaseSpeed = pokemonSpeciesCreateDto.BaseSpeed,
                PrimaryTypeId = pokemonSpeciesCreateDto.PrimaryTypeId,
                SecondaryTypeId = pokemonSpeciesCreateDto.SecondaryTypeId,
                PrimaryAbilityId = pokemonSpeciesCreateDto.PrimaryAbilityId,
                SecondaryAbilityId = pokemonSpeciesCreateDto.SecondaryAbilityId,
                HiddenAbilityId = pokemonSpeciesCreateDto.HiddenAbilityId,
                ExperienceCurveId = pokemonSpeciesCreateDto.ExperienceCurveId,
                //MoveLearnSet = dtoToMove(pokemonSpeciesCreateDto.MoveLearnSet)
            };

            _dataContext.Add(pokemonSpeciesToCreate);
            _dataContext.SaveChanges();
            
            var pokemonSpeciesToGet = new PokemonSpeciesGetDto
            {
                Id = pokemonSpeciesToCreate.Id,
                Species = pokemonSpeciesToCreate.Species,
                BaseHealth = pokemonSpeciesToCreate.BaseHealth,
                BaseAttack = pokemonSpeciesToCreate.BaseAttack,
                BaseDefense = pokemonSpeciesToCreate.BaseDefense,
                BaseSpecialAttack = pokemonSpeciesToCreate.BaseSpecialAttack,
                BaseSpecialDefense = pokemonSpeciesToCreate.BaseSpecialDefense,
                BaseSpeed = pokemonSpeciesToCreate.BaseSpeed,
                PrimaryTypeId = pokemonSpeciesToCreate.PrimaryTypeId,
                SecondaryTypeId = pokemonSpeciesToCreate.SecondaryTypeId,
                PrimaryAbilityId = pokemonSpeciesToCreate.PrimaryAbilityId,
                SecondaryAbilityId = pokemonSpeciesToCreate.SecondaryAbilityId,
                HiddenAbilityId = pokemonSpeciesToCreate.HiddenAbilityId,
                ExperienceCurveId = pokemonSpeciesToCreate.ExperienceCurveId,
                //MoveLearnSet = moveToDto(pokemonSpeciesToCreate.MoveLearnSet)
            };

            response.Data = pokemonSpeciesToGet;

            return Created("Pokemon Species created", response);

        }

        // public List<MoveGetDto> moveToDto(List<Move> moves)
        // {
        //     List<MoveGetDto> moveDtos = new List<MoveGetDto>();
        //     
        //     foreach (Move x in moves)
        //     {
        //         moveDtos.Add(new MoveGetDto
        //         {
        //             Id = x.Id,
        //             Species = x.Species,
        //             TypeId = x.TypeId,
        //             MoveCategory = x.MoveCategory,
        //             BasePower = x.BasePower,
        //             PowerPoints = x.PowerPoints,
        //             Accuracy = x.Accuracy,
        //             SpeedPriority = x.SpeedPriority,
        //             IsContactOnHit = x.IsContactOnHit,
        //             IsSoundBased = x.IsSoundBased,
        //             IsPunchBased = x.IsPunchBased,
        //             IsAffectedByGravity = x.IsAffectedByGravity,
        //             IsDefrostOnUse = x.IsDefrostOnUse,
        //             IsBlockedByProtect = x.IsBlockedByProtect
        //         });
        //     }
        //
        //     return moveDtos;
        // }
        //
        // public List<Move> dtoToMove(List<MoveGetDto> movesDtos)
        // {
        //     List<Move> moves = new List<Move>();
        //     
        //     foreach (MoveGetDto x in movesDtos)
        //     {
        //         moves.Add(new Move
        //         {
        //             Id = x.Id,
        //             Species = x.Species,
        //             TypeId = x.TypeId,
        //             MoveCategory = x.MoveCategory,
        //             BasePower = x.BasePower,
        //             PowerPoints = x.PowerPoints,
        //             Accuracy = x.Accuracy,
        //             SpeedPriority = x.SpeedPriority,
        //             IsContactOnHit = x.IsContactOnHit,
        //             IsSoundBased = x.IsSoundBased,
        //             IsPunchBased = x.IsPunchBased,
        //             IsAffectedByGravity = x.IsAffectedByGravity,
        //             IsDefrostOnUse = x.IsDefrostOnUse,
        //             IsBlockedByProtect = x.IsBlockedByProtect
        //         });
        //     }
        //
        //     return moves;
        // }
    }
}