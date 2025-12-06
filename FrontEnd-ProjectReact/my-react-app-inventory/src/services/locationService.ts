import { LOCATION_API } from "../constants/api";
import type { Location } from "../types/location";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getLocations = async (): Promise<Location[]> => {
  return authFetch(LOCATION_API);
};

export const getLocationById = async (id: number): Promise<Location> => {
  return authFetch(`${LOCATION_API}/${id}`);
};

export const getLocationsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<Location[]>> => {
  return authFetch(`${LOCATION_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const createLocation = async (location: Location): Promise<Location> => {
  return authFetch(LOCATION_API, {
    method: "POST",
    body: JSON.stringify(location),
  });
};

export const updateLocation = async (location: Location): Promise<void> => {
  await authFetch(`${LOCATION_API}/${location.id}`, {
    method: "PUT",
    body: JSON.stringify(location),
  });
};

export const deleteLocation = async (id: number): Promise<void> => {
  await authFetch(`${LOCATION_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchLocations = async (keyword: string): Promise<Location[]> => {
  return authFetch(`${LOCATION_API}/search?keyword=${encodeURIComponent(keyword)}`);
};