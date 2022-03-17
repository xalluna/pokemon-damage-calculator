using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class PokemonSpecies
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BaseHealth { get; set; }
        
        public int BaseAttack { get; set; }

        public int BaseDefense { get; set; }

        public int BaseSpecialAttack { get; set; }
        
        public int BaseSpecialDefense { get; set; }

        public int BaseSpeed { get; set; }

        public int PrimaryTypeId { get; set; }
        
        public Type PrimaryType { get; set; }

        // public int? SecondaryTypeId { get; set; }
        //
        // public Type? SecondaryType { get; set; }
        //
        public int PrimaryAbilityId { get; set; }
        
        public Ability PrimaryAbility { get; set; }
        //
        // public int? SecondaryAbilityId { get; set; }
        //
        // public Ability? SecondaryAbility { get; set; }
        //
        // public int? HiddenAbilityId { get; set; }
        //
        // public Ability? HiddenAbility { get; set; }

        public int ExperienceCurveId { get; set; }

        public ExperienceCurve ExperienceCurve { get; set; }

        // public List<Move> MoveLearnSet { get; set; }

    }
    
    public class PokemonSpeciesCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BaseHealth { get; set; }
        
        public int BaseAttack { get; set; }

        public int BaseDefense { get; set; }

        public int BaseSpecialAttack { get; set; }
        
        public int BaseSpecialDefense { get; set; }

        public int BaseSpeed { get; set; }

        public int PrimaryTypeId { get; set; }
        // public int? SecondaryTypeId { get; set; }
  
        public int PrimaryAbilityId { get; set; }

        // public int? SecondaryAbilityId { get; set; }
        //
        // public int? HiddenAbilityId { get; set; }
        public int ExperienceCurveId { get; set; }
        // public List<MoveGetDto> MoveLearnSet { get; set; }

    }
    
    public class PokemonSpeciesGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BaseHealth { get; set; }
        
        public int BaseAttack { get; set; }

        public int BaseDefense { get; set; }

        public int BaseSpecialAttack { get; set; }
        
        public int BaseSpecialDefense { get; set; }

        public int BaseSpeed { get; set; }

        public int PrimaryTypeId { get; set; }
        
        //public int? SecondaryTypeId { get; set; }
  
        public int PrimaryAbilityId { get; set; }

        // public int? SecondaryAbilityId { get; set; }
        //
        // public int? HiddenAbilityId { get; set; }
        
        public int ExperienceCurveId { get; set; }

        // public List<MoveGetDto> MoveLearnSet { get; set; }

    }
    
    public class PokemonSpeciesUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BaseHealth { get; set; }
        
        public int BaseAttack { get; set; }

        public int BaseDefense { get; set; }

        public int BaseSpecialAttack { get; set; }
        
        public int BaseSpecialDefense { get; set; }

        public int BaseSpeed { get; set; }

        public int PrimaryTypeId { get; set; }
        
        //public int? SecondaryTypeId { get; set; }
  
        public int PrimaryAbilityId { get; set; }

        // public int? SecondaryAbilityId { get; set; }
        //
        // public int? HiddenAbilityId { get; set; }
        
        public int ExperienceCurveId { get; set; }

        // public List<MoveGetDto> MoveLearnSet { get; set; }

    }
}