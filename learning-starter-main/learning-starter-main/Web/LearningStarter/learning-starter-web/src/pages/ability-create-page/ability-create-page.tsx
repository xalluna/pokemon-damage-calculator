import React from "react";
import { Field, Form, Formik } from "formik";
import { Input, Button } from "semantic-ui-react";
import {
  AbilityCreateDto,
  AbilityGetDto,
  ApiResponse,
} from "../../constants/types";
import axios from "axios";
import { BaseUrl } from "../../constants/env-vars";
import { useHistory } from "react-router-dom";
import { routes } from "../../routes/config";

const initialValues: AbilityCreateDto = {
  name: "",
};

export const AbilityCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: AbilityCreateDto) => {
    const response = await axios.post<ApiResponse<AbilityGetDto>>(
      `${BaseUrl}/api/abilities`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.home);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={() => {}}>
        <Form>
          <div>
            <label>Name</label>
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
            <div>
              <Button type="submit">Create</Button>
            </div>
          </div>
        </Form>
      </Formik>
    </>
  );
};
