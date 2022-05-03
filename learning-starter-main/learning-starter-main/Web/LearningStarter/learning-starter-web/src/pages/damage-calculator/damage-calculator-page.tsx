import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, ButtonGroup, Dropdown, Header } from "semantic-ui-react";
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
            ),
            calcStat(
              child.pokemonSpecies.baseDefense,
              child.pokemon.defenseIv,
              child.pokemon.defenseEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseSpecialAttack,
              child.pokemon.specialAttackIv,
              child.pokemon.specialAttackEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseSpecialDefense,
              child.pokemon.specialDefenseIv,
              child.pokemon.specialDefenseEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseSpeed,
              child.pokemon.speedIv,
              child.pokemon.speedEv,
              child.pokemon.level
            ),
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
            ),
            calcStat(
              child.pokemonSpecies.baseDefense,
              child.pokemon.defenseIv,
              child.pokemon.defenseEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseSpecialAttack,
              child.pokemon.specialAttackIv,
              child.pokemon.specialAttackEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseSpecialDefense,
              child.pokemon.specialDefenseIv,
              child.pokemon.specialDefenseEv,
              child.pokemon.level
            ),
            calcStat(
              child.pokemonSpecies.baseSpeed,
              child.pokemon.speedIv,
              child.pokemon.speedEv,
              child.pokemon.level
            ),
          ]);
          return;
        }
      });
    }
  };

  const calcHp = (statBase, statIv, statEv, level) => {
    return ((2 * statBase + statIv + statEv / 4) * level) / 100 + level + 10;
  };

  const calcStat = (statBase, statIv, statEv, level) => {
    return ((2 * statBase + statIv + statEv / 4) * level) / 100 + 5;
  };

  //returns 1.1 or .9
  const getNatureMultiplier = (natureId) => {
    var natureMultiplier = [1, 1, 1, 1, 1];
    if (natureId) {
    }
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
              {currentPokemon2 && (
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
