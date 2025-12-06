import type { RootState } from "../../app/store";

export const selectLocations = (state: RootState) => state.locations.locations;
export const selectLocationsLoading = (state: RootState) => state.locations.loading;
export const selectLocationError = (state: RootState) => state.locations.error;

// Optional: selector para sa isang customer by id
export const selectLocationById = (id: number) => (state: RootState) =>
  state.locations.locations.find((c) => c.id === id);
