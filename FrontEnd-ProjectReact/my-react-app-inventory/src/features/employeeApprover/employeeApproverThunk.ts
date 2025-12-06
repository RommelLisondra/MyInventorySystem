
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as employeeApproverService from "../../services/employeeApproverService";
import type { EmployeeApprover } from "../../types/employeeApprover";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all employee
export const fetchEmployeeApprover = createAsyncThunk(
  "employeeApprover/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await employeeApproverService.getEmployeeApprovers();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all employee By page
export const fetchEmployeeApproverPaged = createAsyncThunk<PagedResponse<EmployeeApprover[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "employeeApprover/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await employeeApproverService.getEmployeeApproversPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single employee
export const fetchEmployeeApproverById = createAsyncThunk(
  "employeeApprover/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await employeeApproverService.getEmployeeApproverById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new employee
export const addEmployeeApprover = createAsyncThunk(
  "employeeApprover/add",
  async (employee: EmployeeApprover, thunkAPI) => {
    try {
      return await employeeApproverService.createEmployeeApprover(employee);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateEmployeeApproverById = createAsyncThunk<EmployeeApprover, EmployeeApprover>(
  "employeeApprover/update",
  async (employee, thunkAPI) => {
    try {
      await employeeApproverService.updateEmployeeApprover(employee);
      // Return updated employee so reducer gets correct payload type
      return employee;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete employee
export const deleteEmployeeApproverById = createAsyncThunk<number, number>(
  "employeeApprover/delete",
  async (id, thunkAPI) => {
    try {
      await employeeApproverService.deleteEmployeeApprover(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search employee
export const searchEmployeeApprovers = createAsyncThunk(
  "employeeApprover/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await employeeApproverService.searchEmployeeApprovers(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

