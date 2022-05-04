import axios from "axios";
import {
  Header,
  Divider,
  Button,
  Modal,
  Card,
  Image,
  Dropdown,
  Input,
  Message,
} from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import { baseUrl } from "../../../constants/env-vars";
import {
  PokemonListDto,
  ApiResponse,
  PokemonOptionsDto,
  PokemonGetDto,
  PokemonUpdateDto,
  Error,
} from "../../../constants/types";
import { PokemonCreatePage } from "../pokemon-create-page/pokemon-create-page";
import pikachu from "../../../assets/pikachu.png";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { Field, Formik, Form } from "formik";
import "./pokemon-listing-page.css";

export const PokemonListingPage = () => {
  const [pokemon, setPokemon] = useState<PokemonListDto[]>();
  const [options, setOptions] = useState<PokemonOptionsDto>();
  const [errors, setErrors] = useState<Error[]>();
  const [open, setOpen] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);
  const [openEdit, setOpenEdit] = useState(false);
  const [deleteId, setDeleteId] = useState<number>();
  const [pokemonEdit, setPokemonEdit] = useState<PokemonGetDto>();
  const history = useHistory();

  const fetchPokemon = async () => {
    const response = await axios.get<ApiResponse<PokemonListDto[]>>(
      `${baseUrl}/api/pokemon/list`
    );
    if (response.data.hasErrors) {
      setErrors(response.data.errors);
    } else {
      setPokemon(response.data.data);
    }
  };

  const fetchPokemonOptions = async () => {
    const response = await axios.get<ApiResponse<PokemonOptionsDto>>(
      `/api/pokemon/options`
    );

    if (response.data.hasErrors) {
      setErrors(response.data.errors);
    }

    setOptions(response.data.data);
  };

  const initDeletePokemon = (id: number) => {
    setDeleteId(id);
    setOpenDelete(true);
  };

  const deletePokemon = async () => {
    const response = await axios.delete(`${baseUrl}/api/pokemon/${deleteId}`);

    if (response.data.hasErrors) {
      setErrors(response.data.errors);
    }

    history.push(routes.home);
    history.push(routes.pokemon.listing);
  };

  const initEditPokemon = (pokemon: PokemonGetDto) => {
    setPokemonEdit(pokemon);
    setOpenEdit(true);
  };

  const calcHp = (statBase, statIv, statEv, level) => {
    return (
      ((2 * statBase + statIv + statEv / 4.0) * level) / 100.0 + level + 10
    );
  };

  const calcStat = (statBase, statIv, statEv, level) => {
    return ((2 * statBase + statIv + statEv / 4.0) * level) / 100.0 + 5;
  };

  //returns 1.1 or .9
  const getNatureMultiplier = (natureId) => {
    var natureMultiplier = [1, 1, 1, 1, 1]; //[atk, def, spA, spD, spe]

    if (
      natureId === 2 ||
      natureId === 7 ||
      natureId === 9 ||
      natureId === 20 ||
      natureId === 24
    ) {
      return natureMultiplier;
    }

    if (
      natureId === 1 ||
      natureId === 4 ||
      natureId === 14 ||
      natureId === 18
    ) {
      natureMultiplier[0] = 1.1;
    }

    if (
      natureId === 3 ||
      natureId === 11 ||
      natureId === 13 ||
      natureId === 22
    ) {
      natureMultiplier[1] = 1.1;
    }

    if (
      natureId === 15 ||
      natureId === 16 ||
      natureId === 19 ||
      natureId === 21
    ) {
      natureMultiplier[2] = 1.1;
    }

    if (natureId === 5 || natureId === 6 || natureId === 8 || natureId === 23) {
      natureMultiplier[3] = 1.1;
    }

    if (
      natureId === 10 ||
      natureId === 12 ||
      natureId === 17 ||
      natureId === 25
    ) {
      natureMultiplier[4] = 1.1;
    }

    if (
      natureId === 3 ||
      natureId === 5 ||
      natureId === 16 ||
      natureId === 25
    ) {
      natureMultiplier[0] = 0.9;
    }

    if (
      natureId === 8 ||
      natureId === 10 ||
      natureId === 14 ||
      natureId === 15
    ) {
      natureMultiplier[1] = 0.9;
    }

    if (
      natureId === 1 ||
      natureId === 6 ||
      natureId === 11 ||
      natureId === 12
    ) {
      natureMultiplier[2] = 0.9;
    }

    if (
      natureId === 13 ||
      natureId === 17 ||
      natureId === 18 ||
      natureId === 21
    ) {
      natureMultiplier[3] = 0.9;
    }

    if (
      natureId === 4 ||
      natureId === 19 ||
      natureId === 22 ||
      natureId === 23
    ) {
      natureMultiplier[4] = 0.9;
    }

    return natureMultiplier;
  };

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
      `${baseUrl}/api/pokemon/${formValues.id}`,
      values
    );

    if (response.data.hasErrors) {
      setErrors(response.data.errors);
    } else {
      history.push(routes.home);
      history.push(routes.pokemon.listing);
    }
  };

  useEffect(() => {
    fetchPokemon();
    fetchPokemonOptions();
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
        {errors && (
          <Message
            error
            header="There was some errors with your submission"
            list={errors}
          />
        )}
        <Card.Group className="pokemon-card-group" itemsPerRow={6}>
          {pokemon ? (
            pokemon.map((pokemon) => {
              return (
                <div className="pokemon-card">
                  <Card>
                    <Image src={pikachu} />
                    <Card.Content>
                      <span>
                        <Header>{pokemon.name}</Header>
                      </span>

                      <div>
                        Pokemon:
                        <div>{pokemon.pokemonSpecies}</div>
                      </div>

                      <div>
                        Level:
                        <div>{pokemon.level}</div>
                      </div>

                      <div>
                        Ability:
                        <div>{pokemon.ability}</div>
                      </div>

                      <span>
                        <div>Item:</div>
                        <div>{pokemon.item}</div>
                      </span>
                      <span>
                        <div>Nature:</div>
                        <div>{pokemon.nature}</div>
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
                        Hp:
                        {calcHp(
                          pokemon.pokemon.pokemonSpecies.baseHealth,
                          pokemon.pokemon.pokemon.healthIv,
                          pokemon.pokemon.pokemon.healthEv,
                          pokemon.pokemon.pokemon.level
                        ).toFixed(0)}{" "}
                        [{pokemon.healthIv}] [{pokemon.healthEv}]
                      </div>
                      <div>
                        Atk:{" "}
                        {(
                          calcStat(
                            pokemon.pokemon.pokemonSpecies.baseAttack,
                            pokemon.pokemon.pokemon.attackIv,
                            pokemon.pokemon.pokemon.attackEv,
                            pokemon.pokemon.pokemon.level
                          ) *
                          getNatureMultiplier(
                            pokemon.pokemon.pokemon.natureId
                          )[0]
                        ).toFixed(0)}{" "}
                        [{pokemon.attackIv}] [{pokemon.attackEv}]
                      </div>
                      <div>
                        Def:{" "}
                        {(
                          calcStat(
                            pokemon.pokemon.pokemonSpecies.baseDefense,
                            pokemon.pokemon.pokemon.defenseIv,
                            pokemon.pokemon.pokemon.defenseEv,
                            pokemon.pokemon.pokemon.level
                          ) *
                          getNatureMultiplier(
                            pokemon.pokemon.pokemon.natureId
                          )[1]
                        ).toFixed(0)}{" "}
                        [{pokemon.defenseIv}] [{pokemon.defenseEv}]
                      </div>
                      <div>
                        SpA:{" "}
                        {(
                          calcStat(
                            pokemon.pokemon.pokemonSpecies.baseSpecialAttack,
                            pokemon.pokemon.pokemon.specialAttackIv,
                            pokemon.pokemon.pokemon.specialAttackEv,
                            pokemon.pokemon.pokemon.level
                          ) *
                          getNatureMultiplier(
                            pokemon.pokemon.pokemon.natureId
                          )[2]
                        ).toFixed(0)}{" "}
                        [{pokemon.specialAttackIv}] [{pokemon.specialAttackEv}]
                      </div>
                      <div>
                        SpD:{" "}
                        {(
                          calcStat(
                            pokemon.pokemon.pokemonSpecies.baseSpecialDefense,
                            pokemon.pokemon.pokemon.specialDefenseIv,
                            pokemon.pokemon.pokemon.specialDefenseEv,
                            pokemon.pokemon.pokemon.level
                          ) *
                          getNatureMultiplier(
                            pokemon.pokemon.pokemon.natureId
                          )[3]
                        ).toFixed(0)}{" "}
                        [{pokemon.specialDefenseIv}] [{pokemon.specialDefenseEv}
                        ]
                      </div>
                      <div>
                        Spe:{" "}
                        {(
                          calcStat(
                            pokemon.pokemon.pokemonSpecies.baseSpeed,
                            pokemon.pokemon.pokemon.speedIv,
                            pokemon.pokemon.pokemon.speedEv,
                            pokemon.pokemon.pokemon.level
                          ) *
                          getNatureMultiplier(
                            pokemon.pokemon.pokemon.natureId
                          )[4]
                        ).toFixed(0)}{" "}
                        [{pokemon.speedIv}] [{pokemon.speedEv}]
                      </div>
                    </Card.Content>
                    <div>
                      <Button.Group compact floated="right">
                        <Button
                          label="Edit"
                          labelPosition="left"
                          icon="edit outline"
                          color="teal"
                          onClick={() =>
                            initEditPokemon(pokemon.pokemon.pokemon)
                          }
                        />

                        <Button
                          label="Delete"
                          labelPosition="left"
                          icon="trash alternate outline"
                          color="red"
                          onClick={() => initDeletePokemon(pokemon.id)}
                        />
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
      <Modal
        onOpen={() => setOpenDelete(true)}
        onClose={() => setOpenDelete(false)}
        open={openDelete}
        dimmer="blurring"
      >
        <div className="modal-content">
          <div>
            <Header size="huge" textAlign="center">
              You are about to delete a Pokemon. Would you like submit this
              form?
            </Header>
            <div className="buttons">
              <Button
                color="grey"
                size="huge"
                onClick={(open) => setOpenDelete(false)}
              >
                No
              </Button>
              <Button color="teal" size="huge" onClick={deletePokemon}>
                Yes
              </Button>
            </div>
          </div>
        </div>
      </Modal>
      {options && pokemonEdit && (
        <Modal
          onOpen={() => setOpenEdit(true)}
          onClose={() => setOpenEdit(false)}
          open={openEdit}
          dimmer="blurring"
        >
          <Card>
            <Header textAlign="center">Update Pokemon</Header>
            <Formik initialValues={pokemonEdit} onSubmit={onSubmit}>
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
            <div>
              <Button
                content="Update"
                color="teal"
                size="large"
                type="submit"
                form="form"
              />
              <Button
                content="Cancel"
                color="grey"
                size="large"
                onClick={(openEdit) => setOpenEdit(false)}
              />
            </div>
          </Card>
        </Modal>
      )}
    </div>
  );
};
