
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchEmployeeSalesRefs,
  fetchEmployeeSalesRefById,
  addEmployeeSalesRef,
  updateEmployeeSalesRefById,
  deleteEmployeeSalesRefById,
  searchEmployeeSalesRefs,
  fetchEmployeeSalesRefsPaged,
} from "./employeeSalesRefThunk";
import type { EmployeeSalesRef } from "../../types/employeeSalesRef";
import type { PagedResponse } from "../../types/pagedResponse";

interface EmployeeSalesRefState {
  employeeSalesRef: EmployeeSalesRef[];
  selectedEmployeeSalesRef: EmployeeSalesRef | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: EmployeeSalesRefState = {
  employeeSalesRef: [],
  selectedEmployeeSalesRef: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const EmployeeSalesRefSlice = createSlice({
  name: "EmployeeSalesRef",
  initialState,
  reducers: {
    clearSelectedEmployeeSalesRef: (state) => {
      state.selectedEmployeeSalesRef = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchEmployeeSalesRefs.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeSalesRefs.fulfilled, (state, action: PayloadAction<EmployeeSalesRef[]>) => {
        state.loading = false;
        state.employeeSalesRef = action.payload;
      })
      .addCase(fetchEmployeeSalesRefs.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchEmployeeSalesRefsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeSalesRefsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<EmployeeSalesRef[]>>) => {
          state.loading = false;
          state.employeeSalesRef = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchEmployeeSalesRefsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchEmployeeSalesRefById.fulfilled, (state, action: PayloadAction<EmployeeSalesRef>) => {
        state.selectedEmployeeSalesRef = action.payload;
      })

      // Add
      .addCase(addEmployeeSalesRef.fulfilled, (state: { employeeSalesRef: EmployeeSalesRef[]; }, action: PayloadAction<EmployeeSalesRef>) => {
        state.employeeSalesRef.push(action.payload);
      })

      // Update
      .addCase(updateEmployeeSalesRefById.fulfilled, (state: { employeeSalesRef: any[]; }, action: PayloadAction<EmployeeSalesRef>) => {
        const index = state.employeeSalesRef.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.employeeSalesRef[index] = action.payload;
      })

      // Delete
      .addCase(deleteEmployeeSalesRefById.fulfilled, (state: { employeeSalesRef: any[]; }, action: PayloadAction<number>) => {
        state.employeeSalesRef = state.employeeSalesRef.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchEmployeeSalesRefs.fulfilled, (state: { employeeSalesRef: EmployeeSalesRef[]; }, action: PayloadAction<EmployeeSalesRef[]>) => {
        state.employeeSalesRef = action.payload;
      });
  },
});

export const { clearSelectedEmployeeSalesRef } = EmployeeSalesRefSlice.actions;
export default EmployeeSalesRefSlice.reducer;
