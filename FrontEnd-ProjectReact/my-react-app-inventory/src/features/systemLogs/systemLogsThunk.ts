
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as systemLogsservice from "../../services/systemLogsservice";
import type { SystemLog } from "../../types/systemLog";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all SystemLog
export const fetchSystemLogs = createAsyncThunk(
  "systemLog/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await systemLogsservice.getSystemLogs();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchSystemLogsPaged = createAsyncThunk<PagedResponse<SystemLog[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "systemLog/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await systemLogsservice.getSystemLogsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single SystemLog
export const fetchSystemLogById = createAsyncThunk(
  "systemLog/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await systemLogsservice.getSystemLogById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new SystemLog
export const addSystemLog = createAsyncThunk(
  "systemLog/add",
  async (systemLog: SystemLog, thunkAPI) => {
    try {
      return await systemLogsservice.createSystemLog(systemLog);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateSystemLogById = createAsyncThunk<SystemLog, SystemLog>(
  "systemLog/update",
  async (systemLog, thunkAPI) => {
    try {
      await systemLogsservice.updateSystemLog(systemLog);
      // Return updated SystemLog so reducer gets correct payload type
      return systemLog;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete SystemLog
export const deleteSystemLogById = createAsyncThunk<number, number>(
  "systemLog/delete",
  async (id, thunkAPI) => {
    try {
      await systemLogsservice.deleteSystemLog(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search SystemLog
export const searchSystemLogs = createAsyncThunk(
  "systemLog/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await systemLogsservice.searchSystemLogs(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

