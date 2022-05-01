import axios from "axios";
import {
  Header,
  Container,
  Divider,
  Button,
  Modal,
  Card,
  Image,
  Icon,
} from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import { baseUrl } from "../../../constants/env-vars";
import { PokemonListDto, ApiResponse } from "../../../constants/types";
import { PokemonCreatePage } from "../pokemon-create-page/pokemon-create-page";
import pikachu from "../../../assets/pikachu.png";

export const PokemonListingPage = () => {
  const [pokemon, setPokemon] = useState<PokemonListDto[]>();
  const [open, setOpen] = useState(false);
  const fetchPokemon = async () => {
    const response = await axios.get<ApiResponse<PokemonListDto[]>>(
      `${baseUrl}/api/pokemon/list`
    );
    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      setPokemon(response.data.data);
    }
  };

  useEffect(() => {
    fetchPokemon();
  }, []);

  return (
    <div className="pokemon-page-container">
      <div>
        <div className="pokemon-listing-page-create-button">
          <Button size="huge" color="teal" onClick={(open) => setOpen(true)}>
            Create
          </Button>
        </div>
        <Divider></Divider>

        <Header textAlign="center" size="huge">
          Pokemon
        </Header>
        <Divider></Divider>
        <Card.Group itemsPerRow={6} centered>
          {pokemon ? (
            pokemon.map((pokemon) => {
              return (
                <div className="item">
                  <Card>
                    <Image src={pikachu} />
                    <Card.Content>
                      <span>
                        <Header>{pokemon.name}</Header>
                      </span>

                      <span>
                        <div>Pokemon:</div>
                        <div>{pokemon.pokemonSpecies}</div>
                      </span>
                      <span>
                        <div>Ability:</div>
                        <div>{pokemon.ability}</div>
                      </span>
                      <span>
                        <div>Item:</div>
                        <div>{pokemon.item}</div>
                      </span>
                      <span>
                        <div>Gender:</div>
                        <div>{pokemon.gender}</div>
                      </span>
                      <span>
                        <div>Shiny:</div>
                        <div>{pokemon.isShiny ? "Yes" : "No"}</div>
                      </span>
                      <Divider />
                      <Header>Moves</Header>
                      <span>
                        <div>[{pokemon.moveOne}]</div>
                        <div>[{pokemon.moveTwo}]</div>
                      </span>
                      <span>
                        <div>[{pokemon.moveThree}]</div>
                        <div>[{pokemon.moveFour}]</div>
                      </span>
                      <Divider />
                      <Header>Stats</Header>
                      <div>
                        Hp:[{pokemon.healthIv}] [{pokemon.healthEv}]
                      </div>
                      <div>
                        Atk: [{pokemon.attackIv}] [{pokemon.attackEv}]
                      </div>
                      <div>
                        Def: [{pokemon.defenseIv}] [{pokemon.defenseEv}]
                      </div>
                      <div>
                        SpA: [{pokemon.specialAttackIv}] [
                        {pokemon.specialAttackEv}]
                      </div>
                      <div>
                        SpD: [{pokemon.specialDefenseIv}] [
                        {pokemon.specialDefenseEv}]
                      </div>
                      <div>
                        Spe: [{pokemon.speedIv}] [{pokemon.speedEv}]
                      </div>
                    </Card.Content>
                    <div>
                      <Button.Group compact floated="right">
                        <Button
                          label="Edit"
                          labelPosition="left"
                          icon="edit outline"
                          color="teal"
                        ></Button>
                        <Button
                          label="Delete"
                          labelPosition="left"
                          icon="trash alternate outline"
                          color="red"
                        ></Button>
                      </Button.Group>
                    </div>
                  </Card>
                  <Divider />
                </div>
              );
            })
          ) : (
            <div>No Pokemon</div>
          )}
        </Card.Group>
      </div>
      <Modal
        onOpen={() => setOpen(true)}
        onClose={() => setOpen(false)}
        open={open}
        closeIcon
        dimmer="blurring"
      >
        <PokemonCreatePage />
      </Modal>
    </div>
  );
};
