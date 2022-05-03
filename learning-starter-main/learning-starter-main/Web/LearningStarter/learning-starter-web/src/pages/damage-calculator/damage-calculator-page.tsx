import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Dropdown, Header } from "semantic-ui-react";
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
  const [currentPokemon2, setCurrentPokemon2] = useState<PokemonBattleDto>();

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
          return;
        }
      });
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
                <Button>{currentPokemon1.pokemon.name}</Button>
              )}
            </span>

            <span>
              {currentPokemon2 && (
                <Button>{currentPokemon2.pokemon.name}</Button>
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
