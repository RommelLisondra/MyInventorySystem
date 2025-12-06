
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as rolePermissionService from "../../services/rolePermissionService";
import type { RolePermission } from "../../types/rolePermission";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all rolePermission
export const fetchRolePermissions = createAsyncThunk(
  "rolePermission/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await rolePermissionService.getRolePermissions();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchRolePermissionsPaged = createAsyncThunk<PagedResponse<RolePermission[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "rolePermission/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await rolePermissionService.getRolePermissionPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single rolePermission
export const fetchRolePermissionById = createAsyncThunk(
  "rolePermission/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await rolePermissionService.getRolePermissionById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new rolePermission
export const addRolePermission = createAsyncThunk(
  "rolePermission/add",
  async (rolePermission: RolePermission, thunkAPI) => {
    try {
      return await rolePermissionService.createRolePermission(rolePermission);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateRolePermissionById = createAsyncThunk<RolePermission, RolePermission>(
  "rolePermission/update",
  async (rolePermission, thunkAPI) => {
    try {
      await rolePermissionService.updateRolePermission(rolePermission);
      // Return updated rolePermission so reducer gets correct payload type
      return rolePermission;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete rolePermission
export const deleteRolePermissionById = createAsyncThunk<number, number>(
  "rolePermission/delete",
  async (id, thunkAPI) => {
    try {
      await rolePermissionService.deleteRolePermission(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search rolePermission
export const searchRolePermissions = createAsyncThunk(
  "rolePermission/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await rolePermissionService.searchRolePermissions(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

