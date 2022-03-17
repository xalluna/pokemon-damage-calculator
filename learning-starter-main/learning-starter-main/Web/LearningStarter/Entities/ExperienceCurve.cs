using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class ExperienceCurve
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // public List<PokemonSpecies> PokemonSpeciesList { get; set; }
        
    }
    
    public class ExperienceCurveCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class ExperienceCurveGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    
    public class ExperienceCurveUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}