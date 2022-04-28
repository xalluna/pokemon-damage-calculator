import axios from "axios";
import { Header, Container, Divider } from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import { BaseUrl } from "../../../constants/env-vars";
import {
  PokemonGetDto,
  AbilityGetDto,
  ItemGetDto,
  ApiResponse,
  PokemonSpeciesGetDto,
} from "../../../constants/types";

export const PokemonListingPage = () => {
  const [pokemon, setPokemon] = useState<PokemonGetDto[]>();
  const fetchPokemon = async () => {
    const response = await axios.get<ApiResponse<PokemonGetDto[]>>(
      `${BaseUrl}/api/pokemon`
    );
    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      setPokemon(response.data.data);
    }
  };

  const fetchPokemonSpecies = (id: number) => {
    const name = axios.get<ApiResponse<PokemonSpeciesGetDto>>(
      `${BaseUrl}/api/pokemon-species/${id}`
    );

    return name;
  };

  useEffect(() => {
    fetchPokemon();
  }, []);

  return (
    <div className="pokemon-page-container">
      <div>
        <Header>Pokemon</Header>
        {pokemon ? (
          pokemon.map((pokemon) => {
            return (
              <div>
                <Container textAlign="left">
                  <p>{pokemon.name}</p>
                  <p>{pokemon.pokemonSpeciesId}</p>
                </Container>
                <Divider />
              </div>
            );
          })
        ) : (
          <div>No Abilities</div>
        )}
      </div>
    </div>
  );
};
