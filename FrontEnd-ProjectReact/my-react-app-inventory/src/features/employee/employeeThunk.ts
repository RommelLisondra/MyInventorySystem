
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as empoyeeService from "../../services/empoyeeService";
import type { Employee } from "../../types/employee";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all employee
export const fetchEmployees = createAsyncThunk(
  "employee/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await empoyeeService.getEmployees();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all employee By page
export const fetchEmployeesPaged = createAsyncThunk<PagedResponse<Employee[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "employee/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await empoyeeService.getEmployeesPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single employee
export const fetchEmployeeById = createAsyncThunk(
  "employee/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await empoyeeService.getEmployeeById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new employee
export const addEmployee = createAsyncThunk(
  "employee/add",
  async (employee: Employee, thunkAPI) => {
    try {
      return await empoyeeService.createEmployee(employee);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateEmployeeById = createAsyncThunk<Employee, Employee>(
  "employee/update",
  async (employee, thunkAPI) => {
    try {
      await empoyeeService.updateEmployee(employee);
      // Return updated employee so reducer gets correct payload type
      return employee;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete employee
export const deleteEmployeeById = createAsyncThunk<number, number>(
  "employee/delete",
  async (id, thunkAPI) => {
    try {
      await empoyeeService.deleteEmployee(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search employee
export const searchEmployees = createAsyncThunk(
  "employee/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await empoyeeService.searchEmployees(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

