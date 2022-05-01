using System.Collections.Generic;
using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    
    public class PokemonController : ControllerBase
    {
        private DataContext _dataContext;

        public PokemonController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var pokemonToGet = _dataContext
                .Pokemon
                .Select(x => new PokemonGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PokemonSpeciesId = x.PokemonSpeciesId,
                    HealthEv = x.HealthEv,
                    AttackEv = x.AttackEv,
                    DefenseEv = x.DefenseEv,
                    SpecialAttackEv = x.SpecialAttackEv,
                    SpecialDefenseEv = x.SpecialDefenseEv,
                    SpeedEv = x.SpeedEv,
                    HealthIv = x.HealthIv,
                    AttackIv = x.AttackIv,
                    DefenseIv = x.DefenseIv,
                    SpecialAttackIv = x.SpecialAttackIv,
                    SpecialDefenseIv = x.SpecialDefenseIv,
                    SpeedIv = x.SpeedIv,
                    AbilityId = x.AbilityId,
                    ItemId = x.ItemId,
                    MoveOneId = x.MoveOneId,
                    MoveTwoId = x.MoveTwoId,
                    MoveThreeId = x.MoveThreeId,
                    MoveFourId = x.MoveFourId,
                    Level = x.Level,
                    Experience = x.Experience,
                    NatureId = x.NatureId,
                    Gender = x.Gender,
                    IsShiny = x.IsShiny
                })
                .ToList();
            
            

            response.Data = pokemonToGet;
            
            return Ok(response);
        }

        [HttpGet("list")]
        public IActionResult GetAsList()
        {
            var response = new Response();

            var pokemonToGet = _dataContext
                .Pokemon
                .Select(x => new PokemonListDto
                {
                    Id = x.Id,
                    Pokemon = new PokemonGetDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PokemonSpeciesId = x.PokemonSpeciesId,
                        HealthEv = x.HealthEv,
                        AttackEv = x.AttackEv,
                        DefenseEv = x.DefenseEv,
                        SpecialAttackEv = x.SpecialAttackEv,
                        SpecialDefenseEv = x.SpecialDefenseEv,
                        SpeedEv = x.SpeedEv,
                        HealthIv = x.HealthIv,
                        AttackIv = x.AttackIv,
                        DefenseIv = x.DefenseIv,
                        SpecialAttackIv = x.SpecialAttackIv,
                        SpecialDefenseIv = x.SpecialDefenseIv,
                        SpeedIv = x.SpeedIv,
                        AbilityId = x.AbilityId,
                        ItemId = x.ItemId,
                        MoveOneId = x.MoveOneId,
                        MoveTwoId = x.MoveTwoId,
                        MoveThreeId = x.MoveThreeId,
                        MoveFourId = x.MoveFourId,
                        Level = x.Level,
                        Experience = x.Experience,
                        NatureId = x.NatureId,
                        Gender = x.Gender,
                        IsShiny = x.IsShiny
                    },
                    Name = x.Name,
                    PokemonSpecies = x.PokemonSpecies.Name,
                    HealthEv = x.HealthEv,
                    AttackEv = x.AttackEv,
                    DefenseEv = x.DefenseEv,
                    SpecialAttackEv = x.SpecialAttackEv,
                    SpecialDefenseEv = x.SpecialDefenseEv,
                    SpeedEv = x.SpeedEv,
                    HealthIv = x.HealthIv,
                    AttackIv = x.AttackIv,
                    DefenseIv = x.DefenseIv,
                    SpecialAttackIv = x.SpecialAttackIv,
                    SpecialDefenseIv = x.SpecialDefenseIv,
                    SpeedIv = x.SpeedIv,
                    Ability = x.Ability.Name,
                    Item = x.Item.Name,
                    MoveOne = _dataContext
                        .Moves
                        .FirstOrDefault(y => y.Id == x.MoveOneId)!
                        .Name,
                    MoveTwo = _dataContext
                        .Moves
                        .FirstOrDefault(y => y.Id == x.MoveTwoId)!
                        .Name,
                    MoveThree = _dataContext
                        .Moves
                        .FirstOrDefault(y => y.Id == x.MoveThreeId)!
                        .Name,
                    MoveFour = _dataContext
                        .Moves
                        .FirstOrDefault(y => y.Id == x.MoveFourId)!
                        .Name,
                    Level = x.Level,
                    Experience = x.Experience,
                    Gender = (x.Gender == 0) ? "Male" : (x.Gender == 1) ? "Female" : "Undefined",
                    Nature = x.Nature.Name,
                    IsShiny = x.IsShiny
                })
                .ToList();

            response.Data = pokemonToGet;
            
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

            var pokemonFromDatabase = _dataContext
                .Pokemon
                .FirstOrDefault(x => x.Id == id);

            if (pokemonFromDatabase == null)
            {
                response.AddError("Id", "Pokemon not found");
                return NotFound(response);
            }

            var pokemonToReturn = new PokemonGetDto
            {
                Id = pokemonFromDatabase.Id,
                Name = pokemonFromDatabase.Name,
                PokemonSpeciesId = pokemonFromDatabase.PokemonSpeciesId,
                HealthEv = pokemonFromDatabase.HealthEv,
                AttackEv = pokemonFromDatabase.AttackEv,
                DefenseEv = pokemonFromDatabase.DefenseEv,
                SpecialAttackEv = pokemonFromDatabase.SpecialAttackEv,
                SpecialDefenseEv = pokemonFromDatabase.SpecialDefenseEv,
                SpeedEv = pokemonFromDatabase.SpeedEv,
                HealthIv = pokemonFromDatabase.HealthIv,
                AttackIv = pokemonFromDatabase.AttackIv,
                DefenseIv = pokemonFromDatabase.DefenseIv,
                SpecialAttackIv = pokemonFromDatabase.SpecialAttackIv,
                SpecialDefenseIv = pokemonFromDatabase.SpecialDefenseIv,
                SpeedIv = pokemonFromDatabase.SpeedIv,
                AbilityId = pokemonFromDatabase.AbilityId,
                ItemId = pokemonFromDatabase.ItemId,
                MoveOneId = pokemonFromDatabase.MoveOneId,
                MoveTwoId = pokemonFromDatabase.MoveTwoId,
                MoveThreeId = pokemonFromDatabase.MoveThreeId,
                MoveFourId = pokemonFromDatabase.MoveFourId,
                Level = pokemonFromDatabase.Level,
                Experience = pokemonFromDatabase.Experience,
                NatureId = pokemonFromDatabase.NatureId,
                Gender = pokemonFromDatabase.Gender,
                IsShiny = pokemonFromDatabase.IsShiny
            };

            response.Data = pokemonToReturn;

            return Ok(response);
        }
        
        [HttpGet("species/{id:int}")]

        public IActionResult GetBySpecies(int id)
        {
            var response = new Response();

            var isValidSpecies = _dataContext
                .PokemonSpecies
                .Any(x => x.Id == id);

            if (!isValidSpecies)
            {
                response.AddError("Species","Species not found");
                return BadRequest(response);
            }

            var pokemonFromDatabase = _dataContext
                .Pokemon
                .Where(x => x.PokemonSpeciesId == id)
                .ToList();
            
            if (!pokemonFromDatabase.Any())
            {
                response.AddError("Pokemon", "No pokemon species found");
                return BadRequest(response);
            }

            var pokemonToGet = pokemonFromDatabase
                .Select(x => new PokemonGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PokemonSpeciesId = x.PokemonSpeciesId,
                    HealthEv = x.HealthEv,
                    AttackEv = x.AttackEv,
                    DefenseEv = x.DefenseEv,
                    SpecialAttackEv = x.SpecialAttackEv,
                    SpecialDefenseEv = x.SpecialDefenseEv,
                    SpeedEv = x.SpeedEv,
                    HealthIv = x.HealthIv,
                    AttackIv = x.AttackIv,
                    DefenseIv = x.DefenseIv,
                    SpecialAttackIv = x.SpecialAttackIv,
                    SpecialDefenseIv = x.SpecialDefenseIv,
                    SpeedIv = x.SpeedIv,
                    AbilityId = x.AbilityId,
                    ItemId = x.ItemId,
                    MoveOneId = x.MoveOneId,
                    MoveTwoId = x.MoveTwoId,
                    MoveThreeId = x.MoveThreeId,
                    MoveFourId = x.MoveFourId,
                    Level = x.Level,
                    Experience = x.Experience,
                    NatureId = x.NatureId,
                    Gender = x.Gender,
                    IsShiny = x.IsShiny
                })
                .ToList();
            
            response.Data = pokemonToGet;
            
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PokemonCreateDto pokemon)
        {
            var response = new Response();

            if (pokemon == null)
            {
                response.AddError("", "Entity cannot be null");
                return BadRequest(response);
            }
            
            var pokemonSpecies = _dataContext
                .PokemonSpecies
                .FirstOrDefault(x => x.Id == pokemon.PokemonSpeciesId);

            if (pokemonSpecies == null)
            {
                response.AddError("Pokemon Species", "Pokemon Species not found");
                return NotFound(response);
            }

            pokemon.Name = pokemon.Name.Trim();
            if (string.IsNullOrEmpty(pokemon.Name))
            {
                response.AddError("Name","Name cannot be null or empty");
            }

            if (pokemon.HealthEv > 255)
            {
                response.AddError("HealthEv", "HealthEv cannot be greater than 255");
            }
            
            if (pokemon.HealthEv < 0)
            {
                response.AddError("HealthEv", "HealthEv cannot be less than 0");
            }
            
            if (pokemon.AttackEv > 255)
            {
                response.AddError("AttackEv", "AttackEv cannot be greater than 255");
            }
            
            if (pokemon.AttackEv < 0)
            {
                response.AddError("AttackEv", "AttackEv cannot be less than 0");
            }
            
            if (pokemon.DefenseEv > 255)
            {
                response.AddError("DefenseEv", "DefenseEv cannot be greater than 255");
            }
            
            if (pokemon.DefenseEv < 0)
            {
                response.AddError("DefenseEv", "DefenseEv cannot be less than 0");
            }
            
            if (pokemon.SpecialAttackEv > 255)
            {
                response.AddError("SpecialAttackEv", "SpecialAttackEv cannot be greater than 255");
            }
            
            if (pokemon.SpecialAttackEv < 0)
            {
                response.AddError("SpecialAttackEv", "SpecialAttackEv cannot be less than 0");
            }
            
            if (pokemon.SpecialDefenseEv > 255)
            {
                response.AddError("SpecialDefenseEv", "SpecialDefenseEv cannot be greater than 255");
            }
            
            if (pokemon.SpecialDefenseEv < 0)
            {
                response.AddError("SpecialDefenseEv", "SpecialDefenseEv cannot be less than 0");
            }
            
            if (pokemon.SpeedEv > 255)
            {
                response.AddError("SpeedEv", "SpeedEv cannot be greater than 255");
            }
            
            if (pokemon.SpeedEv < 0)
            {
                response.AddError("SpeedEv", "SpeedEv cannot be less than 0");
            }

            var evTotal = pokemon.AttackEv + pokemon.HealthEv + pokemon.DefenseEv
                      + pokemon.SpeedEv + pokemon.SpecialAttackEv + pokemon.SpecialDefenseEv;

            if (evTotal > 510)
            {
                response.AddError("Ev Total", "Ev Total cannot be greater than 510");
            }

            if (pokemon.HealthIv > 31 || pokemon.HealthIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }
            
            if (pokemon.AttackIv > 31 || pokemon.AttackIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.DefenseIv > 31 || pokemon.DefenseIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.SpecialAttackIv > 31 || pokemon.SpecialAttackIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.SpecialDefenseIv > 31 || pokemon.SpecialDefenseIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.SpeedIv > 31 || pokemon.SpeedIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            var isValidAbility = pokemon.AbilityId == pokemonSpecies.PrimaryAbilityId ||
                                 pokemon.AbilityId == pokemonSpecies.SecondaryAbilityId ||
                                 pokemon.AbilityId == pokemonSpecies.HiddenAbilityId;

            if (!isValidAbility)
            {
                response.AddError("Ability", "Invalid ability");
            }

            var isValidItem = _dataContext
                .Items
                .Any(x => x.Id == pokemon.ItemId);

            if (!isValidItem)
            {
                response.AddError("Item", "Invalid item");
            }

            if (pokemon.Level > 100 || pokemon.Level < 0)
            {
                response.AddError("Level", "Level must be between 0 and 100");
            }

            if (pokemon.Gender > 2 || pokemon.Gender < 0)
            {
                response.AddError("Gender", "Invalid Gender");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var pokemonToCreate = new Pokemon
            {
                Name = pokemon.Name,
                PokemonSpeciesId = pokemon.PokemonSpeciesId,
                HealthEv = pokemon.HealthEv,
                AttackEv = pokemon.AttackEv,
                DefenseEv = pokemon.DefenseEv,
                SpecialAttackEv = pokemon.SpecialAttackEv,
                SpecialDefenseEv = pokemon.SpecialDefenseEv,
                SpeedEv = pokemon.SpeedEv,
                HealthIv = pokemon.HealthIv,
                AttackIv = pokemon.AttackIv,
                DefenseIv = pokemon.DefenseIv,
                SpecialAttackIv = pokemon.SpecialAttackIv,
                SpecialDefenseIv = pokemon.SpecialDefenseIv,
                SpeedIv = pokemon.SpeedIv,
                AbilityId = pokemon.AbilityId,
                ItemId = pokemon.ItemId,
                MoveOneId = pokemon.MoveOneId,
                MoveTwoId = pokemon.MoveTwoId,
                MoveThreeId = pokemon.MoveThreeId,
                MoveFourId = pokemon.MoveFourId,
                Level = pokemon.Level,
                Experience = pokemon.Experience,
                NatureId = pokemon.NatureId,
                Gender = pokemon.Gender,
                IsShiny = pokemon.IsShiny
            };

            _dataContext.Add(pokemonToCreate);
            _dataContext.SaveChanges();

            return Created("Pokemon created", response);
        }
        
        // [HttpPost("form")]
        // public IActionResult Create([FromBody] PokemonFormDto pokemonIn)
        // {
        //     var response = new Response();
        //
        //     if (pokemonIn == null)
        //     {
        //         response.AddError("", "Entity cannot be null");
        //         return BadRequest(response);
        //     }
        //     
        //     pokemonIn.Name = pokemonIn.Name.Trim();
        //     pokemonIn.PokemonSpecies = pokemonIn.PokemonSpecies.Trim();
        //     pokemonIn.HealthEv = pokemonIn.HealthEv.Trim();
        //     pokemonIn.AttackEv = pokemonIn.AttackEv.Trim();
        //     pokemonIn.DefenseEv = pokemonIn.DefenseEv.Trim();
        //     pokemonIn.SpecialAttackEv = pokemonIn.SpecialAttackEv.Trim();
        //     pokemonIn.SpecialDefenseEv = pokemonIn.SpecialDefenseEv.Trim();
        //     pokemonIn.SpeedEv = pokemonIn.SpeedEv.Trim();
        //     pokemonIn.HealthIv = pokemonIn.HealthIv.Trim();
        //     pokemonIn.AttackIv = pokemonIn.AttackIv.Trim();
        //     pokemonIn.DefenseIv = pokemonIn.DefenseIv.Trim();
        //     pokemonIn.SpecialAttackIv = pokemonIn.SpecialAttackIv.Trim();
        //     pokemonIn.SpecialDefenseIv = pokemonIn.SpecialDefenseIv.Trim();
        //     pokemonIn.SpeedIv = pokemonIn.SpeedIv.Trim();
        //     pokemonIn.Ability = pokemonIn.Ability.Trim();
        //     pokemonIn.Item = pokemonIn.Item.Trim();
        //     pokemonIn.MoveOne = pokemonIn.MoveOne.Trim();
        //     if (pokemonIn.MoveTwo != null){ pokemonIn.MoveTwo = pokemonIn.MoveTwo.Trim(); }
        //     if (pokemonIn.MoveThree != null){ pokemonIn.MoveThree = pokemonIn.MoveThree.Trim(); }
        //     if (pokemonIn.MoveFour != null){ pokemonIn.MoveFour = pokemonIn.MoveFour.Trim(); }
        //     pokemonIn.Level = pokemonIn.Level.Trim();
        //     pokemonIn.Experience = pokemonIn.Experience.Trim();
        //     pokemonIn.Nature = pokemonIn.Nature.Trim();
        //     pokemonIn.Gender = pokemonIn.Gender.Trim();
        //     pokemonIn.IsShiny = pokemonIn.IsShiny.Trim();
        //
        //     var pokemon = new PokemonCreateDto
        //     {
        //         Name = pokemonIn.Name,
        //         PokemonSpeciesId = (_dataContext
        //             .PokemonSpecies
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.PokemonSpecies.ToLower()))!
        //             .Id),
        //         HealthEv = int.Parse(pokemonIn.HealthEv),
        //         AttackEv = int.Parse(pokemonIn.AttackEv),
        //         DefenseEv = int.Parse(pokemonIn.DefenseEv),
        //         SpecialAttackEv = int.Parse(pokemonIn.SpecialAttackEv),
        //         SpecialDefenseEv = int.Parse(pokemonIn.SpecialDefenseEv),
        //         SpeedEv = int.Parse(pokemonIn.SpeedEv),
        //         HealthIv = int.Parse(pokemonIn.HealthIv),
        //         AttackIv = int.Parse(pokemonIn.AttackIv),
        //         DefenseIv = int.Parse(pokemonIn.DefenseIv),
        //         SpecialAttackIv = int.Parse(pokemonIn.SpecialAttackIv),
        //         SpecialDefenseIv = int.Parse(pokemonIn.SpecialDefenseIv),
        //         SpeedIv = int.Parse(pokemonIn.SpeedIv),
        //         AbilityId = _dataContext
        //             .Abilities
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.Ability.ToLower()))!
        //             .Id,
        //         ItemId = _dataContext
        //             .Items
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.Item.ToLower()))!
        //             .Id,
        //         MoveOneId = _dataContext
        //             .Moves
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.MoveOne.ToLower()))!
        //             .Id,
        //         MoveTwoId = _dataContext
        //             .Moves
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.MoveTwo.ToLower()))!
        //             .Id,
        //         MoveThreeId = _dataContext
        //             .Moves
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.MoveThree.ToLower()))!
        //             .Id,
        //         MoveFourId = _dataContext
        //             .Moves
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.MoveFour.ToLower()))!
        //             .Id,
        //         Level = int.Parse(pokemonIn.Level),
        //         Experience = int.Parse(pokemonIn.Experience),
        //         NatureId = _dataContext
        //             .Natures
        //             .FirstOrDefault(x => x.Name.ToLower().Equals(pokemonIn.Nature.ToLower()))!
        //             .Id,
        //         Gender = (pokemonIn.Gender.ToLower() == "male") ? 0 :
        //             (pokemonIn.Gender.ToLower() == "female") ? 1 : 2,
        //         IsShiny = (pokemonIn.IsShiny.ToLower() == "true")
        //     };
        //     
        //     var pokemonSpecies = _dataContext
        //         .PokemonSpecies
        //         .FirstOrDefault(x => x.Id == pokemon.PokemonSpeciesId);
        //     
        //     if (pokemonSpecies == null)
        //     {
        //         response.AddError("Pokemon Species", "Pokemon Species not found");
        //         return NotFound(response);
        //     }
        //     
        //     pokemon.Name = pokemon.Name.Trim();
        //     if (string.IsNullOrEmpty(pokemon.Name))
        //     {
        //         response.AddError("Name","Name cannot be null or empty");
        //     }
        //     
        //     if (pokemon.HealthEv > 255)
        //     {
        //         response.AddError("HealthEv", "HealthEv cannot be greater than 255");
        //     }
        //     
        //     if (pokemon.HealthEv < 0)
        //     {
        //         response.AddError("HealthEv", "HealthEv cannot be less than 0");
        //     }
        //     
        //     if (pokemon.AttackEv > 255)
        //     {
        //         response.AddError("AttackEv", "AttackEv cannot be greater than 255");
        //     }
        //     
        //     if (pokemon.AttackEv < 0)
        //     {
        //         response.AddError("AttackEv", "AttackEv cannot be less than 0");
        //     }
        //     
        //     if (pokemon.DefenseEv > 255)
        //     {
        //         response.AddError("DefenseEv", "DefenseEv cannot be greater than 255");
        //     }
        //     
        //     if (pokemon.DefenseEv < 0)
        //     {
        //         response.AddError("DefenseEv", "DefenseEv cannot be less than 0");
        //     }
        //     
        //     if (pokemon.SpecialAttackEv > 255)
        //     {
        //         response.AddError("SpecialAttackEv", "SpecialAttackEv cannot be greater than 255");
        //     }
        //     
        //     if (pokemon.SpecialAttackEv < 0)
        //     {
        //         response.AddError("SpecialAttackEv", "SpecialAttackEv cannot be less than 0");
        //     }
        //     
        //     if (pokemon.SpecialDefenseEv > 255)
        //     {
        //         response.AddError("SpecialDefenseEv", "SpecialDefenseEv cannot be greater than 255");
        //     }
        //     
        //     if (pokemon.SpecialDefenseEv < 0)
        //     {
        //         response.AddError("SpecialDefenseEv", "SpecialDefenseEv cannot be less than 0");
        //     }
        //     
        //     if (pokemon.SpeedEv > 255)
        //     {
        //         response.AddError("SpeedEv", "SpeedEv cannot be greater than 255");
        //     }
        //     
        //     if (pokemon.SpeedEv < 0)
        //     {
        //         response.AddError("SpeedEv", "SpeedEv cannot be less than 0");
        //     }
        //     
        //     var evTotal = pokemon.AttackEv + pokemon.HealthEv + pokemon.DefenseEv
        //               + pokemon.SpeedEv + pokemon.SpecialAttackEv + pokemon.SpecialDefenseEv;
        //     
        //     if (evTotal > 510)
        //     {
        //         response.AddError("Ev Total", "Ev Total cannot be greater than 510");
        //     }
        //     
        //     if (pokemon.HealthIv > 31 || pokemon.HealthIv < 0)
        //     {
        //         response.AddError("Iv", " Iv must be between 0 and 31");
        //     }
        //     
        //     if (pokemon.AttackIv > 31 || pokemon.AttackIv < 0)
        //     {
        //         response.AddError("Iv", " Iv must be between 0 and 31");
        //     }
        //     
        //     if (pokemon.DefenseIv > 31 || pokemon.DefenseIv < 0)
        //     {
        //         response.AddError("Iv", " Iv must be between 0 and 31");
        //     }
        //     
        //     if (pokemon.SpecialAttackIv > 31 || pokemon.SpecialAttackIv < 0)
        //     {
        //         response.AddError("Iv", " Iv must be between 0 and 31");
        //     }
        //     
        //     if (pokemon.SpecialDefenseIv > 31 || pokemon.SpecialDefenseIv < 0)
        //     {
        //         response.AddError("Iv", " Iv must be between 0 and 31");
        //     }
        //     
        //     if (pokemon.SpeedIv > 31 || pokemon.SpeedIv < 0)
        //     {
        //         response.AddError("Iv", " Iv must be between 0 and 31");
        //     }
        //     
        //     var isValidAbility = pokemon.AbilityId == pokemonSpecies.PrimaryAbilityId ||
        //                          pokemon.AbilityId == pokemonSpecies.SecondaryAbilityId ||
        //                          pokemon.AbilityId == pokemonSpecies.HiddenAbilityId;
        //     
        //     if (!isValidAbility)
        //     {
        //         response.AddError("Ability", "Invalid ability");
        //     }
        //     
        //     var isValidItem = _dataContext
        //         .Items
        //         .Any(x => x.Id == pokemon.ItemId);
        //     
        //     if (!isValidItem)
        //     {
        //         response.AddError("Item", "Invalid item");
        //     }
        //     
        //     if (pokemon.Level > 100 || pokemon.Level < 0)
        //     {
        //         response.AddError("Level", "Level must be between 0 and 100");
        //     }
        //     
        //     if (pokemon.Gender > 2 || pokemon.Gender < 0)
        //     {
        //         response.AddError("Gender", "Invalid Gender");
        //     }
        //     
        //     if (response.HasErrors)
        //     {
        //         return BadRequest(response);
        //     }
        //     
        //     var pokemonToCreate = new Pokemon
        //     {
        //         Name = pokemon.Name,
        //         PokemonSpeciesId = pokemon.PokemonSpeciesId,
        //         HealthEv = pokemon.HealthEv,
        //         AttackEv = pokemon.AttackEv,
        //         DefenseEv = pokemon.DefenseEv,
        //         SpecialAttackEv = pokemon.SpecialAttackEv,
        //         SpecialDefenseEv = pokemon.SpecialDefenseEv,
        //         SpeedEv = pokemon.SpeedEv,
        //         HealthIv = pokemon.HealthIv,
        //         AttackIv = pokemon.AttackIv,
        //         DefenseIv = pokemon.DefenseIv,
        //         SpecialAttackIv = pokemon.SpecialAttackIv,
        //         SpecialDefenseIv = pokemon.SpecialDefenseIv,
        //         SpeedIv = pokemon.SpeedIv,
        //         AbilityId = pokemon.AbilityId,
        //         ItemId = pokemon.ItemId,
        //         MoveOneId = pokemon.MoveOneId,
        //         MoveTwoId = pokemon.MoveTwoId,
        //         MoveThreeId = pokemon.MoveThreeId,
        //         MoveFourId = pokemon.MoveFourId,
        //         Level = pokemon.Level,
        //         Experience = pokemon.Experience,
        //         NatureId = pokemon.NatureId,
        //         Gender = pokemon.Gender,
        //         IsShiny = pokemon.IsShiny
        //     };
        //     
        //     _dataContext.Add(pokemonToCreate);
        //     _dataContext.SaveChanges();
        //     
        //     return Created("Pokemon created", response);
        // }

        [HttpPut("{id:int}")]
        public IActionResult Edit(
            [FromRoute] int id,
            [FromBody] PokemonUpdateDto pokemon)
        {
            var response = new Response();

            var isValidPokemon = _dataContext
                .Pokemon
                .Any(x => x.Id == id);
            
            if (pokemon == null || !isValidPokemon)
            {
                response.AddError("", "Entity cannot be null");
                return BadRequest(response);
            }
            
            var pokemonSpecies = _dataContext
                .PokemonSpecies
                .FirstOrDefault(x => x.Id == pokemon.PokemonSpeciesId);

            if (pokemonSpecies == null)
            {
                response.AddError("Pokemon Species", "Pokemon Species not found");
                return NotFound(response);
            }

            pokemon.Name = pokemon.Name.Trim();
            if (string.IsNullOrEmpty(pokemon.Name))
            {
                response.AddError("Name","Name cannot be null or empty");
            }

            if (pokemon.HealthEv > 255)
            {
                response.AddError("HealthEv", "HealthEv cannot be greater than 255");
            }
            
            if (pokemon.HealthEv < 0)
            {
                response.AddError("HealthEv", "HealthEv cannot be less than 0");
            }
            
            if (pokemon.AttackEv > 255)
            {
                response.AddError("AttackEv", "AttackEv cannot be greater than 255");
            }
            
            if (pokemon.AttackEv < 0)
            {
                response.AddError("AttackEv", "AttackEv cannot be less than 0");
            }
            
            if (pokemon.DefenseEv > 255)
            {
                response.AddError("DefenseEv", "DefenseEv cannot be greater than 255");
            }
            
            if (pokemon.DefenseEv < 0)
            {
                response.AddError("DefenseEv", "DefenseEv cannot be less than 0");
            }
            
            if (pokemon.SpecialAttackEv > 255)
            {
                response.AddError("SpecialAttackEv", "SpecialAttackEv cannot be greater than 255");
            }
            
            if (pokemon.SpecialAttackEv < 0)
            {
                response.AddError("SpecialAttackEv", "SpecialAttackEv cannot be less than 0");
            }
            
            if (pokemon.SpecialDefenseEv > 255)
            {
                response.AddError("SpecialDefenseEv", "SpecialDefenseEv cannot be greater than 255");
            }
            
            if (pokemon.SpecialDefenseEv < 0)
            {
                response.AddError("SpecialDefenseEv", "SpecialDefenseEv cannot be less than 0");
            }
            
            if (pokemon.SpeedEv > 255)
            {
                response.AddError("SpeedEv", "SpeedEv cannot be greater than 255");
            }
            
            if (pokemon.SpeedEv < 0)
            {
                response.AddError("SpeedEv", "SpeedEv cannot be less than 0");
            }

            var evTotal = pokemon.AttackEv + pokemon.HealthEv + pokemon.DefenseEv
                      + pokemon.SpeedEv + pokemon.SpecialAttackEv + pokemon.SpecialDefenseEv;

            if (evTotal > 510)
            {
                response.AddError("Ev Total", "Ev Total cannot be greater than 510");
            }

            if (pokemon.HealthIv > 31 || pokemon.HealthIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }
            
            if (pokemon.AttackIv > 31 || pokemon.AttackIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.DefenseIv > 31 || pokemon.DefenseIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.SpecialAttackIv > 31 || pokemon.SpecialAttackIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.SpecialDefenseIv > 31 || pokemon.SpecialDefenseIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            if (pokemon.SpeedIv > 31 || pokemon.SpeedIv < 0)
            {
                response.AddError("Iv", " Iv must be between 0 and 31");
            }

            var isValidAbility = pokemon.AbilityId == pokemonSpecies.PrimaryAbilityId ||
                                 pokemon.AbilityId == pokemonSpecies.SecondaryAbilityId ||
                                 pokemon.AbilityId == pokemonSpecies.HiddenAbilityId;

            if (!isValidAbility)
            {
                response.AddError("Ability", "Invalid ability");
            }

            var isValidItem = _dataContext
                .Items
                .Any(x => x.Id == pokemon.ItemId);

            if (!isValidItem)
            {
                response.AddError("Item", "Invalid item");
            }

            if (pokemon.Level > 100 || pokemon.Level < 0)
            {
                response.AddError("Level", "Level must be between 0 and 100");
            }

            if (pokemon.Gender > 2 || pokemon.Gender < 0)
            {
                response.AddError("Gender", "Invalid Gender");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var pokemonToUpdate = _dataContext
                .Pokemon
                .First(x => x.Id == id);
            
            pokemonToUpdate.Name = pokemon.Name;
            pokemonToUpdate.PokemonSpeciesId = pokemon.PokemonSpeciesId;
            pokemonToUpdate.HealthEv = pokemon.HealthEv;
            pokemonToUpdate.AttackEv = pokemon.AttackEv;
            pokemonToUpdate.DefenseEv = pokemon.DefenseEv;
            pokemonToUpdate.SpecialAttackEv = pokemon.SpecialAttackEv;
            pokemonToUpdate.SpecialDefenseEv = pokemon.SpecialDefenseEv;
            pokemonToUpdate.SpeedEv = pokemon.SpeedEv;
            pokemonToUpdate.HealthIv = pokemon.HealthIv;
            pokemonToUpdate.AttackIv = pokemon.AttackIv;
            pokemonToUpdate.DefenseIv = pokemon.DefenseIv;
            pokemonToUpdate.SpecialAttackIv = pokemon.SpecialAttackIv;
            pokemonToUpdate.SpecialDefenseIv = pokemon.SpecialDefenseIv;
            pokemonToUpdate.SpeedIv = pokemon.SpeedIv;
            pokemonToUpdate.AbilityId = pokemon.AbilityId;
            pokemonToUpdate.ItemId = pokemon.ItemId;
            pokemonToUpdate.MoveOneId = pokemon.MoveOneId;
            pokemonToUpdate.MoveTwoId = pokemon.MoveTwoId;
            pokemonToUpdate.MoveThreeId = pokemon.MoveThreeId;
            pokemonToUpdate.MoveFourId = pokemon.MoveFourId;
            pokemonToUpdate.Level = pokemon.Level;
            pokemonToUpdate.Experience = pokemon.Experience;
            pokemonToUpdate.NatureId = pokemon.NatureId;
            pokemonToUpdate.Gender = pokemon.Gender;
            pokemonToUpdate.IsShiny = pokemon.IsShiny;

            _dataContext.SaveChanges();

            var pokemonGet = new PokemonGetDto
            {
                Id = id,
                Name = pokemon.Name,
                PokemonSpeciesId = pokemon.PokemonSpeciesId,
                HealthEv = pokemon.HealthEv,
                AttackEv = pokemon.AttackEv,
                DefenseEv = pokemon.DefenseEv,
                SpecialAttackEv = pokemon.SpecialAttackEv,
                SpecialDefenseEv = pokemon.SpecialDefenseEv,
                SpeedEv = pokemon.SpeedEv,
                HealthIv = pokemon.HealthIv,
                AttackIv = pokemon.AttackIv,
                DefenseIv = pokemon.DefenseIv,
                SpecialAttackIv = pokemon.SpecialAttackIv,
                SpecialDefenseIv = pokemon.SpecialDefenseIv,
                SpeedIv = pokemon.SpeedIv,
                AbilityId = pokemon.AbilityId,
                ItemId = pokemon.ItemId,
                MoveOneId = pokemon.MoveOneId,
                MoveTwoId = pokemon.MoveTwoId,
                MoveThreeId = pokemon.MoveThreeId,
                MoveFourId = pokemon.MoveFourId,
                Level = pokemon.Level,
                Experience = pokemon.Experience,
                NatureId = pokemon.NatureId,
                Gender = pokemon.Gender,
                IsShiny = pokemon.IsShiny
            };

            response.Data = pokemonGet;

            return Ok(response);
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var pokemon = _dataContext.Pokemon.FirstOrDefault(x => x.Id == id);

            if (pokemon == null)
            {
                response.AddError("id", "Pokemon not found.");
                return NotFound(response);
            }

            _dataContext.Pokemon.Remove(pokemon);
            _dataContext.SaveChanges();

            return Ok(response);
        }

        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var response = new Response();
        
            var speciesOptions = _dataContext
                .PokemonSpecies
                .Select(x => new SpeciesOptionsDto
                {
                    Text = x.Name,
                    Value = x.Id,
                    Key = x.Id
                })
                .ToList();
        
            var abilityOptions = _dataContext
                .Abilities
                .Select(x => new AbilityOptionsDto
                {
                    Text = x.Name,
                    Value = x.Id,
                    Key = x.Id
                })
                .ToList();
            
            var moveOptions = _dataContext
                .Moves
                .Select(x => new MoveOptionsDto
                {
                    Text = x.Name,
                    Value = x.Id,
                    Key = x.Id
                })
                .ToList();
            
            var itemOptions = _dataContext
                .Items
                .Select(x => new ItemOptionsDto
                {
                    Text = x.Name,
                    Value = x.Id,
                    Key = x.Id
                })
                .ToList();
            
            var natureOptions = _dataContext
                .Natures
                .Select(x => new NatureOptionsDto
                {
                    Text = x.Name,
                    Value = x.Id,
                    Key = x.Id
                })
                .ToList();

            var optionsToReturn = new PokemonOptionsDto
            {
                Abilities = abilityOptions,
                Items = itemOptions,
                Moves = moveOptions,
                Natures = natureOptions,
                Species = speciesOptions
            };

            response.Data = optionsToReturn;
            
            return Ok(response);
        }
    }
}