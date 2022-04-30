using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Nature
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Pokemon> PokemonList { get; set; }

    }
    
    public class NatureCreateDto
    {
        public string Name { get; set; }

    }
    
    public class NatureGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    
    public class NatureUpdateDto
    { 
        public string Name { get; set; }

    }
    
    public class NatureOptionsDto
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}