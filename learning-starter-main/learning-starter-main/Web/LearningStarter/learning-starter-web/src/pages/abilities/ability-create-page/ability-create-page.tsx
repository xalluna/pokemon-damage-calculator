import React, { useState } from "react";
import { Field, Form, Formik } from "formik";
import { Input, Button, Modal, Header } from "semantic-ui-react";
import {
  AbilityCreateDto,
  AbilityGetDto,
  ApiResponse,
} from "../../../constants/types";
import axios from "axios";
import { BaseUrl } from "../../../constants/env-vars";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";

const initialValues: AbilityCreateDto = {
  name: "",
};

export const AbilityCreatePage = () => {
  const [open, setOpen] = useState(false);
  const history = useHistory();

  const onSubmit = async (values: AbilityCreateDto) => {
    setOpen(false);
    const response = await axios.post<ApiResponse<AbilityGetDto>>(
      `${BaseUrl}/api/abilities`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      history.push(routes.abilities.listing);
    }
  };

  return (
    <>
      <div>
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
                You are about to create an Ability. Would you like submit this
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
      </div>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form id="form">
          <div>
            <label>Name</label>
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
        </Form>
      </Formik>
      <div>
        <Button onClick={(open) => setOpen(true)} color="teal">
          Create
        </Button>
      </div>
    </>
  );
};
