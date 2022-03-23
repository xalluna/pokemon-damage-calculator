using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class PType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PokemonSpecies> PokemonSpecies { get; set; }
        
        public List<Move> Moves { get; set; }
        
    }
    
    public class TypeCreateDto
    {
        public string Name { get; set; }

    }
    
    public class TypeGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class TypeUpdateDto
    {
        public string Name { get; set; }
    }
}