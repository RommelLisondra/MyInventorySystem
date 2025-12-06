import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchLocations,
  fetchLocationById,
  searchLocations,
  addLocation,
  updateLocationById,
  deleteLocationById,
} from "../features/locations/locationsThunk";
import type { Location } from "../types/location";

interface UseLocationReturn {
  locations: Location[];
  selectedLocation: Location | null;
  loading: boolean;
  error: string | null;
  addNewLocation: (Location: Location) => void;
  updateLocation: (Location: Location) => void;
  deleteLocation: (id: number) => void;
  reloadLocations: () => void;
  getLocationById: (id: number) => void;
  searchLocation: (name: string) => void;
}

export const useLocation = (): UseLocationReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { locations, selectedLocation, loading, error } = useSelector(
    (state: RootState) => state.locations
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchLocations());
  }, [dispatch]);

  const addNewLocation = useCallback(
    (Location: Location) => {
      dispatch(addLocation(Location));
    },
    [dispatch]
  );

  const updateLocation = useCallback(
    (Location: Location) => {
      dispatch(updateLocationById(Location));
    },
    [dispatch]
  );

  const deleteLocation = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Location?")) {
        dispatch(deleteLocationById(id));
      }
    },
    [dispatch]
  );

  const reloadLocations = useCallback(() => {
    dispatch(fetchLocations());
  }, [dispatch]);

  const getLocationById = useCallback(
    (id: number) => {
      dispatch(fetchLocationById(id));
    },
    [dispatch]
  );

  const searchLocation = useCallback(
    (keyword: string) => {
      dispatch(searchLocations(keyword));
    },
    [dispatch]
  );

  return {
    locations,
    selectedLocation,
    loading,
    error,
    addNewLocation,
    updateLocation,
    deleteLocation,
    reloadLocations,
    getLocationById,
    searchLocation,
  };
};
