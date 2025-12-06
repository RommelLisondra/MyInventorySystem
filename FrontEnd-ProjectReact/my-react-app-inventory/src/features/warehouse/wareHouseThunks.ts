
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as warehouseService from "../../services/warehouseService";
import type { Warehouse } from "../../types/warehouse";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Warehouse
export const fetchWarehouses = createAsyncThunk(
  "warehouse/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await warehouseService.getWarehouses();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchWarehousesPaged = createAsyncThunk<PagedResponse<Warehouse[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "warehouse/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await warehouseService.getWarehousesPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Warehouse
export const fetchWarehouseById = createAsyncThunk(
  "warehouse/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await warehouseService.getWarehouseById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Warehouse
export const addWarehouse = createAsyncThunk(
  "warehouse/add",
  async (Warehouse: Warehouse, thunkAPI) => {
    try {
      return await warehouseService.createWarehouse(Warehouse);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateWarehouseById = createAsyncThunk<Warehouse, Warehouse>(
  "warehouse/update",
  async (Warehouse, thunkAPI) => {
    try {
      await warehouseService.updateWarehouse(Warehouse);
      // Return updated Warehouse so reducer gets correct payload type
      return Warehouse;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Warehouse
export const deleteWarehouseById = createAsyncThunk<number, number>(
  "warehouse/delete",
  async (id, thunkAPI) => {
    try {
      await warehouseService.deleteWarehouse(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Warehouse
export const searchWarehouses = createAsyncThunk(
  "warehouse/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await warehouseService.searchWarehouses(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

