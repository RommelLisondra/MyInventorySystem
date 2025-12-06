
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as roleService from "../../services/roleService";
import type { Role } from "../../types/role";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Role
export const fetchRoles = createAsyncThunk(
  "role/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await roleService.getRoles();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchRolesPaged = createAsyncThunk<PagedResponse<Role[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "role/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await roleService.getRolesPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Role
export const fetchRoleById = createAsyncThunk(
  "role/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await roleService.getRoleById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Role
export const addRole = createAsyncThunk(
  "role/add",
  async (role: Role, thunkAPI) => {
    try {
      return await roleService.createRole(role);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateRoleById = createAsyncThunk<Role, Role>(
  "role/update",
  async (role, thunkAPI) => {
    try {
      await roleService.updateRole(role);
      // Return updated Role so reducer gets correct payload type
      return role;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Role
export const deleteRoleById = createAsyncThunk<number, number>(
  "role/delete",
  async (id, thunkAPI) => {
    try {
      await roleService.deleteRole(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Role
export const searchRoles = createAsyncThunk(
  "role/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await roleService.searchRoles(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

