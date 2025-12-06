
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as employeeSalesRefService from "../../services/employeeSalesRefService";
import type { EmployeeSalesRef } from "../../types/employeeSalesRef";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all EmployeeSalesRefs
export const fetchEmployeeSalesRefs = createAsyncThunk(
  "employeeSalesRef/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await employeeSalesRefService.getEmployeeSalesRef();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all EmployeeSalesRefs By page
export const fetchEmployeeSalesRefsPaged = createAsyncThunk<PagedResponse<EmployeeSalesRef[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "employeeSalesRef/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await employeeSalesRefService.getEmployeeSalesRefPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single EmployeeSalesRef
export const fetchEmployeeSalesRefById = createAsyncThunk(
  "employeeSalesRef/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await employeeSalesRefService.getEmployeeSalesRefById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new EmployeeSalesRef
export const addEmployeeSalesRef = createAsyncThunk(
  "employeeSalesRef/add",
  async (employeeSalesRef: EmployeeSalesRef, thunkAPI) => {
    try {
      return await employeeSalesRefService.createEmployeeSalesRef(employeeSalesRef);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateEmployeeSalesRefById = createAsyncThunk<EmployeeSalesRef, EmployeeSalesRef>(
  "employeeSalesRef/update",
  async (employeeSalesRef, thunkAPI) => {
    try {
      await employeeSalesRefService.updateEmployeeSalesRef(employeeSalesRef);
      // Return updated EmployeeSalesRef so reducer gets correct payload type
      return employeeSalesRef;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete EmployeeSalesRef
export const deleteEmployeeSalesRefById = createAsyncThunk<number, number>(
  "employeeSalesRef/delete",
  async (id, thunkAPI) => {
    try {
      await employeeSalesRefService.deleteEmployeeSalesRef(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search EmployeeSalesRefs
export const searchEmployeeSalesRefs = createAsyncThunk(
  "employeeSalesRef/search",
  async (name: string, thunkAPI) => {
    try {
      return await employeeSalesRefService.searchEmployeeSalesRef(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

