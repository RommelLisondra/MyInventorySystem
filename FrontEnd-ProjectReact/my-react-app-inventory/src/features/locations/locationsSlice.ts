
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchLocations,
  fetchLocationById,
  addLocation,
  updateLocationById,
  deleteLocationById,
  searchLocations,
  fetchLocationsPaged
} from "./locationsThunk";
import type { Location } from "../../types/location";
import type { PagedResponse } from "../../types/pagedResponse";

interface LocationState {
  locations: Location[];
  selectedLocation: Location | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: LocationState = {
  locations: [],
  selectedLocation: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const LocationSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedLocation = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchLocations.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchLocations.fulfilled, (state, action: PayloadAction<Location[]>) => {
        state.loading = false;
        state.locations = action.payload;
      })
      .addCase(fetchLocations.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchLocationsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchLocationsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Location[]>>) => {
        state.loading = false;
        state.locations = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchLocationsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchLocationById.fulfilled, (state, action: PayloadAction<Location>) => {
        state.selectedLocation = action.payload;
      })

      // Add
      .addCase(addLocation.fulfilled, (state, action: PayloadAction<Location>) => {
        state.locations.push(action.payload);
      })

      // Update
      .addCase(updateLocationById.fulfilled, (state, action: PayloadAction<Location>) => {
        const index = state.locations.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.locations[index] = action.payload;
      })

      // Delete
      .addCase(deleteLocationById.fulfilled, (state, action: PayloadAction<number>) => {
        state.locations = state.locations.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchLocations.fulfilled, (state, action: PayloadAction<Location[]>) => {
        state.locations = action.payload;
      });
  },
});

export const { clearSelectedItem } = LocationSlice.actions;
export default LocationSlice.reducer;
