import React from "react";
import { useNavigate } from "react-router-dom";
import { useLocation } from "../../hooks/useLocation";
import LocationList from "../../components/location/LocationList";
import type { Location } from "../../types/location";
import { ROUTES } from "../../constants/routes";

const LocationListPage: React.FC = () => {
  const { locations, deleteLocation, reloadLocations,searchLocation } = useLocation();
  const navigate = useNavigate();

  return (
    <LocationList
          locations={locations}
          onDelete={deleteLocation}
          onEdit={(id) => navigate(ROUTES.LOCATION_EDIT.replace(":id", String(id)))}
          onReload={reloadLocations} 
          onUpdate={function (_location: Location): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchLocation}   />
  );
};

export default LocationListPage;