import React from "react";
import { Field, Form, Formik } from "formik";
import {
  Input,
  Button,
  Modal,
  Header,
  Card,
  Dropdown,
} from "semantic-ui-react";
import {
  PokemonGetDto,
  PokemonFormDto,
  PokemonCreateDto,
  ApiResponse,
  PokemonOptionsDto,
} from "../../../constants/types";
import axios from "axios";
import { baseUrl } from "../../../constants/env-vars";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useState, useEffect } from "react";

const initialValues: PokemonFormDto = {
  name: "",
  pokemonSpecies: "",
  healthEv: "",
  attackEv: "",
  defenseEv: "",
  specialAttackEv: "",
  specialDefenseEv: "",
  speedEv: "",
  healthIv: "",
  attackIv: "",
  defenseIv: "",
  specialAttackIv: "",
  specialDefenseIv: "",
  speedIv: "",
  ability: "",
  item: "",
  moveOne: "",
  moveTwo: "",
  moveThree: "",
  moveFour: "",
  level: "",
  experience: "0",
  nature: "",
  gender: "",
  isShiny: "",
};

// const moveList: DropdownProps[] = () => {
//   return ["Undefined"];
// }, [];

export const PokemonCreatePage = () => {
  const [open, setOpen] = useState(false);
  const [options, setOptions] = useState<PokemonOptionsDto>();
  const history = useHistory();

  useEffect(() => {
    const fetchPokemonOptions = async () => {
      const response = await axios.get<ApiResponse<PokemonOptionsDto>>(
        `/api/pokemon/options`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err);
          return;
        });
      }

      setOptions(response.data.data);
    };

    fetchPokemonOptions();
  }, []);

  const onSubmit = async (formValues: PokemonFormDto) => {
    const values: PokemonCreateDto = {
      name: formValues.name,
      pokemonSpeciesId: parseInt(formValues.pokemonSpecies),
      healthEv: parseInt(formValues.healthEv),
      healthIv: parseInt(formValues.healthIv),
      attackEv: parseInt(formValues.attackEv),
      attackIv: parseInt(formValues.attackIv),
      defenseEv: parseInt(formValues.defenseEv),
      defenseIv: parseInt(formValues.defenseIv),
      specialAttackEv: parseInt(formValues.specialAttackEv),
      specialAttackIv: parseInt(formValues.specialAttackIv),
      specialDefenseEv: parseInt(formValues.specialDefenseEv),
      specialDefenseIv: parseInt(formValues.specialDefenseIv),
      speedEv: parseInt(formValues.speedEv),
      speedIv: parseInt(formValues.speedIv),
      abilityId: parseInt(formValues.ability),
      itemId: parseInt(formValues.item),
      moveOneId: parseInt(formValues.moveOne),
      moveTwoId: parseInt(formValues.moveTwo),
      moveThreeId: parseInt(formValues.moveThree),
      moveFourId: parseInt(formValues.moveFour),
      level: parseInt(formValues.level),
      experience: parseInt(formValues.experience),
      natureId: parseInt(formValues.nature),
      gender: parseInt(formValues.gender),
      isShiny: formValues.isShiny.toLowerCase() === "true",
    };

    const response = await axios.post<ApiResponse<PokemonGetDto>>(
      `${baseUrl}/api/pokemon`,
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
      {options && (
        <div className="pokemon-create-page">
          {/* <div>
        <Modal
          className="modal"
          onOpen={() => setOpen(true)}
          onClose={() => setOpen(false)}
          open={open}
          closeIcon
          dimmer="blurring"
        >
          <div className="modal-content">
            <div>
              <Header size="huge" textAlign="center">
                You are about to create a Pokemon. Would you like submit this
                form?
              </Header>
              <div className="buttons">
                <Button
                  color="grey"
                  size="huge"
                  onClick={(open) => setOpen(false)}
                >
                  No
                </Button>
                <Button color="teal" size="huge" type="submit" form="form">
                  Yes
                </Button>
              </div>
            </div>
          </div>
        </Modal>
      </div> */}
          <Card>
            <Header textAlign="center">Create Pokemon</Header>
            <Formik initialValues={initialValues} onSubmit={onSubmit}>
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
                    {/* <Field id="pokemonSpecies" name="pokemonSpecies">
                  {({ field }) => <Input {...field} />}
                </Field> */}
                    <Dropdown
                      selection
                      clearable
                      placeholder="Pokemon"
                      options={options?.species}
                    />
                  </div>

                  <div>
                    <label>Ability</label>
                    <Field id="ability" name="ability">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>

                  <div>
                    <label>Item</label>
                    <Field id="item" name="item">
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
                    <Field id="moveOne" name="moveOne">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>
                  <div>
                    <label>Move 2</label>
                    <Field id="moveTwo" name="moveTwo">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>

                  <div>
                    <label>Move 3</label>
                    <Field id="moveThree" name="moveThree">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>
                  <div>
                    <label>Move 4</label>
                    <Field id="moveFour" name="moveFour">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>

                  <div>
                    <label>HP EVs</label>
                    <Field id="healthEv" name="healthEv">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
                    <label>Spe IVs</label>
                    <Field id="speedIv" name="speedIv">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>

                  <div>
                    <label>Nature</label>
                    <Field id="nature" name="nature">
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
          </Card>
          <div>
            <Button
              className="submit-button"
              color="teal"
              size="large"
              type="submit"
              form="form"
            >
              Create Pokemon
            </Button>
          </div>
        </div>
      )}
    </>
  );
};
