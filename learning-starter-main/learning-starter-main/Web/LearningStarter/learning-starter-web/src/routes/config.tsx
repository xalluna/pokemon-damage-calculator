import React from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { LandingPage } from "../pages/landing-page/landing-page";
import { NotFoundPage } from "../pages/not-found";
import { useUser } from "../authentication/use-auth";
import { UserPage } from "../pages/user-page/user-page";
import { PageWrapper } from "../components/page-wrapper/page-wrapper";
import { AbilityCreatePage } from "../pages/abilities/ability-create-page/ability-create-page";
import { AbilityListingPage } from "../pages/abilities/ability-listing-page/ability-listing-page";
import { PokemonListingPage } from "../pages/pokemon/pokemon-listing-page/pokemon-listing-page";
import { PokemonCreatePage } from "../pages/pokemon/pokemon-create-page/pokemon-create-page";
import { PokemonUpdatePage } from "../pages/pokemon/pokemon-update-page/pokemon-update-page";

//This is where you will declare all of your routes (the ones that show up in the search bar)
export const routes = {
  root: `/`,
  home: `/home`,
  user: `/user`,
  abilities: {
    create: "/abilities/create",
    listing: "/abilities",
  },
  pokemon: {
    create: "/pokemon/create",
    update: "/pokemon/:id",
    listing: "/pokemon",
  },
};

//This is where you will tell React Router what to render when the path matches the route specified.
export const Routes = () => {
  //Calling the useUser() from the use-auth.tsx in order to get user information
  const user = useUser();
  return (
    <>
      {/* The page wrapper is what shows the NavBar at the top, it is around all pages inside of here. */}
      <PageWrapper user={user}>
        <Switch>
          {/* When path === / render LandingPage */}
          <Route path={routes.home} exact>
            <LandingPage />
          </Route>
          {/* When path === /iser render UserPage */}
          <Route path={routes.user} exact>
            <UserPage />
          </Route>
          {/* Going to route "localhost:5001/" will go to homepage */}
          <Route path={routes.root} exact>
            <Redirect to={routes.home} />
          </Route>

          <Route path={routes.abilities.listing} exact>
            <AbilityListingPage />
          </Route>

          <Route path={routes.abilities.create} exact>
            <AbilityCreatePage />
          </Route>

          <Route path={routes.pokemon.listing} exact>
            <PokemonListingPage />
          </Route>

          <Route path={routes.pokemon.create} exact>
            <PokemonCreatePage />
          </Route>
          <Route path={routes.pokemon.update} exact>
            <PokemonUpdatePage />
          </Route>

          {/* This should always come last.  
            If the path has no match, show page not found */}
          <Route path="*" exact>
            <NotFoundPage />
          </Route>
        </Switch>
      </PageWrapper>
    </>
  );
};
