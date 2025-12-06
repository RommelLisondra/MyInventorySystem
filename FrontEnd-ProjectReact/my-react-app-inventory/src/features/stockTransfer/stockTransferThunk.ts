
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as stockTransferService from "../../services/stockTransferService";
import type { StockTransfer } from "../../types/stockTransfer";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all StockTransfer
export const fetchstockTransfers = createAsyncThunk(
  "stockTransfer/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await stockTransferService.getStockTransfers();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchstockTransfersPaged = createAsyncThunk<PagedResponse<StockTransfer[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "stockTransfer/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await stockTransferService.getStockTransfersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single StockTransfer
export const fetchstockTransferById = createAsyncThunk(
  "stockTransfer/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await stockTransferService.getStockTransferById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new StockTransfer
export const addstockTransfer = createAsyncThunk(
  "stockTransfer/add",
  async (stockTransfer: StockTransfer, thunkAPI) => {
    try {
      return await stockTransferService.createStockTransfer(stockTransfer);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updatestockTransferById = createAsyncThunk<StockTransfer, StockTransfer>(
  "stockTransfer/update",
  async (stockTransfer, thunkAPI) => {
    try {
      await stockTransferService.updateStockTransfer(stockTransfer);
      // Return updated StockTransfer so reducer gets correct payload type
      return stockTransfer;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete StockTransfer
export const deletestockTransferById = createAsyncThunk<number, number>(
  "stockTransfer/delete",
  async (id, thunkAPI) => {
    try {
      await stockTransferService.deleteStockTransfer(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search StockTransfer
export const searchstockTransfers = createAsyncThunk(
  "stockTransfer/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await stockTransferService.searchStockTransfers(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

