using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class ExperienceCurve
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PokemonSpecies> PokemonSpecies { get; set; }
        
    }
    
    public class ExperienceCurveCreateDto
    {
        public string Name { get; set; }

    }
    
    public class ExperienceCurveGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class ExperienceCurveUpdateDto
    {
        public string Name { get; set; }
    }
}