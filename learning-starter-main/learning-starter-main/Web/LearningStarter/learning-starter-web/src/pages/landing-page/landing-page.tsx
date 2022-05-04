import React from "react";
import { Header, Button, Modal, Icon } from "semantic-ui-react";
import "./landing-page.css";
import { useState } from "react";

export const LandingPage = () => {
  const [open, setOpen] = useState(false);

  return (
    <div>
      <div className="home-page-container">
        <Header size="large">Home Page</Header>
      </div>
      <div className="home-button">
        <Modal
          className="modal"
          onOpen={() => setOpen(true)}
          onClose={() => setOpen(false)}
          open={open}
          closeIcon
          dimmer="blurring"
          trigger={
            <Button color="blue" size="massive" icon>
              In development
              <Icon name="sync" />
            </Button>
          }
        >
          <div className="modal-content">
            <div>
              <Header size="huge" textAlign="center">
                You are about to leave the site. Are you sure you want to
                proceed?
              </Header>
              <div className="buttons">
                <Button
                  color="grey"
                  size="huge"
                  onClick={(open) => setOpen(false)}
                >
                  No
                </Button>
                <a href="https://www.youtube.com/watch?v=dQw4w9WgXcQ">
                  <Button color="blue" size="huge">
                    Yes
                  </Button>
                </a>
              </div>
            </div>
          </div>
        </Modal>
      </div>
    </div>
  );
};
