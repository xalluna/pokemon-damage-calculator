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
} from "semantic-ui-react";
import React, { useEffect, useState } from "react";
import { baseUrl } from "../../../constants/env-vars";
import {
  PokemonListDto,
  ApiResponse,
  PokemonOptionsDto,
  PokemonGetDto,
  PokemonUpdateDto,
} from "../../../constants/types";
import { PokemonCreatePage } from "../pokemon-create-page/pokemon-create-page";
import pikachu from "../../../assets/pikachu.png";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { Field, Formik, Form } from "formik";

export const PokemonListingPage = () => {
  const [pokemon, setPokemon] = useState<PokemonListDto[]>();
  const [options, setOptions] = useState<PokemonOptionsDto>();
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
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      setPokemon(response.data.data);
    }
  };

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

  const initDeletePokemon = (id: number) => {
    setDeleteId(id);
    setOpenDelete(true);
  };

  const deletePokemon = async () => {
    const response = await axios.delete(`${baseUrl}/api/pokemon/${deleteId}`);

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => console.log(err.message));
    }

    history.push(routes.home);
    history.push(routes.pokemon.listing);
  };

  const initEditPokemon = (pokemon: PokemonGetDto) => {
    setPokemonEdit(pokemon);
    setOpenEdit(true);
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
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
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
                          onClick={() => initEditPokemon(pokemon.pokemon)}
                        ></Button>

                        <Button
                          label="Delete"
                          labelPosition="left"
                          icon="trash alternate outline"
                          color="red"
                          onClick={() => initDeletePokemon(pokemon.id)}
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
