import axios from "axios";
import React, { useEffect, useState } from "react";
import {
  Button,
  ButtonGroup,
  Container,
  Divider,
  Dropdown,
  Header,
} from "semantic-ui-react";
import { baseUrl } from "../../constants/env-vars";
import {
  ApiResponse,
  MoveGetDto,
  PokemonBattleDto,
  PokemonBattleGroup,
  PokemonGetDto,
  PokemonOptionsDto,
} from "../../constants/types";
import "./damage-calculator-page.css";

export const DamageCalculatorPage = () => {
  const [pokemon, setPokemon] = useState<PokemonBattleGroup>();
  const [options, setOptions] = useState<PokemonOptionsDto>();
  const [currentPokemon1, setCurrentPokemon1] = useState<PokemonBattleDto>();
  const [pokemon1Stats, setPokemon1Stats] = useState<number[]>();
  const [currentPokemon2, setCurrentPokemon2] = useState<PokemonBattleDto>();
  const [pokemon2Stats, setPokemon2Stats] = useState<number[]>();
  const [damage, setDamage] = useState<number[]>();
  const [move, setMove] = useState<MoveGetDto>();

  const fetchPokemon = async () => {
    const response = await axios.get<ApiResponse<PokemonBattleGroup>>(
      `${baseUrl}/api/pokemon/battle`
    );
    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      setPokemon(response.data.data);
    }
  };

  const fetchPokemonOptions = async () => {
    const response = await axios.get<ApiResponse<PokemonOptionsDto>>(
      `${baseUrl}/api/pokemon/options`
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
        return;
      });
    }

    setOptions(response.data.data);
  };

  const setPokemon1 = (id) => {
    if (pokemon) {
      pokemon.pokemon.forEach((child) => {
        if (child.pokemon.id === id) {
          setCurrentPokemon1(child);
          var natureMultiplier = getNatureMultiplier(child.pokemon.natureId);
          setPokemon1Stats([
            calcHp(
              child.pokemonSpecies.baseHealth,
              child.pokemon.healthIv,
              child.pokemon.healthEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseAttack,
              child.pokemon.attackIv,
              child.pokemon.attackEv,
              child.pokemon.level
            ) * natureMultiplier[0],
            calcStat(
              child.pokemonSpecies.baseDefense,
              child.pokemon.defenseIv,
              child.pokemon.defenseEv,
              child.pokemon.level
            ) * natureMultiplier[1],
            calcStat(
              child.pokemonSpecies.baseSpecialAttack,
              child.pokemon.specialAttackIv,
              child.pokemon.specialAttackEv,
              child.pokemon.level
            ) * natureMultiplier[2],
            calcStat(
              child.pokemonSpecies.baseSpecialDefense,
              child.pokemon.specialDefenseIv,
              child.pokemon.specialDefenseEv,
              child.pokemon.level
            ) * natureMultiplier[3],
            calcStat(
              child.pokemonSpecies.baseSpeed,
              child.pokemon.speedIv,
              child.pokemon.speedEv,
              child.pokemon.level
            ) * natureMultiplier[4],
          ]);
          return;
        }
      });
    }
  };

  const setPokemon2 = (id) => {
    if (pokemon) {
      pokemon.pokemon.forEach((child) => {
        if (child.pokemon.id === id) {
          setCurrentPokemon2(child);
          var natureMultiplier = getNatureMultiplier(child.pokemon.natureId);
          setPokemon2Stats([
            calcHp(
              child.pokemonSpecies.baseHealth,
              child.pokemon.healthIv,
              child.pokemon.healthEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseAttack,
              child.pokemon.attackIv,
              child.pokemon.attackEv,
              child.pokemon.level
            ) * natureMultiplier[0],
            calcStat(
              child.pokemonSpecies.baseDefense,
              child.pokemon.defenseIv,
              child.pokemon.defenseEv,
              child.pokemon.level
            ) * natureMultiplier[1],
            calcStat(
              child.pokemonSpecies.baseSpecialAttack,
              child.pokemon.specialAttackIv,
              child.pokemon.specialAttackEv,
              child.pokemon.level
            ) * natureMultiplier[2],
            calcStat(
              child.pokemonSpecies.baseSpecialDefense,
              child.pokemon.specialDefenseIv,
              child.pokemon.specialDefenseEv,
              child.pokemon.level
            ) * natureMultiplier[3],
            calcStat(
              child.pokemonSpecies.baseSpeed,
              child.pokemon.speedIv,
              child.pokemon.speedEv,
              child.pokemon.level
            ) * natureMultiplier[4],
          ]);
          return;
        }
      });
    }
  };

  const calcHp = (statBase, statIv, statEv, level) => {
    return (
      ((2 * statBase + statIv + statEv / 4.0) * level) / 100.0 + level + 10
    );
  };

  const calcStat = (statBase, statIv, statEv, level) => {
    return ((2 * statBase + statIv + statEv / 4.0) * level) / 100.0 + 5;
  };

  //returns 1.1 or .9
  const getNatureMultiplier = (natureId) => {
    var natureMultiplier = [1, 1, 1, 1, 1]; //[atk, def, spA, spD, spe]

    if (
      natureId === 2 ||
      natureId === 7 ||
      natureId === 9 ||
      natureId === 20 ||
      natureId === 24
    ) {
      return natureMultiplier;
    }

    if (
      natureId === 1 ||
      natureId === 4 ||
      natureId === 14 ||
      natureId === 18
    ) {
      natureMultiplier[0] = 1.1;
    }

    if (
      natureId === 3 ||
      natureId === 11 ||
      natureId === 13 ||
      natureId === 22
    ) {
      natureMultiplier[1] = 1.1;
    }

    if (
      natureId === 15 ||
      natureId === 16 ||
      natureId === 19 ||
      natureId === 21
    ) {
      natureMultiplier[2] = 1.1;
    }

    if (natureId === 5 || natureId === 6 || natureId === 8 || natureId === 23) {
      natureMultiplier[3] = 1.1;
    }

    if (
      natureId === 10 ||
      natureId === 12 ||
      natureId === 17 ||
      natureId === 25
    ) {
      natureMultiplier[4] = 1.1;
    }

    if (
      natureId === 3 ||
      natureId === 5 ||
      natureId === 16 ||
      natureId === 25
    ) {
      natureMultiplier[0] = 0.9;
    }

    if (
      natureId === 8 ||
      natureId === 10 ||
      natureId === 14 ||
      natureId === 15
    ) {
      natureMultiplier[1] = 0.9;
    }

    if (
      natureId === 1 ||
      natureId === 6 ||
      natureId === 11 ||
      natureId === 12
    ) {
      natureMultiplier[2] = 0.9;
    }

    if (
      natureId === 13 ||
      natureId === 17 ||
      natureId === 18 ||
      natureId === 21
    ) {
      natureMultiplier[3] = 0.9;
    }

    if (
      natureId === 4 ||
      natureId === 19 ||
      natureId === 22 ||
      natureId === 23
    ) {
      natureMultiplier[4] = 0.9;
    }

    return natureMultiplier;
  };

  const damageCalculation = (
    move: MoveGetDto,
    user: PokemonBattleDto,
    target: PokemonBattleDto,
    stats: number[]
  ) => {
    var values = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

    var weaknessMultiplier = getWeakness(move, target);
    var stab =
      user.pokemonSpecies.primaryTypeId === move.typeId ||
      user.pokemonSpecies.secondaryTypeId === move.typeId
        ? 1.5
        : 1;
    var A = move.moveCategoryId === 1 ? stats[1] : stats[3];
    var D = move.moveCategoryId === 1 ? stats[2] : stats[4];

    for (let i = 0, j = 16; i < values.length; i++, j--) {
      values[j] =
        ((((2 * user.pokemon.level) / 5.0 + 2) * move.basePower * (A / D)) /
          50.0) *
        stab *
        weaknessMultiplier *
        ((100 - i) / 100.0);
    }
    setMove(move);
    setDamage(values);
  };

  const percentCalculation = (
    move: MoveGetDto,
    user: PokemonBattleDto,
    target: PokemonBattleDto,
    stats: number[],
    roll: number
  ) => {
    var weaknessMultiplier = getWeakness(move, target);
    var stab =
      user.pokemonSpecies.primaryTypeId === move.typeId ||
      user.pokemonSpecies.secondaryTypeId === move.typeId
        ? 1.5
        : 1;
    var A = move.moveCategoryId === 1 ? stats[1] : stats[3];
    var D = move.moveCategoryId === 1 ? stats[2] : stats[4];

    var damage =
      ((((2 * user.pokemon.level) / 5.0 + 2) * move.basePower * (A / D)) /
        50.0) *
      stab *
      weaknessMultiplier *
      ((100 - roll) / 100.0);

    return (
      (damage /
        calcHp(
          target.pokemonSpecies.baseHealth,
          target.pokemon.healthIv,
          target.pokemon.healthEv,
          target.pokemon.level
        )) *
      100
    ).toFixed(1);
  };

  const getWeakness = (move: MoveGetDto, target: PokemonBattleDto) => {
    var weaknessMultiplier = 1;

    if (move.typeId === 1) {
      if (
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 14 ||
        target.pokemonSpecies.secondaryTypeId === 14
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 2) {
      if (
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 6 ||
        target.pokemonSpecies.primaryTypeId === 13 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 6 ||
        target.pokemonSpecies.secondaryTypeId === 13 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 3 ||
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 15
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 3 ||
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 15
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 3) {
      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 11
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 11
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 3 ||
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 15
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 3 ||
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 15
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 4) {
      if (
        target.pokemonSpecies.primaryTypeId === 3 ||
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 11
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 3 ||
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 11
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 6 ||
        target.pokemonSpecies.primaryTypeId === 7 ||
        target.pokemonSpecies.primaryTypeId === 12 ||
        target.pokemonSpecies.primaryTypeId === 15 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 6 ||
        target.pokemonSpecies.secondaryTypeId === 7 ||
        target.pokemonSpecies.secondaryTypeId === 12 ||
        target.pokemonSpecies.secondaryTypeId === 15 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 5) {
      if (
        target.pokemonSpecies.primaryTypeId === 3 ||
        target.pokemonSpecies.primaryTypeId === 7
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 3 ||
        target.pokemonSpecies.secondaryTypeId === 7
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 5 ||
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 15
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 5 ||
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 15
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 10
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 6) {
      if (
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 9 ||
        target.pokemonSpecies.primaryTypeId === 16
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 9 ||
        target.pokemonSpecies.secondaryTypeId === 16
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 7 ||
        target.pokemonSpecies.primaryTypeId === 8 ||
        target.pokemonSpecies.primaryTypeId === 12 ||
        target.pokemonSpecies.primaryTypeId === 14 ||
        target.pokemonSpecies.primaryTypeId === 17 ||
        target.pokemonSpecies.primaryTypeId === 18
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 8 ||
        target.pokemonSpecies.secondaryTypeId === 14 ||
        target.pokemonSpecies.secondaryTypeId === 7 ||
        target.pokemonSpecies.secondaryTypeId === 12 ||
        target.pokemonSpecies.secondaryTypeId === 18 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 7) {
      if (
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 6 ||
        target.pokemonSpecies.primaryTypeId === 8
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 6 ||
        target.pokemonSpecies.secondaryTypeId === 8
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 5 ||
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 5 ||
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 8) {
      if (
        target.pokemonSpecies.primaryTypeId === 1 ||
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 13 ||
        target.pokemonSpecies.primaryTypeId === 16 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 1 ||
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 13 ||
        target.pokemonSpecies.secondaryTypeId === 16 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 6 ||
        target.pokemonSpecies.primaryTypeId === 7 ||
        target.pokemonSpecies.primaryTypeId === 9 ||
        target.pokemonSpecies.primaryTypeId === 12 ||
        target.pokemonSpecies.primaryTypeId === 18
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 6 ||
        target.pokemonSpecies.secondaryTypeId === 7 ||
        target.pokemonSpecies.secondaryTypeId === 9 ||
        target.pokemonSpecies.secondaryTypeId === 12 ||
        target.pokemonSpecies.secondaryTypeId === 18
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 14 ||
        target.pokemonSpecies.secondaryTypeId === 14
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 9) {
      if (
        target.pokemonSpecies.primaryTypeId === 8 ||
        target.pokemonSpecies.primaryTypeId === 12
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 8 ||
        target.pokemonSpecies.secondaryTypeId === 12
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 9 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 9 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 16 ||
        target.pokemonSpecies.secondaryTypeId === 16
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 10) {
      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 6 ||
        target.pokemonSpecies.primaryTypeId === 7 ||
        target.pokemonSpecies.primaryTypeId === 13
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 6 ||
        target.pokemonSpecies.secondaryTypeId === 7 ||
        target.pokemonSpecies.secondaryTypeId === 13
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 11 ||
        target.pokemonSpecies.primaryTypeId === 8
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 8 ||
        target.pokemonSpecies.secondaryTypeId === 11
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 11) {
      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 5 ||
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 17 ||
        target.pokemonSpecies.primaryTypeId === 12
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 5 ||
        target.pokemonSpecies.secondaryTypeId === 12 ||
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 6
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 6
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 12) {
      if (
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 18
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 18
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 10 ||
        target.pokemonSpecies.primaryTypeId === 11 ||
        target.pokemonSpecies.primaryTypeId === 12 ||
        target.pokemonSpecies.primaryTypeId === 14
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 10 ||
        target.pokemonSpecies.secondaryTypeId === 11 ||
        target.pokemonSpecies.secondaryTypeId === 12 ||
        target.pokemonSpecies.secondaryTypeId === 14
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 17 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 13) {
      if (
        target.pokemonSpecies.primaryTypeId === 4 ||
        target.pokemonSpecies.primaryTypeId === 6 ||
        target.pokemonSpecies.primaryTypeId === 11 ||
        target.pokemonSpecies.primaryTypeId === 15
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 4 ||
        target.pokemonSpecies.secondaryTypeId === 6 ||
        target.pokemonSpecies.secondaryTypeId === 11 ||
        target.pokemonSpecies.secondaryTypeId === 15
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 3 ||
        target.pokemonSpecies.primaryTypeId === 13 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 3 ||
        target.pokemonSpecies.secondaryTypeId === 13 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 14) {
      if (
        target.pokemonSpecies.primaryTypeId === 9 ||
        target.pokemonSpecies.primaryTypeId === 14
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 9 ||
        target.pokemonSpecies.secondaryTypeId === 14
      ) {
        weaknessMultiplier *= 2;
      }

      if (target.pokemonSpecies.primaryTypeId === 16) {
        weaknessMultiplier /= 2;
      }

      if (target.pokemonSpecies.secondaryTypeId === 16) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 1 ||
        target.pokemonSpecies.secondaryTypeId === 1
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 15) {
      if (target.pokemonSpecies.primaryTypeId === 15) {
        weaknessMultiplier *= 2;
      }

      if (target.pokemonSpecies.secondaryTypeId === 15) {
        weaknessMultiplier *= 2;
      }

      if (target.pokemonSpecies.primaryTypeId === 17) {
        weaknessMultiplier /= 2;
      }

      if (target.pokemonSpecies.secondaryTypeId === 17) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 18 ||
        target.pokemonSpecies.secondaryTypeId === 18
      ) {
        weaknessMultiplier *= 0;
      }
    }

    if (move.typeId === 16) {
      if (
        target.pokemonSpecies.primaryTypeId === 9 ||
        target.pokemonSpecies.primaryTypeId === 14
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 9 ||
        target.pokemonSpecies.secondaryTypeId === 14
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 8 ||
        target.pokemonSpecies.primaryTypeId === 16 ||
        target.pokemonSpecies.primaryTypeId === 18
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 8 ||
        target.pokemonSpecies.secondaryTypeId === 16 ||
        target.pokemonSpecies.secondaryTypeId === 18
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 17) {
      if (
        target.pokemonSpecies.primaryTypeId === 18 ||
        target.pokemonSpecies.primaryTypeId === 13 ||
        target.pokemonSpecies.primaryTypeId === 10
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 13 ||
        target.pokemonSpecies.secondaryTypeId === 18 ||
        target.pokemonSpecies.secondaryTypeId === 10
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 3 ||
        target.pokemonSpecies.primaryTypeId === 5 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 3 ||
        target.pokemonSpecies.secondaryTypeId === 5 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }
    }

    if (move.typeId === 18) {
      if (
        target.pokemonSpecies.primaryTypeId === 8 ||
        target.pokemonSpecies.primaryTypeId === 15 ||
        target.pokemonSpecies.primaryTypeId === 16
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 8 ||
        target.pokemonSpecies.secondaryTypeId === 15 ||
        target.pokemonSpecies.secondaryTypeId === 16
      ) {
        weaknessMultiplier *= 2;
      }

      if (
        target.pokemonSpecies.primaryTypeId === 2 ||
        target.pokemonSpecies.primaryTypeId === 12 ||
        target.pokemonSpecies.primaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }

      if (
        target.pokemonSpecies.secondaryTypeId === 2 ||
        target.pokemonSpecies.secondaryTypeId === 12 ||
        target.pokemonSpecies.secondaryTypeId === 17
      ) {
        weaknessMultiplier /= 2;
      }
    }

    return weaknessMultiplier;
  };

  useEffect(() => {
    fetchPokemon();
    fetchPokemonOptions();
  }, []);

  return (
    <>
      {pokemon && options && (
        <div>
          <Header textAlign="center" size="huge">
            Damge Calculator
          </Header>
          <div className="calculator-container">
            <span>
              <Dropdown
                className="pokemon-one"
                selection
                options={options.pokemon}
                onChange={(_, { value }) => {
                  setPokemon1(value);
                }}
              />
              {currentPokemon1 && pokemon1Stats && currentPokemon2 && (
                <span>
                  <ButtonGroup vertical>
                    <Button
                      onClick={() =>
                        damageCalculation(
                          currentPokemon1.moveOne,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats
                        )
                      }
                    >
                      {currentPokemon1.moveOne.name}
                      <span>
                        (
                        {percentCalculation(
                          currentPokemon1.moveOne,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          15
                        )}
                        % -{" "}
                        {percentCalculation(
                          currentPokemon1.moveOne,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          0
                        )}
                        %)
                      </span>
                    </Button>
                    <Button
                      onClick={() =>
                        damageCalculation(
                          currentPokemon1.moveTwo,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats
                        )
                      }
                    >
                      {currentPokemon1.moveTwo.name}
                      <span>
                        (
                        {percentCalculation(
                          currentPokemon1.moveTwo,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          15
                        )}
                        % -{" "}
                        {percentCalculation(
                          currentPokemon1.moveTwo,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          0
                        )}
                        %)
                      </span>
                    </Button>
                    <Button
                      onClick={() =>
                        damageCalculation(
                          currentPokemon1.moveThree,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats
                        )
                      }
                    >
                      {currentPokemon1.moveThree.name}
                      <span>
                        (
                        {percentCalculation(
                          currentPokemon1.moveThree,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          15
                        )}
                        % -{" "}
                        {percentCalculation(
                          currentPokemon1.moveThree,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          0
                        )}
                        %)
                      </span>
                    </Button>
                    <Button
                      onClick={() =>
                        damageCalculation(
                          currentPokemon1.moveFour,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats
                        )
                      }
                    >
                      {currentPokemon1.moveFour.name}
                      <span>
                        (
                        {percentCalculation(
                          currentPokemon1.moveFour,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          15
                        )}
                        % -{" "}
                        {percentCalculation(
                          currentPokemon1.moveFour,
                          currentPokemon1,
                          currentPokemon2,
                          pokemon1Stats,
                          0
                        )}
                        %)
                      </span>
                    </Button>
                  </ButtonGroup>
                  <span></span>
                </span>
              )}
            </span>

            <span>
              {currentPokemon2 &&
                pokemon2Stats &&
                currentPokemon1 &&
                pokemon1Stats && (
                  <>
                    <ButtonGroup vertical>
                      <Button
                        onClick={() =>
                          damageCalculation(
                            currentPokemon2.moveOne,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon2Stats
                          )
                        }
                      >
                        <span>
                          (
                          {percentCalculation(
                            currentPokemon2.moveOne,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            15
                          )}
                          % -{" "}
                          {percentCalculation(
                            currentPokemon2.moveOne,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            0
                          )}
                          %)
                        </span>
                        {currentPokemon2.moveOne.name}
                      </Button>
                      <Button
                        onClick={() =>
                          damageCalculation(
                            currentPokemon2.moveTwo,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon2Stats
                          )
                        }
                      >
                        <span>
                          (
                          {percentCalculation(
                            currentPokemon2.moveTwo,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            15
                          )}
                          % -{" "}
                          {percentCalculation(
                            currentPokemon2.moveTwo,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            0
                          )}
                          %)
                        </span>
                        {currentPokemon2.moveTwo.name}
                      </Button>
                      <Button
                        onClick={() =>
                          damageCalculation(
                            currentPokemon2.moveThree,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon2Stats
                          )
                        }
                      >
                        <span>
                          (
                          {percentCalculation(
                            currentPokemon2.moveThree,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            15
                          )}
                          % -{" "}
                          {percentCalculation(
                            currentPokemon2.moveThree,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            0
                          )}
                          %)
                        </span>
                        {currentPokemon2.moveThree.name}
                      </Button>
                      <Button
                        onClick={() =>
                          damageCalculation(
                            currentPokemon2.moveFour,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon2Stats
                          )
                        }
                      >
                        <span>
                          (
                          {percentCalculation(
                            currentPokemon2.moveFour,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            15
                          )}
                          % -{" "}
                          {percentCalculation(
                            currentPokemon2.moveFour,
                            currentPokemon2,
                            currentPokemon1,
                            pokemon1Stats,
                            0
                          )}
                          %)
                        </span>
                        {currentPokemon2.moveFour.name}
                      </Button>
                    </ButtonGroup>
                  </>

                  // <Button>{currentPokemon2.pokemon.name}</Button>
                )}
              <Dropdown
                className="pokemon-one"
                selection
                options={options.pokemon}
                onChange={(_, { value }) => {
                  setPokemon2(value);
                }}
              />
            </span>
          </div>
        </div>
      )}
      {damage && move && (
        <div>
          <Header size="medium" textAlign="center">
            {move.name}
          </Header>
          <Container textAlign="center">
            Possible damage amounts: ({damage[0].toFixed(0)},{" "}
            {damage[1].toFixed(0)}, {damage[2].toFixed(0)},
            {damage[3].toFixed(0)}, {damage[4].toFixed(0)},{" "}
            {damage[5].toFixed(0)}, {damage[6].toFixed(0)},{" "}
            {damage[7].toFixed(0)},{damage[8].toFixed(0)},{" "}
            {damage[9].toFixed(0)}, {damage[10].toFixed(0)},{" "}
            {damage[11].toFixed(0)}, {damage[12].toFixed(0)},
            {damage[13].toFixed(0)}, {damage[14].toFixed(0)},{" "}
            {damage[15].toFixed(0)})
          </Container>
        </div>
      )}
    </>
  );
};
