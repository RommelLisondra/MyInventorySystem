
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as systemSettingsService from "../../services/systemSettingsService";
import type { SystemSetting } from "../../types/systemSetting";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all SystemSetting
export const fetchSystemSettings = createAsyncThunk(
  "SystemSetting/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await systemSettingsService.getSystemSettings();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchSystemSettingsPaged = createAsyncThunk<PagedResponse<SystemSetting[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "systemSetting/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await systemSettingsService.getSystemSettingsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single SystemSetting
export const fetchSystemSettingById = createAsyncThunk(
  "systemSetting/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await systemSettingsService.getSystemSettingById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new SystemSetting
export const addSystemSetting = createAsyncThunk(
  "systemSetting/add",
  async (systemSetting: SystemSetting, thunkAPI) => {
    try {
      return await systemSettingsService.createSystemSetting(systemSetting);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateSystemSettingById = createAsyncThunk<SystemSetting, SystemSetting>(
  "systemSetting/update",
  async (systemSetting, thunkAPI) => {
    try {
      await systemSettingsService.updateSystemSetting(systemSetting);
      // Return updated SystemSetting so reducer gets correct payload type
      return systemSetting;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete SystemSetting
export const deleteSystemSettingById = createAsyncThunk<number, number>(
  "systemSetting/delete",
  async (id, thunkAPI) => {
    try {
      await systemSettingsService.deleteSystemSetting(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search SystemSetting
export const searchSystemSettings = createAsyncThunk(
  "systemSetting/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await systemSettingsService.searchSystemSettings(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

