using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningStarter.Entities
{
    [NotMapped]
    public class MoveCategory
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        // public List<Move> Moves { get; set; }
    }
    
    public class MoveCategoryCreateDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class MoveCategoryGetDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class MoveCategoryUpdateDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}