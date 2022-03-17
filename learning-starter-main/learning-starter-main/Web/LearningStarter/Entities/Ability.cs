using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class Ability
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public List<PokemonSpecies> PokemonSpeciesList { get; set; }
        
        //public List<Pokemon> PokemonList { get; set; }

    }
    
    public class AbilityCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class AbilityGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class AbilityUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}