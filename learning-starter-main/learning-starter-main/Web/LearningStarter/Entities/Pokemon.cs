using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int PokemonSpeciesId { get; set; }
        
        public PokemonSpecies PokemonSpecies { get; set; }
        
        public int HealthEv {get; set; }
        
        public int AttackEv {get; set; }

        public int DefenseEv {get; set; }

        public int SpecialAttackEv {get; set; }

        public int SpecialDefenseEv {get; set; }

        public int SpeedEv{get; set; }

        public int HealthIv {get; set; }

        public int AttackIv {get; set; }

        public int DefenseIv {get; set; }

        public int SpecialAttackIv {get; set; }

        public int SpecialDefenseIv {get; set; }

        public int SpeedIv {get; set; }
        
        public int AbilityId { get; set; }

        public Ability Ability { get; set; }
        
        public int ItemId { get; set; }

        public Item Item { get; set; }
        
        public int MoveOneId {get; set; }
        
        public int? MoveTwoId {get; set; }
        
        public int? MoveThreeId {get; set; }
        
        public int? MoveFourId {get; set; }
        
        public List<Move> Moves { get; set; }
        
        public int Level { get; set; }
        
        public int Experience { get; set; }
        
        public int NatureId { get; set; }
        
        public Nature Nature { get; set; }
        
        public int Gender { get; set; }
        
        public bool? IsShiny { get; set; }
    }
    
    public class PokemonCreateDto
    {
        public string Name { get; set; }
        
        public int PokemonSpeciesId { get; set; }
        
        public int HealthEv {get; set; }
        
        public int AttackEv {get; set; }

        public int DefenseEv {get; set; }

        public int SpecialAttackEv {get; set; }

        public int SpecialDefenseEv {get; set; }

        public int SpeedEv {get; set; }

        public int HealthIv {get; set; }

        public int AttackIv {get; set; }

        public int DefenseIv {get; set; }

        public int SpecialAttackIv {get; set; }

        public int SpecialDefenseIv {get; set; }

        public int SpeedIv {get; set; }
        
        public int AbilityId { get; set; }

        public int ItemId { get; set; }

        public int MoveOneId {get; set; }
        
        public int? MoveTwoId {get; set; }
        
        public int? MoveThreeId {get; set; }
        
        public int? MoveFourId {get; set; }
        
        public int Level { get; set; }
        
        public int Experience { get; set; }
        
        public int NatureId { get; set; }
        
        public int Gender { get; set; }
        
        public bool? IsShiny { get; set; }
    }
    
    public class PokemonGetDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int PokemonSpeciesId { get; set; }
        
        public int HealthEv {get; set; }
        
        public int AttackEv {get; set; }

        public int DefenseEv {get; set; }

        public int SpecialAttackEv {get; set; }

        public int SpecialDefenseEv {get; set; }

        public int SpeedEv{get; set; }

        public int HealthIv {get; set; }

        public int AttackIv {get; set; }

        public int DefenseIv {get; set; }

        public int SpecialAttackIv {get; set; }

        public int SpecialDefenseIv {get; set; }

        public int SpeedIv {get; set; }
        
        public int AbilityId { get; set; }

        public int ItemId { get; set; }

        public int MoveOneId {get; set; }
        
        public int? MoveTwoId {get; set; }
        
        public int? MoveThreeId {get; set; }
        
        public int? MoveFourId {get; set; }
        
        public int Level { get; set; }
        
        public int Experience { get; set; }
        
        public int NatureId { get; set; }
        
        public int Gender { get; set; }
        
        public bool? IsShiny { get; set; }
    }
    
    public class PokemonUpdateDto
    {
        public string Name { get; set; }
        public int PokemonSpeciesId { get; set; }
        public int HealthEv {get; set; }
        public int AttackEv {get; set; }
        public int DefenseEv {get; set; }
        public int SpecialAttackEv {get; set; }
        public int SpecialDefenseEv {get; set; }
        public int SpeedEv{get; set; }
        public int HealthIv {get; set; }
        public int AttackIv {get; set; }
        public int DefenseIv {get; set; }
        public int SpecialAttackIv {get; set; }
        public int SpecialDefenseIv {get; set; }
        public int SpeedIv {get; set; }
        public int AbilityId { get; set; }
        public int ItemId { get; set; }
        public int MoveOneId {get; set; }
        public int? MoveTwoId {get; set; }
        public int? MoveThreeId {get; set; }
        public int? MoveFourId {get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int NatureId { get; set; }
        public int Gender { get; set; }
        public bool? IsShiny { get; set; }
    }
    
    public class PokemonListDto
    {
        public int Id { get; set; }
        public PokemonGetDto Pokemon { get; set; }
        public string Name { get; set; }
        public string PokemonSpecies { get; set; }
        public int HealthEv {get; set; }
        public int AttackEv {get; set; }
        public int DefenseEv {get; set; }
        public int SpecialAttackEv {get; set; }
        public int SpecialDefenseEv {get; set; }
        public int SpeedEv{get; set; }
        public int HealthIv {get; set; }
        public int AttackIv {get; set; }
        public int DefenseIv {get; set; }
        public int SpecialAttackIv {get; set; }
        public int SpecialDefenseIv {get; set; }
        public int SpeedIv {get; set; }
        public string Ability { get; set; }
        public string Item { get; set; }
        public string MoveOne {get; set; }
        public string? MoveTwo {get; set; }
        public string? MoveThree {get; set; }
        public string? MoveFour {get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public string Nature { get; set; }
        public string Gender { get; set; }
        public bool? IsShiny { get; set; }
    }

    public class PokemonFormDto
    {
        public string Name { get; set; }
        public string PokemonSpecies { get; set; }
        public string HealthEv {get; set; }
        public string AttackEv {get; set; }
        public string DefenseEv {get; set; }
        public string SpecialAttackEv {get; set; }
        public string SpecialDefenseEv {get; set; }
        public string SpeedEv{get; set; }
        public string HealthIv {get; set; }
        public string AttackIv {get; set; }
        public string DefenseIv {get; set; }
        public string SpecialAttackIv {get; set; }
        public string SpecialDefenseIv {get; set; }
        public string SpeedIv {get; set; }
        public string Ability { get; set; }
        public string Item { get; set; }
        public string MoveOne {get; set; }
        public string? MoveTwo {get; set; }
        public string? MoveThree {get; set; }
        public string? MoveFour {get; set; }
        public string Level { get; set; }
        public string Experience { get; set; }
        public string Nature { get; set; }
        public string Gender { get; set; }
        public string IsShiny { get; set; }
    }
    
    class PokemonOptionsDto
    {
        public List<SpeciesOptionsDto> Species{ get; set; }
        public List<AbilityOptionsDto> Abilities{ get; set; }
        public List<ItemOptionsDto> Items{ get; set; }
        public List<MoveOptionsDto> Moves{ get; set; }
        public List<NatureOptionsDto> Natures{ get; set; }
    }

    class PokemonBattleDto
    {
        public PokemonGetDto Pokemon { get; set; }
        public PokemonSpeciesGetDto PokemonSpecies { get; set; }
        // public AbilityGetDto Ability { get; set; }
        // public ItemGetDto Item { get; set; }
        public MoveGetDto MoveOne { get; set; }
        public MoveGetDto? MoveTwo { get; set; }
        public MoveGetDto? MoveThree { get; set; }
        public MoveGetDto? MoveFour { get; set; }
        // public NatureGetDto Nature { get; set; }
    }

    class PokemonBattleGroupDto
    {
        public List<PokemonBattleDto> Pokemon { get; set; }
    }
}