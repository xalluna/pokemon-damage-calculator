using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Type
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // public List<PokemonSpecies> PokemonSpeciesList { get; set; }
        //
        // public List<Move> Moves { get; set; }
        
    }
    
    public class TypeCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class TypeGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class TypeUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}