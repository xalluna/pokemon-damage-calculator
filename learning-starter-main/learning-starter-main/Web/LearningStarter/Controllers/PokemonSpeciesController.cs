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
                    ExperienceCurveId = x.ExperienceCurveId
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
                ExperienceCurveId = pokemonSpeciesFromDatabase.ExperienceCurveId
            };

            response.Data = pokemonSpeciesToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] PokemonSpeciesCreateDto pokemon)
        {
            var response = new Response();
            
            if(pokemon == null)
            {
                response.AddError("","Cannot pass a null entity.");
                return BadRequest(response);
            }

            pokemon.Species = pokemon.Species.Trim();
            if (string.IsNullOrEmpty(pokemon.Species))
            {
                response.AddError("Species", "Species cannot be null or empty.");
            }

            var hasNameInDatabase = _dataContext
                .PokemonSpecies
                .Any(x => x.Species == pokemon.Species);
            
            if (hasNameInDatabase)
            {
                response.AddError("Species", "Species already exists.");
            }

            if (pokemon.BaseHealth < 0 || pokemon.BaseHealth > 255)
            {
                response.AddError("Base Health", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseAttack < 0 || pokemon.BaseAttack > 255)
            {
                response.AddError("BaseAttack", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseDefense < 0 || pokemon.BaseDefense > 255)
            {
                response.AddError("Base Defense", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseSpecialAttack < 0 || pokemon.BaseSpecialAttack > 255)
            {
                response.AddError("Base Special Attack", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseSpecialDefense < 0 || pokemon.BaseSpecialDefense > 255)
            {
                response.AddError("Base Special Defense", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseSpeed < 0 || pokemon.BaseSpeed > 255)
            {
                response.AddError("Base Speed", "Base Stats cannot be negative or greater than 255");
            }
            
            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == pokemon.PrimaryTypeId);
            
            if (!isValidType)
            {
                response.AddError("Primary Type", "Primary Type is not valid.");
            }
            
            isValidType = _dataContext
                .Types
                .Any(x => x.Id == pokemon.SecondaryTypeId);
            
            if (!isValidType && pokemon.SecondaryTypeId != null)
            {
                response.AddError("Secondary Type", "Secondary Type is not valid.");
            }
            
            var isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemon.PrimaryAbilityId);

            if (!isValidAbility)
            {
               response.AddError("Primary Ability", "Primary Ability is not valid."); 
            }
            
            isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemon.SecondaryAbilityId);
            
            if (!isValidAbility && pokemon.SecondaryAbilityId != null)
            {
                response.AddError("Secondary Ability", "Secondary Ability is not valid."); 
            }
            
            isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemon.HiddenAbilityId);
            
            if (!isValidAbility && pokemon.HiddenAbilityId != null)
            {
                response.AddError("Hidden Ability", "Hidden Ability is not valid."); 
            }

            var isValidExperienceCurve = _dataContext
                .ExperienceCurves
                .Any(x => x.Id == pokemon.ExperienceCurveId);

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
                Species = pokemon.Species,
                BaseHealth = pokemon.BaseHealth,
                BaseAttack = pokemon.BaseAttack,
                BaseDefense = pokemon.BaseDefense,
                BaseSpecialAttack = pokemon.BaseSpecialAttack,
                BaseSpecialDefense = pokemon.BaseSpecialDefense,
                BaseSpeed = pokemon.BaseSpeed,
                PrimaryTypeId = pokemon.PrimaryTypeId,
                SecondaryTypeId = pokemon.SecondaryTypeId,
                PrimaryAbilityId = pokemon.PrimaryAbilityId,
                SecondaryAbilityId = pokemon.SecondaryAbilityId,
                HiddenAbilityId = pokemon.HiddenAbilityId,
                ExperienceCurveId = pokemon.ExperienceCurveId
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
                ExperienceCurveId = pokemonSpeciesToCreate.ExperienceCurveId
            };

            response.Data = pokemonSpeciesToGet;

            return Created("Pokemon Species created", response);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit([FromRoute] int id,
            [FromBody] PokemonSpeciesUpdateDto pokemon)
        {
            var response = new Response();
            
            if(pokemon == null)
            {
                response.AddError("","Cannot pass a null entity.");
                return BadRequest(response);
            }

            pokemon.Species = pokemon.Species.Trim();
            if (string.IsNullOrEmpty(pokemon.Species))
            {
                response.AddError("Species", "Species cannot be null or empty.");
            }

            var hasNameInDatabase = _dataContext
                .PokemonSpecies
                .Any(x => x.Species == pokemon.Species && x.Id != id);
            
            if (hasNameInDatabase)
            {
                response.AddError("Species", "Species already exists.");
            }

            if (pokemon.BaseHealth < 0 || pokemon.BaseHealth > 255)
            {
                response.AddError("Base Health", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseAttack < 0 || pokemon.BaseAttack > 255)
            {
                response.AddError("BaseAttack", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseDefense < 0 || pokemon.BaseDefense > 255)
            {
                response.AddError("Base Defense", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseSpecialAttack < 0 || pokemon.BaseSpecialAttack > 255)
            {
                response.AddError("Base Special Attack", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseSpecialDefense < 0 || pokemon.BaseSpecialDefense > 255)
            {
                response.AddError("Base Special Defense", "Base Stats cannot be negative or greater than 255");
            }
            
            if (pokemon.BaseSpeed < 0 || pokemon.BaseSpeed > 255)
            {
                response.AddError("Base Speed", "Base Stats cannot be negative or greater than 255");
            }
            
            var isValidType = _dataContext
                .Types
                .Any(x => x.Id == pokemon.PrimaryTypeId);
            
            if (!isValidType)
            {
                response.AddError("Primary Type", "Primary Type is not valid.");
            }
            
            isValidType = _dataContext
                .Types
                .Any(x => x.Id == pokemon.SecondaryTypeId);
            
            if (!isValidType && pokemon.SecondaryTypeId != null)
            {
                response.AddError("Secondary Type", "Secondary Type is not valid.");
            }
            
            var isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemon.PrimaryAbilityId);

            if (!isValidAbility)
            {
               response.AddError("Primary Ability", "Primary Ability is not valid."); 
            }
            
            isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemon.SecondaryAbilityId);
            
            if (!isValidAbility && pokemon.SecondaryAbilityId != null)
            {
                response.AddError("Secondary Ability", "Secondary Ability is not valid."); 
            }
            
            isValidAbility = _dataContext
                .Abilities
                .Any(x => x.Id == pokemon.HiddenAbilityId);
            
            if (!isValidAbility && pokemon.HiddenAbilityId != null)
            {
                response.AddError("Hidden Ability", "Hidden Ability is not valid."); 
            }

            var isValidExperienceCurve = _dataContext
                .ExperienceCurves
                .Any(x => x.Id == pokemon.ExperienceCurveId);

            if (!isValidExperienceCurve)
            {
                response.AddError("Experience Curve", "Experience Curve is not valid.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var pokemonToUpdate = _dataContext
                .PokemonSpecies
                .FirstOrDefault(x => x.Id == id);

            pokemonToUpdate.Species = pokemon.Species;
            pokemonToUpdate.BaseHealth = pokemon.BaseHealth;
            pokemonToUpdate.BaseAttack = pokemon.BaseAttack;
            pokemonToUpdate.BaseDefense = pokemon.BaseDefense;
            pokemonToUpdate.BaseSpecialAttack = pokemon.BaseSpecialAttack;
            pokemonToUpdate.BaseSpecialDefense = pokemon.BaseSpecialDefense;
            pokemonToUpdate.BaseSpeed = pokemon.BaseSpeed;
            pokemonToUpdate.PrimaryTypeId = pokemon.PrimaryTypeId;
            pokemonToUpdate.SecondaryTypeId = pokemon.SecondaryTypeId;
            pokemonToUpdate.PrimaryAbilityId = pokemon.PrimaryAbilityId;
            pokemonToUpdate.SecondaryAbilityId = pokemon.SecondaryAbilityId;
            pokemonToUpdate.HiddenAbilityId = pokemon.HiddenAbilityId;
            pokemonToUpdate.ExperienceCurveId = pokemon.ExperienceCurveId;

            _dataContext.SaveChanges();

            var pokemonGet = new PokemonSpeciesGetDto
            {
                Id = id,
                Species = pokemon.Species,
                BaseHealth = pokemon.BaseHealth,
                BaseAttack = pokemon.BaseAttack,
                BaseDefense = pokemon.BaseDefense,
                BaseSpecialAttack = pokemon.BaseSpecialAttack,
                BaseSpecialDefense = pokemon.BaseSpecialDefense,
                BaseSpeed = pokemon.BaseSpeed,
                PrimaryTypeId = pokemon.PrimaryTypeId,
                SecondaryTypeId = pokemon.SecondaryTypeId,
                PrimaryAbilityId = pokemon.PrimaryAbilityId,
                SecondaryAbilityId = pokemon.SecondaryAbilityId,
                HiddenAbilityId = pokemon.HiddenAbilityId,
                ExperienceCurveId = pokemon.ExperienceCurveId
            };

            response.Data = pokemonGet;

            return Ok(response);
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var pokemon = _dataContext.PokemonSpecies.FirstOrDefault(x => x.Id == id);

            if (pokemon == null)
            {
                response.AddError("id", "Pokemon species not found.");
                return NotFound(response);
            }

            _dataContext.PokemonSpecies.Remove(pokemon);
            _dataContext.SaveChanges();

            return Ok(response);
        }
    }
}