import axios from "axios";
import { Header, Container, Divider } from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import { BaseUrl } from "../../../constants/env-vars";
import {
  PokemonListDto,
  AbilityGetDto,
  ItemGetDto,
  ApiResponse,
  PokemonSpeciesGetDto,
} from "../../../constants/types";

export const PokemonListingPage = () => {
  const [pokemon, setPokemon] = useState<PokemonListDto[]>();
  const fetchPokemon = async () => {
    const response = await axios.get<ApiResponse<PokemonListDto[]>>(
      `${BaseUrl}/api/pokemon/list`
    );
    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      setPokemon(response.data.data);
    }
  };

  // const fetchPokemonSpecies = (id: number) => {
  //   const name = axios.get<ApiResponse<PokemonSpeciesGetDto>>(
  //     `${BaseUrl}/api/pokemon-species/${id}`
  //   );

  //   return name;
  // };

  useEffect(() => {
    fetchPokemon();
  }, []);

  return (
    <div className="pokemon-page-container">
      <div>
        <Header textAlign="center">Pokemon</Header>
        <Divider></Divider>
        {pokemon ? (
          pokemon.map((pokemon) => {
            return (
              <div>
                <Container textAlign="left">
                  <Header>{pokemon.name}</Header>
                  <p>Pokemon: {pokemon.pokemonSpecies}</p>
                  <p>Ability: {pokemon.ability}</p>
                  <p>Item: {pokemon.item}</p>
                  <Header size="small" textAlign="left">
                    Moves
                  </Header>
                  <p>
                    [{pokemon.moveOne}] [{pokemon.moveTwo}]
                  </p>
                  <p>
                    [{pokemon.moveThree}] [{pokemon.moveFour}]
                  </p>
                  <Header>Stats</Header>
                  <p>
                    HP:{"\t"}[{pokemon.healthIv}] [{pokemon.healthEv}]
                  </p>
                  <p>
                    ATK: [{pokemon.attackIv}] [{pokemon.attackEv}]
                  </p>
                  <p>
                    DEF: [{pokemon.defenseIv}] [{pokemon.defenseEv}]
                  </p>
                  <p>
                    SPA: [{pokemon.specialAttackIv}] [{pokemon.specialAttackEv}]
                  </p>
                  <p>
                    SPD: [{pokemon.specialDefenseIv}] [
                    {pokemon.specialDefenseEv}]
                  </p>
                  <p>
                    SPE: [{pokemon.speedIv}] [{pokemon.speedEv}]
                  </p>

                  <p>Gender: {pokemon.gender}</p>
                  <p>Shiny: {pokemon.isShiny ? "true" : "false"}</p>
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
