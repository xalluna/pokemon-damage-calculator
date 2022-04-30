import axios from "axios";
import React, { useEffect, useState } from "react";
import { Container, Divider, Header } from "semantic-ui-react";
import { baseUrl } from "../../../constants/env-vars";
import { AbilityGetDto, ApiResponse } from "../../../constants/types";

export const AbilityListingPage = () => {
  const [abilities, setAbilities] = useState<AbilityGetDto[]>();
  const fetchAbilities = async () => {
    const response = await axios.get<ApiResponse<AbilityGetDto[]>>(
      `${baseUrl}/api/abilities`
    );
    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err);
      });
    } else {
      setAbilities(response.data.data);
    }
  };

  useEffect(() => {
    fetchAbilities();
  }, []);

  return (
    <>
      <div>
        <Header>Abilities</Header>
        {abilities ? (
          abilities.map((ability) => {
            return (
              <div>
                <Container textAlign="left">
                  <p>{ability.name}</p>
                </Container>
                <Divider />
              </div>
            );
          })
        ) : (
          <div>No Abilities</div>
        )}
      </div>
    </>
  );
};
