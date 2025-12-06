
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchEmployeeApprover,
  fetchEmployeeApproverById,
  addEmployeeApprover,
  updateEmployeeApproverById,
  deleteEmployeeApproverById,
  searchEmployeeApprovers,
  fetchEmployeeApproverPaged
} from "./employeeApproverThunk";
import type { EmployeeApprover } from "../../types/employeeApprover";
import type { PagedResponse } from "../../types/pagedResponse";

interface EmployeeApproverState {
  employeeApprover: EmployeeApprover[];
  selectedEmployeeApprover: EmployeeApprover | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: EmployeeApproverState = {
  employeeApprover: [],
  selectedEmployeeApprover: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const employeeApproverSlice = createSlice({
  name: "employeeApprover",
  initialState,
  reducers: {
    clearSelectedEmployeeApprover: (state) => {
      state.selectedEmployeeApprover = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchEmployeeApprover.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeApprover.fulfilled, (state, action: PayloadAction<EmployeeApprover[]>) => {
        state.loading = false;
        state.employeeApprover = action.payload;
      })
      .addCase(fetchEmployeeApprover.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchEmployeeApproverPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeeApproverPaged.fulfilled,(state, action: PayloadAction<PagedResponse<EmployeeApprover[]>>) => {
        state.loading = false;
        state.employeeApprover = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchEmployeeApproverPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchEmployeeApproverById.fulfilled, (state, action: PayloadAction<EmployeeApprover>) => {
        state.selectedEmployeeApprover = action.payload;
      })

      // Add
      .addCase(addEmployeeApprover.fulfilled, (state, action: PayloadAction<EmployeeApprover>) => {
        state.employeeApprover.push(action.payload);
      })

      // Update
      .addCase(updateEmployeeApproverById.fulfilled, (state, action: PayloadAction<EmployeeApprover>) => {
        const index = state.employeeApprover.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.employeeApprover[index] = action.payload;
      })

      // Delete
      .addCase(deleteEmployeeApproverById.fulfilled, (state, action: PayloadAction<number>) => {
        state.employeeApprover = state.employeeApprover.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchEmployeeApprovers.fulfilled, (state, action: PayloadAction<EmployeeApprover[]>) => {
        state.employeeApprover = action.payload;
      });
  },
});

export const { clearSelectedEmployeeApprover } = employeeApproverSlice.actions;
export default employeeApproverSlice.reducer;
