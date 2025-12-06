
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchEmployees,
  fetchEmployeeById,
  addEmployee,
  updateEmployeeById,
  deleteEmployeeById,
  searchEmployees,
  fetchEmployeesPaged
} from "./employeeThunk";
import type { Employee } from "../../types/employee";
import type { PagedResponse } from "../../types/pagedResponse";

interface EmployeeState {
  employees: Employee[];
  selectedEmployee: Employee | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: EmployeeState = {
  employees: [],
  selectedEmployee: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const employeeSlice = createSlice({
  name: "employee",
  initialState,
  reducers: {
    clearSelectedEmployee: (state) => {
      state.selectedEmployee = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchEmployees.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployees.fulfilled, (state, action: PayloadAction<Employee[]>) => {
        state.loading = false;
        state.employees = action.payload;
      })
      .addCase(fetchEmployees.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchEmployeesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchEmployeesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Employee[]>>) => {
        state.loading = false;
        state.employees = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchEmployeesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchEmployeeById.fulfilled, (state, action: PayloadAction<Employee>) => {
        state.selectedEmployee = action.payload;
      })

      // Add
      .addCase(addEmployee.fulfilled, (state, action: PayloadAction<Employee>) => {
        state.employees.push(action.payload);
      })

      // Update
      .addCase(updateEmployeeById.fulfilled, (state, action: PayloadAction<Employee>) => {
        const index = state.employees.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.employees[index] = action.payload;
      })

      // Delete
      .addCase(deleteEmployeeById.fulfilled, (state, action: PayloadAction<number>) => {
        state.employees = state.employees.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchEmployees.fulfilled, (state, action: PayloadAction<Employee[]>) => {
        state.employees = action.payload;
      });
  },
});

export const { clearSelectedEmployee } = employeeSlice.actions;
export default employeeSlice.reducer;
