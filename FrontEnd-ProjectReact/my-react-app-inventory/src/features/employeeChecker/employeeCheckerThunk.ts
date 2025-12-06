
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as employeeCheckerService from "../../services/employeeCheckerService";
import type { EmployeeChecker } from "../../types/employeeChecker";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all EmployeeCheckers
export const fetchEmployeeCheckers = createAsyncThunk(
  "employeeChecker/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await employeeCheckerService.getEmployeeCheckers();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all EmployeeCheckers By page
export const fetchEmployeeCheckersPaged = createAsyncThunk<PagedResponse<EmployeeChecker[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "employeeChecker/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await employeeCheckerService.getEmployeeCheckersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single EmployeeChecker
export const fetchEmployeeCheckerById = createAsyncThunk(
  "employeeChecker/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await employeeCheckerService.getEmployeeCheckerById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new EmployeeChecker
export const addEmployeeChecker = createAsyncThunk(
  "employeeChecker/add",
  async (employeeChecker: EmployeeChecker, thunkAPI) => {
    try {
      return await employeeCheckerService.createEmployeeChecker(employeeChecker);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateEmployeeCheckerById = createAsyncThunk<EmployeeChecker, EmployeeChecker>(
  "employeeChecker/update",
  async (employeeChecker, thunkAPI) => {
    try {
      await employeeCheckerService.updateEmployeeChecker(employeeChecker);
      // Return updated EmployeeChecker so reducer gets correct payload type
      return employeeChecker;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete EmployeeChecker
export const deleteEmployeeCheckerById = createAsyncThunk<number, number>(
  "employeeChecker/delete",
  async (id, thunkAPI) => {
    try {
      await employeeCheckerService.deleteEmployeeChecker(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search EmployeeCheckers
export const searchEmployeeCheckers = createAsyncThunk(
  "employeeChecker/search",
  async (name: string, thunkAPI) => {
    try {
      return await employeeCheckerService.searchEmployeeCheckers(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

