//This type uses a generic (<T>).  For more information on generics see: https://www.typescriptlang.org/docs/handbook/2/generics.html
//You probably wont need this for the scope of this class :)
export type Nullable<T> = T | null;

export type ApiResponse<T> = {
  data: T;
  errors: Error[];
  hasErrors: boolean;
};

export type Error = {
  property: string;
  message: string;
};

export type AnyObject = {
  [index: string]: any;
};

export type UserDto = {
  id: number;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
};

export type User = {
  id: number;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
};

export type AbilityGetDto = {
  id: number;
  name: string;
};

export type AbilityCreateDto = {
  name: string;
};

export type ItemGetDto = {
  id: number;
  name: string;
};

export type ItemCreateDto = {
  name: string;
};

export type ExperienceCurveGetDto = {
  id: number;
  name: string;
};

export type ExperienceCurveCreateDto = {
  name: string;
};

export type TypeGetDto = {
  id: number;
  name: string;
};

export type TypeCreateDto = {
  name: string;
};

export type NatureGetDto = {
  id: number;
  name: string;
};

export type NatureCreateDto = {
  name: string;
};

export type MoveGetDto = {
  id: number;
  name: string;
  typeId: number;
  moveCategoryId: number;
  basePower: number;
  accuracy: number;
  powerPoints: number;
  speedPriority: number;
  isContactOnHit: boolean;
  isSoundBased: boolean;
  isPunchBased: boolean;
  isAffectedByGravity: boolean;
  isDefrostOnUse: boolean;
  isBlockedByProtect: boolean;
};

export type MoveCreateDto = {};

export type PokemonSpeciesGetDto = {
  id: number;
  name: string;
  baseHealth: number;
  baseAttack: number;
  baseDefense: number;
  baseSpecialAttack: number;
  baseSpecialDefense: number;
  baseSpeed: number;
  primaryTypeId: number;
  secondaryTypeId: Nullable<number>;
  primaryAbilityId: number;
  secondaryAbilityId: Nullable<number>;
  hiddenAbilityId: Nullable<number>;
  experiencecurveId: number;
};

export type PokemonSpeciesCreateDto = {};

export type PokemonGetDto = {
  id: number;
  name: string;
  pokemonSpeciesId: number;
  healthEv: number;
  attackEv: number;
  defenseEv: number;
  specialAttackEv: number;
  specialDefenseEv: number;
  speedEv: number;
  healthIv: number;
  attackIv: number;
  defenseIv: number;
  specialAttackIv: number;
  specialDefenseIv: number;
  speedIv: number;
  abilityId: number;
  ItemId: number;
  moveOneId: number;
  moveTwoId: number;
  moveThreeId: number;
  moveFourId: number;
  level: number;
  experience: number;
  natureId: number;
  gender: number;
  isShiny: Nullable<boolean>;
};

export type PokemonListDto = {
  id: number;
  name: string;
  pokemonSpecies: string;
  healthEv: number;
  attackEv: number;
  defenseEv: number;
  specialAttackEv: number;
  specialDefenseEv: number;
  speedEv: number;
  healthIv: number;
  attackIv: number;
  defenseIv: number;
  specialAttackIv: number;
  specialDefenseIv: number;
  speedIv: number;
  ability: string;
  item: string;
  moveOne: string;
  moveTwo: string;
  moveThree: string;
  moveFour: string;
  level: number;
  experience: number;
  nature: string;
  gender: string;
  isShiny: Nullable<boolean>;
};

export type PokemonCreateDto = {};
