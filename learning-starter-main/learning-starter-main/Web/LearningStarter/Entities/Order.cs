using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class Order
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        public int PreparationStepId { get; set; }
        
        public PreparationStep PreparationStep { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public List<OrderProduct> OrderProducts { get; set; }
        
    }
    
    public class OrderCreateDto
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }

        public int PreparationStepId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
    
    public class OrderGetDto
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }

        public int PreparationStepId { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
    
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }
    
        public int PreparationStepId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}