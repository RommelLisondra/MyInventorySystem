
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchEmployeeDelivereds,
  fetchEmployeeDeliveredById,
  addEmployeeDelivered,
  updateEmployeeDeliveredById,
  deleteEmployeeDeliveredById,
  searchEmployeeDelivereds,
  fetchEmployeeDeliveredsPaged,
} from "./employeeDelivererThunk";
import type { EmployeeDelivered } from "../../types/employeeDelivered";
import type { PagedResponse } from "../../types/pagedResponse";

interface EmployeeDeliveredState {
  employeeDelivered: EmployeeDelivered[];
  selectedEmployeeDelivered: EmployeeDelivered | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: EmployeeDeliveredState = {
  employeeDelivered: [],
  selectedEmployeeDelivered: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const EmployeeDeliveredSlice = createSlice({
  name: "EmployeeDelivered",
  initialState,
  reducers: {
    clearSelectedEmployeeDelivered: (state) => {
      state.selectedEmployeeDelivered = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchEmployeeDelivereds.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeDelivereds.fulfilled, (state, action: PayloadAction<EmployeeDelivered[]>) => {
        state.loading = false;
        state.employeeDelivered = action.payload;
      })
      .addCase(fetchEmployeeDelivereds.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchEmployeeDeliveredsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeDeliveredsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<EmployeeDelivered[]>>) => {
          state.loading = false;
          state.employeeDelivered = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchEmployeeDeliveredsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchEmployeeDeliveredById.fulfilled, (state, action: PayloadAction<EmployeeDelivered>) => {
        state.selectedEmployeeDelivered = action.payload;
      })

      // Add
      .addCase(addEmployeeDelivered.fulfilled, (state: { employeeDelivered: EmployeeDelivered[]; }, action: PayloadAction<EmployeeDelivered>) => {
        state.employeeDelivered.push(action.payload);
      })

      // Update
      .addCase(updateEmployeeDeliveredById.fulfilled, (state: { employeeDelivered: any[]; }, action: PayloadAction<EmployeeDelivered>) => {
        const index = state.employeeDelivered.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.employeeDelivered[index] = action.payload;
      })

      // Delete
      .addCase(deleteEmployeeDeliveredById.fulfilled, (state: { employeeDelivered: any[]; }, action: PayloadAction<number>) => {
        state.employeeDelivered = state.employeeDelivered.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchEmployeeDelivereds.fulfilled, (state: { employeeDelivered: EmployeeDelivered[]; }, action: PayloadAction<EmployeeDelivered[]>) => {
        state.employeeDelivered = action.payload;
      });
  },
});

export const { clearSelectedEmployeeDelivered } = EmployeeDeliveredSlice.actions;
export default EmployeeDeliveredSlice.reducer;
