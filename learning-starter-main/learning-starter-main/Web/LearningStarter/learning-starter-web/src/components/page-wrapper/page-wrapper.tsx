import "./page-wrapper.css";
import React from "react";
import { UserDto } from "../../constants/types";
import { PrimaryNavigation } from "../navigation/navigation";

type PageWrapperProps = {
  user?: UserDto;
  children?: {};
};

//This is the wrapper that surrounds every page in the app.  Changes made here will be reflect all over.
export const PageWrapper: React.FC<PageWrapperProps> = ({ user }) => {
  return (
    <div className="content">
      <PrimaryNavigation user={user} />
    </div>
  );
};
