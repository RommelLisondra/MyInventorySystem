
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as employeeDeliveredService from "../../services/employeeDeliveredService";
import type { EmployeeDelivered } from "../../types/employeeDelivered";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all EmployeeDelivereds
export const fetchEmployeeDelivereds = createAsyncThunk(
  "employeeDelivered/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await employeeDeliveredService.getEmployeeDelivered();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all EmployeeDelivereds By page
export const fetchEmployeeDeliveredsPaged = createAsyncThunk<PagedResponse<EmployeeDelivered[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "employeeDelivered/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await employeeDeliveredService.getEmployeeDeliveredPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single EmployeeDelivered
export const fetchEmployeeDeliveredById = createAsyncThunk(
  "employeeDelivered/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await employeeDeliveredService.getEmployeeDeliveredById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new EmployeeDelivered
export const addEmployeeDelivered = createAsyncThunk(
  "employeeDelivered/add",
  async (employeeDelivered: EmployeeDelivered, thunkAPI) => {
    try {
      return await employeeDeliveredService.createEmployeeDelivered(employeeDelivered);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateEmployeeDeliveredById = createAsyncThunk<EmployeeDelivered, EmployeeDelivered>(
  "employeeDelivered/update",
  async (employeeDelivered, thunkAPI) => {
    try {
      await employeeDeliveredService.updateEmployeeDelivered(employeeDelivered);
      // Return updated EmployeeDelivered so reducer gets correct payload type
      return employeeDelivered;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete EmployeeDelivered
export const deleteEmployeeDeliveredById = createAsyncThunk<number, number>(
  "employeeDelivered/delete",
  async (id, thunkAPI) => {
    try {
      await employeeDeliveredService.deleteEmployeeDelivered(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search EmployeeDelivereds
export const searchEmployeeDelivereds = createAsyncThunk(
  "employeeDelivered/search",
  async (name: string, thunkAPI) => {
    try {
      return await employeeDeliveredService.searchEmployeeDelivered(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

