import axios from "axios";
import React, { useEffect, useState } from "react";
import { BaseUrl } from "../../../constants/env-vars";
import { AbilityGetDto, ApiResponse } from "../../../constants/types";

export const AbilityListingPage = () => {
    const [abilities, setAbilities] = useState<AbilityGetDto[]>()
    const fetchAbilities = async() =>{
        const response = await axios.get<ApiResponse<AbilityGetDto[]>>(`${BaseUrl}/api/abilities`);
        if(response.data.hasErrors){
            response.data.errors.forEach(err =>{
                console.log(err);
            })
        }else{
            setAbilities(response.data.data)
        }
    }

    useEffect( () =>{
        fetchAbilities();
    }, []);

    return (
        <>
            <div>
                {abilities ? (
                abilities.map(ability => {
                    return (
                        <div>
                            <div>
                                Ability
                            </div>
                            <div>
                                {ability.name}
                            </div>
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