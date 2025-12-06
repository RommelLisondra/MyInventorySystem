
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchEmployeeCheckers,
  fetchEmployeeCheckerById,
  addEmployeeChecker,
  updateEmployeeCheckerById,
  deleteEmployeeCheckerById,
  searchEmployeeCheckers,
  fetchEmployeeCheckersPaged,
} from "./employeeCheckerThunk";
import type { EmployeeChecker } from "../../types/employeeChecker";
import type { PagedResponse } from "../../types/pagedResponse";

interface EmployeeCheckerState {
  employeeCheckers: EmployeeChecker[];
  selectedEmployeeChecker: EmployeeChecker | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: EmployeeCheckerState = {
  employeeCheckers: [],
  selectedEmployeeChecker: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const EmployeeCheckerSlice = createSlice({
  name: "EmployeeChecker",
  initialState,
  reducers: {
    clearSelectedEmployeeChecker: (state) => {
      state.selectedEmployeeChecker = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchEmployeeCheckers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeCheckers.fulfilled, (state, action: PayloadAction<EmployeeChecker[]>) => {
        state.loading = false;
        state.employeeCheckers = action.payload;
      })
      .addCase(fetchEmployeeCheckers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchEmployeeCheckersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeCheckersPaged.fulfilled,(state, action: PayloadAction<PagedResponse<EmployeeChecker[]>>) => {
          state.loading = false;
          state.employeeCheckers = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchEmployeeCheckersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchEmployeeCheckerById.fulfilled, (state, action: PayloadAction<EmployeeChecker>) => {
        state.selectedEmployeeChecker = action.payload;
      })

      // Add
      .addCase(addEmployeeChecker.fulfilled, (state: { employeeCheckers: EmployeeChecker[]; }, action: PayloadAction<EmployeeChecker>) => {
        state.employeeCheckers.push(action.payload);
      })

      // Update
      .addCase(updateEmployeeCheckerById.fulfilled, (state: { employeeCheckers: any[]; }, action: PayloadAction<EmployeeChecker>) => {
        const index = state.employeeCheckers.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.employeeCheckers[index] = action.payload;
      })

      // Delete
      .addCase(deleteEmployeeCheckerById.fulfilled, (state: { employeeCheckers: any[]; }, action: PayloadAction<number>) => {
        state.employeeCheckers = state.employeeCheckers.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchEmployeeCheckers.fulfilled, (state: { employeeCheckers: EmployeeChecker[]; }, action: PayloadAction<EmployeeChecker[]>) => {
        state.employeeCheckers = action.payload;
      });
  },
});

export const { clearSelectedEmployeeChecker } = EmployeeCheckerSlice.actions;
export default EmployeeCheckerSlice.reducer;
