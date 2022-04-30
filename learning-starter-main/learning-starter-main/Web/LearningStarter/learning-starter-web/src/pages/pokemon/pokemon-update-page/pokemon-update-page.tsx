import { Formik, Form, Field } from "formik";
import { Input } from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import axios from "axios";
import { ApiResponse, PokemonGetDto } from "../../../constants/types";
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
      } else {
      }

      setPokemon(response.data.data);
    };

    fetchPokemon();
  }, [id]);

  return (
    <>
      {pokemon && (
        <Formik initialValues={pokemon} onSubmit={() => {}}>
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
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Item</label>
                <Field id="itemId" name="itemId">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Level</label>
                <Field id="level" name="level">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Move 1</label>
                <Field id="moveOneId" name="moveOneId">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>Move 2</label>
                <Field id="moveTwoId" name="moveTwoId">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Move 3</label>
                <Field id="moveThreeId" name="moveThreeId">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>Move 4</label>
                <Field id="moveFourId" name="moveFourId">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>HP EVs</label>
                <Field id="healthEv" name="healthEv">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>HP IVs</label>
                <Field id="healthIv" name="healthIv">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Atk EVs</label>
                <Field id="attackEv" name="attackEv">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>Atk IVs</label>
                <Field id="attackIv" name="attackIv">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Def EVs</label>
                <Field id="defenseEv" name="defenseEv">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>Def IVs</label>
                <Field id="defenseIv" name="defenseIv">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>SpA EVs</label>
                <Field id="specialAttackEv" name="specialAttackEv">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>SpA IVs</label>
                <Field id="specialAttackIv" name="specialAttackIv">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>SpD EVs</label>
                <Field id="specialDefenseEv" name="specialDefenseEv">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>SpD IVs</label>
                <Field id="specialDefenseIv" name="specialDefenseIv">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Spe EVs</label>
                <Field id="speedEv" name="speedEv">
                  {({ field }) => <Input {...field} />}
                </Field>
                <label>Spe IVs</label>
                <Field id="speedIv" name="speedIv">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Nature</label>
                <Field id="natureId" name="natureId">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Gender</label>
                <Field id="gender" name="gender">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>

              <div>
                <label>Shiny</label>
                <Field id="isShiny" name="isShiny">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
