using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class Ability
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PokemonSpecies> PokemonSpecies { get; set; }
        
        public List<Pokemon> Pokemon { get; set; }

    }
    
    public class AbilityCreateDto
    {
        public string Name { get; set; }

    }
    
    public class AbilityGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    
    public class AbilityUpdateDto
    { 
        public string Name { get; set; }

    }
}