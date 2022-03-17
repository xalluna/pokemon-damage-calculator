using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
    
    public class ProductTypeGetDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class ProductTypeUpdateDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
    
    public class ProductTypeCreateDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}