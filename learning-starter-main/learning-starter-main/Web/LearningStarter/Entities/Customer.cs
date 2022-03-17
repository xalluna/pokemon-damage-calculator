using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;

namespace LearningStarter.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public List<Order> Orders { get; set; }

    }
    
    public class CustomerCreateDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
    public class CustomerGetDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
    
    
    public class CustomerUpdateDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}