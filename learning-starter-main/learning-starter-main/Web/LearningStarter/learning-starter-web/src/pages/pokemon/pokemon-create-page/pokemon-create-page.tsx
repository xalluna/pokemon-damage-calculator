import React from "react";
import { Field, Form, Formik } from "formik";
import { Input, Button, Dropdown, DropdownProps } from "semantic-ui-react";
import {
  PokemonCreateDto,
  PokemonGetDto,
  PokemonListDto,
  MoveGetDto,
  PokemonSpeciesGetDto,
  NatureGetDto,
  ItemGetDto,
  AbilityGetDto,
  ApiResponse,
} from "../../../constants/types";
import axios from "axios";
import { BaseUrl } from "../../../constants/env-vars";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";

const initialValues: PokemonCreateDto = {
  name: "",
  pokemonSpeciesId: 0,
  healthEv: 0,
  attackEv: 0,
  defenseEv: 0,
  specialAttackEv: 0,
  specialDefenseEv: 0,
  speedEv: 0,
  healthIv: 0,
  attackIv: 0,
  defenseIv: 0,
  specialAttackIv: 0,
  specialDefenseIv: 0,
  speedIv: 0,
  abilityId: 0,
  ItemId: 0,
  moveOneId: 0,
  moveTwoId: 0,
  moveThreeId: 0,
  moveFourId: 0,
  level: 0,
  experience: 0,
  natureId: 0,
  gender: 0,
  isShiny: false,
};

const moveList: DropdownProps[] = () => {
  return ["Undefined"];
};

export const PokemonCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: PokemonCreateDto) => {
    const response = await axios.post<ApiResponse<PokemonGetDto>>(
      `${BaseUrl}/api/pokemon`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.pokemon.listing);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
          <div>
            <div>
              <label>Name</label>
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>

            <div>
              <label>Species</label>
              <Dropdown selection options={moveList} />
              {/* <Field id="species" name="species">
                {({ field }) => <Input {...field} />}
              </Field> */}
            </div>
            <div>
              <Button type="submit">Create</Button>
            </div>
          </div>
        </Form>
      </Formik>
    </>
  );
};
