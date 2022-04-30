using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Move
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public PType PType { get; set; }

        public int MoveCategoryId { get; set; }

        public int? BasePower { get; set; }

        public int Accuracy { get; set; }
        
        public int PowerPoints { get; set; }

        public int SpeedPriority { get; set; }

        public bool? IsContactOnHit { get; set; }

        public bool? IsSoundBased { get; set; }
        
        public bool? IsPunchBased { get; set; }

        public bool? IsAffectedByGravity { get; set; }
        
        public bool? IsDefrostOnUse { get; set; }

        public bool? IsBlockedByProtect { get; set; }
        
        public virtual List<Pokemon> Pokemon { get; set; }
    }
    
    public class MoveCreateDto
    {
        public string Name { get; set; }

        public int TypeId { get; set; }

        public int MoveCategoryId { get; set; }

        public int? BasePower { get; set; }

        public int Accuracy { get; set; }
        
        public int PowerPoints { get; set; }

        public int SpeedPriority { get; set; }

        public bool? IsContactOnHit { get; set; }

        public bool? IsSoundBased { get; set; }
        
        public bool? IsPunchBased { get; set; }

        public bool? IsAffectedByGravity { get; set; }
        
        public bool? IsDefrostOnUse { get; set; }

        public bool? IsBlockedByProtect { get; set; }
    }
    
    public class MoveGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public int MoveCategoryId { get; set; }

        public int? BasePower { get; set; }

        public int Accuracy { get; set; }
        
        public int PowerPoints { get; set; }

        public int SpeedPriority { get; set; }

        public bool? IsContactOnHit { get; set; }

        public bool? IsSoundBased { get; set; }
        
        public bool? IsPunchBased { get; set; }

        public bool? IsAffectedByGravity { get; set; }
        
        public bool? IsDefrostOnUse { get; set; }

        public bool? IsBlockedByProtect { get; set; }
    }
    
    public class MoveUpdateDto
    {
        public string Name { get; set; }

        public int TypeId { get; set; }

        public int MoveCategoryId { get; set; }

        public int? BasePower { get; set; }

        public int Accuracy { get; set; }
        
        public int PowerPoints { get; set; }

        public int SpeedPriority { get; set; }

        public bool? IsContactOnHit { get; set; }

        public bool? IsSoundBased { get; set; }
        
        public bool? IsPunchBased { get; set; }

        public bool? IsAffectedByGravity { get; set; }
        
        public bool? IsDefrostOnUse { get; set; }

        public bool? IsBlockedByProtect { get; set; }
    }
    
    public class MoveOptionsDto
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public int Key { get; set; }
    }
}