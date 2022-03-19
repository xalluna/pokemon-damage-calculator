using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Move
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public Type Type { get; set; }

        // public int MoveCategory { get; set; }
        
        public bool? MoveCategory { get; set; }

        // public List<MoveCategory> MoveCategories { get; set; }

        public int BasePower { get; set; }

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
    
    public class MoveCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public bool? MoveCategory { get; set; }

        public int BasePower { get; set; }

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

        public bool? MoveCategory { get; set; }

        public int BasePower { get; set; }

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
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public bool? MoveCategory { get; set; }

        public int BasePower { get; set; }

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
}