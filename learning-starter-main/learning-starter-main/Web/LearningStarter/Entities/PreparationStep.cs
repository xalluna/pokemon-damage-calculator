using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class PreparationStep
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<Order> Orders { get; set; }
        
    }
    
    public class PreparationStepCreateDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class PreparationStepGetDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class PreparationStepUpdateDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}