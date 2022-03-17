using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        
        public Product Product { get; set; }
        
        public int OrderId { get; set; }
        
        public Order Order { get; set; }
    }
    
    public class OrderProductCreateDto
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        
        public int OrderId { get; set; }
    }
    
    public class OrderProductGetDto
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        
        public int OrderId { get; set; }
    }
    
    public class OrderProductUpdateDto
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        
        public int OrderId { get; set; }
    }
}