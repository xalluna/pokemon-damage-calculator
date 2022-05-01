import { Formik, Form, Field } from "formik";
import { Button, Input } from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  ApiResponse,
  PokemonGetDto,
  PokemonUpdateDto,
} from "../../../constants/types";
import { baseUrl } from "../../../constants/env-vars";
import { useRouteMatch, useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";

export const PokemonUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [pokemon, setPokemon] = useState<PokemonGetDto>();

  useEffect(() => {
    const fetchPokemon = async () => {
      const response = await axios.get<ApiResponse<PokemonGetDto>>(
        `/api/pokemon/${id}`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err);
          return;
        });
      }

      setPokemon(response.data.data);
    };

    fetchPokemon();
  }, [id]);

  const onSubmit = async (formValues: PokemonGetDto) => {
    const values: PokemonUpdateDto = {
      name: formValues.name,
      pokemonSpeciesId: formValues.pokemonSpeciesId,
      healthEv: formValues.healthEv,
      healthIv: formValues.healthIv,
      attackEv: formValues.attackEv,
      attackIv: formValues.attackIv,
      defenseEv: formValues.defenseEv,
      defenseIv: formValues.defenseIv,
      specialAttackEv: formValues.specialAttackEv,
      specialAttackIv: formValues.specialAttackIv,
      specialDefenseEv: formValues.specialDefenseEv,
      specialDefenseIv: formValues.specialDefenseIv,
      speedEv: formValues.speedEv,
      speedIv: formValues.speedIv,
      abilityId: formValues.abilityId,
      itemId: formValues.itemId,
      moveOneId: formValues.moveOneId,
      moveTwoId: formValues.moveTwoId,
      moveThreeId: formValues.moveThreeId,
      moveFourId: formValues.moveFourId,
      level: formValues.level,
      experience: formValues.experience,
      natureId: formValues.natureId,
      gender: formValues.gender,
      isShiny: formValues.isShiny,
    };

    const response = await axios.put<ApiResponse<PokemonGetDto>>(
      `${baseUrl}/api/pokemon/${id}`,
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
      {pokemon && (
        <Formik initialValues={pokemon} onSubmit={onSubmit}>
          <Form id="form">
            <div>
              <div>
                <label>Name</label>
                <Field id="name" name="name">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Species</label>
                <Field id="pokemonSpeciesId" name="pokemonSpeciesId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Ability</label>
                <Field id="abilityId" name="abilityId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Item</label>
                <Field id="itemId" name="itemId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Level</label>
                <Field id="level" name="level">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Move 1</label>
                <Field id="moveOneId" name="moveOneId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>Move 2</label>
                <Field id="moveTwoId" name="moveTwoId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Move 3</label>
                <Field id="moveThreeId" name="moveThreeId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>Move 4</label>
                <Field id="moveFourId" name="moveFourId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>HP EVs</label>
                <Field id="healthEv" name="healthEv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>HP IVs</label>
                <Field id="healthIv" name="healthIv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Atk EVs</label>
                <Field id="attackEv" name="attackEv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>Atk IVs</label>
                <Field id="attackIv" name="attackIv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Def EVs</label>
                <Field id="defenseEv" name="defenseEv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>Def IVs</label>
                <Field id="defenseIv" name="defenseIv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>SpA EVs</label>
                <Field id="specialAttackEv" name="specialAttackEv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>SpA IVs</label>
                <Field id="specialAttackIv" name="specialAttackIv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>SpD EVs</label>
                <Field id="specialDefenseEv" name="specialDefenseEv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>SpD IVs</label>
                <Field id="specialDefenseIv" name="specialDefenseIv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Spe EVs</label>
                <Field id="speedEv" name="speedEv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
                <label>Spe IVs</label>
                <Field id="speedIv" name="speedIv">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Nature</label>
                <Field id="natureId" name="natureId">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Gender</label>
                <Field id="gender" name="gender">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>

              <div>
                <label>Shiny</label>
                <Field id="isShiny" name="isShiny">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>
              <div>
                <Button type="submit">Submit</Button>
              </div>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
