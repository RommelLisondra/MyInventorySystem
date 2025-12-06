
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as locationService from "../../services/locationService";
import type { Location } from "../../types/location";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Location
export const fetchLocations = createAsyncThunk(
  "location/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await locationService.getLocations();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchLocationsPaged = createAsyncThunk<PagedResponse<Location[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "location/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await locationService.getLocationsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Location
export const fetchLocationById = createAsyncThunk(
  "location/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await locationService.getLocationById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Location
export const addLocation = createAsyncThunk(
  "location/add",
  async (location: Location, thunkAPI) => {
    try {
      return await locationService.createLocation(location);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateLocationById = createAsyncThunk<Location, Location>(
  "location/update",
  async (location, thunkAPI) => {
    try {
      await locationService.updateLocation(location);
      // Return updated Location so reducer gets correct payload type
      return location;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Location
export const deleteLocationById = createAsyncThunk<number, number>(
  "location/delete",
  async (id, thunkAPI) => {
    try {
      await locationService.deleteLocation(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Location
export const searchLocations = createAsyncThunk(
  "location/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await locationService.searchLocations(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

