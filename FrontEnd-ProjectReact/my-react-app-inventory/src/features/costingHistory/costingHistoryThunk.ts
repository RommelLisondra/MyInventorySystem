
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as costingHistoryService from "../../services/costingHistoryService";
import type { CostingHistory } from "../../types/costingHistory";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all CostingHistorys
export const fetchCostingHistorys = createAsyncThunk(
  "costingHistory/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await costingHistoryService.getCostingHistory();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all CostingHistorys By page
export const fetchCostingHistorysPaged = createAsyncThunk<PagedResponse<CostingHistory[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "costingHistory/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await costingHistoryService.getCostingHistorysPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single CostingHistory
export const fetchCostingHistoryById = createAsyncThunk(
  "costingHistory/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await costingHistoryService.getCostingHistoryById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new CostingHistory
export const addCostingHistory = createAsyncThunk(
  "costingHistory/add",
  async (CostingHistory: CostingHistory, thunkAPI) => {
    try {
      return await costingHistoryService.createCostingHistory(CostingHistory);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateCostingHistoryById = createAsyncThunk<CostingHistory, CostingHistory>(
  "costingHistory/update",
  async (CostingHistory, thunkAPI) => {
    try {
      await costingHistoryService.updateCostingHistory(CostingHistory);
      // Return updated CostingHistory so reducer gets correct payload type
      return CostingHistory;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete CostingHistory
export const deleteCostingHistoryById = createAsyncThunk<number, number>(
  "costingHistory/delete",
  async (id, thunkAPI) => {
    try {
      await costingHistoryService.deleteCostingHistory(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search CostingHistorys
export const searchCostingHistory = createAsyncThunk(
  "costingHistory/search",
  async (name: string, thunkAPI) => {
    try {
      return await costingHistoryService.searchCostingHistory(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

