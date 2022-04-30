using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<Pokemon> Pokemon { get; set; }
    }
    
    public class ItemCreateDto
    {
        public string Name { get; set; }
    }
    
    public class ItemGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class ItemUpdateDto
    {
        public string Name { get; set; }
    }
    
    public class ItemOptionsDto
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}