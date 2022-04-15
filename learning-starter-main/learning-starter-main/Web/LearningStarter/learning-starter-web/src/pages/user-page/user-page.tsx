import React from "react";
import { useUser } from "../../authentication/use-auth";
import { Header, Container, Divider } from "semantic-ui-react";
import "./user-page.css";

export const UserPage = () => {
  const user = useUser();
  return (
    <div className="user-page-container">
      <div>
        <Container textAlign="left">
          <p>{user.firstName}</p>
          <Divider />
          <p>{user.lastName}</p>
        </Container>
      </div>
    </div>
  );
};
