import axios from "axios";
import React, { useEffect, useState } from "react";
import {
  Button,
  ButtonGroup,
  Divider,
  Dropdown,
  Header,
} from "semantic-ui-react";
import { baseUrl } from "../../constants/env-vars";
import {
  ApiResponse,
  PokemonBattleDto,
  PokemonBattleGroup,
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
              child.pokemon.attackIv,
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
              child.pokemon.attackIv,
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
              {currentPokemon1 && (
                <ButtonGroup
                  buttons={[
                    `${currentPokemon1.moveOne.name}`,
                    `${currentPokemon1.moveTwo.name}`,
                    `${currentPokemon1.moveThree.name}`,
                    `${currentPokemon1.moveFour.name}`,
                  ]}
                  vertical
                />
                // <Button>{currentPokemon1.pokemon.name}</Button>
              )}
            </span>

            <span>
              {currentPokemon2 && pokemon2Stats && (
                <>
                  <ButtonGroup
                    buttons={[
                      `${currentPokemon2.moveOne.name}`,
                      `${currentPokemon2.moveTwo.name}`,
                      `${currentPokemon2.moveThree.name}`,
                      `${currentPokemon2.moveFour.name}`,
                    ]}
                    vertical
                  />
                  <ButtonGroup
                    buttons={[
                      `${pokemon2Stats[0]}`,
                      `${pokemon2Stats[1]}`,
                      `${pokemon2Stats[2]}`,
                      `${pokemon2Stats[3]}`,
                      `${pokemon2Stats[4]}`,
                      `${pokemon2Stats[5]}`,
                    ]}
                    vertical
                  />
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
    </>
  );
};
