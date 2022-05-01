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
  itemId: 0,
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

  const onSubmit = async (formValues: PokemonCreateDto) => {
    const response = await axios.post<ApiResponse<PokemonGetDto>>(
      `${baseUrl}/api/pokemon`,
      formValues
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
                    <Field id="name" name="name" placeholder="Name">
                      {({ field }) => <Input {...field} />}
                    </Field>
                  </div>

                  <div>
                    <label>Pokemon</label>
                    <Field id="pokemonSpeciesId" name="pokemonSpeciesId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Pokemon"
                          options={options.species}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>

                  <div>
                    <label>Ability</label>
                    <Field id="abilityId" name="abilityId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Ability"
                          options={options.abilities}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>

                  <div>
                    <label>Item</label>
                    <Field id="itemId" name="itemId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Item"
                          options={options.items}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
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
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Move"
                          options={options.moves}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>
                  <div>
                    <label>Move 2</label>
                    <Field id="moveTwoId" name="moveTwoId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Move"
                          options={options.moves}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>

                  <div>
                    <label>Move 3</label>
                    <Field id="moveThreeId" name="moveThreeId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Move"
                          options={options.moves}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>
                  <div>
                    <label>Move 4</label>
                    <Field id="moveFourId" name="moveFourId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Move"
                          options={options.moves}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>

                  <div>
                    <label>HP EVs</label>
                    <Field id="healthEv" name="healthEv">
                      {({ field }) => <Input type="number" {...field} />}
                    </Field>
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
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
                  </div>
                  <div>
                    <label>Spe IVs</label>
                    <Field id="speedIv" name="speedIv">
                      {({ field }) => <Input type="number" {...field} />}
                    </Field>
                  </div>

                  <div>
                    <label>Nature</label>
                    <Field id="natureId" name="natureId">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Nature"
                          options={options.natures}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>

                  <div>
                    <label>Gender</label>
                    <Field id="gender" name="gender">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          placeholder="Gender"
                          options={[
                            {
                              key: "Male",
                              text: "Male",
                              value: 0,
                            },
                            {
                              key: "Female",
                              text: "Female",
                              value: 1,
                            },
                            {
                              key: "Undefined",
                              text: "Undefined",
                              value: 2,
                            },
                          ]}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
                    </Field>
                  </div>

                  <div>
                    <label>Shiny</label>
                    <Field id="isShiny" name="isShiny">
                      {({ field, form }) => (
                        <Dropdown
                          {...field}
                          selection
                          clearable
                          options={[
                            {
                              key: "Yes",
                              text: "Yes",
                              value: true,
                            },
                            {
                              key: "No",
                              text: "No",
                              value: false,
                            },
                          ]}
                          onChange={(_, { value }) => {
                            form.setFieldValue(field.name, value);
                          }}
                        />
                      )}
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
