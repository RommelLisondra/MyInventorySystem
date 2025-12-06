
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as inventoryAdjustmentService from "../../services/inventoryAdjustmentService";
import type { InventoryAdjustment } from "../../types/inventoryAdjustment";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all InventoryAdjustments
export const fetchInventoryAdjustments = createAsyncThunk(
  "inventoryAdjustment/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await inventoryAdjustmentService.getInventoryAdjustments();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all InventoryAdjustments By page
export const fetchInventoryAdjustmentsPaged = createAsyncThunk<PagedResponse<InventoryAdjustment[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "inventoryAdjustment/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await inventoryAdjustmentService.getInventoryAdjustmentsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single InventoryAdjustment
export const fetchInventoryAdjustmentById = createAsyncThunk(
  "inventoryAdjustment/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await inventoryAdjustmentService.getInventoryAdjustmentById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new InventoryAdjustment
export const addInventoryAdjustment = createAsyncThunk(
  "inventoryAdjustment/add",
  async (inventoryAdjustment: InventoryAdjustment, thunkAPI) => {
    try {
      return await inventoryAdjustmentService.createInventoryAdjustment(inventoryAdjustment);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateInventoryAdjustmentById = createAsyncThunk<InventoryAdjustment, InventoryAdjustment>(
  "inventoryAdjustment/update",
  async (inventoryAdjustment, thunkAPI) => {
    try {
      await inventoryAdjustmentService.updateInventoryAdjustment(inventoryAdjustment);
      // Return updated InventoryAdjustment so reducer gets correct payload type
      return inventoryAdjustment;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete InventoryAdjustment
export const deleteInventoryAdjustmentById = createAsyncThunk<number, number>(
  "inventoryAdjustment/delete",
  async (id, thunkAPI) => {
    try {
      await inventoryAdjustmentService.deleteInventoryAdjustment(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search InventoryAdjustments
export const searchInventoryAdjustments = createAsyncThunk(
  "inventoryAdjustment/search",
  async (name: string, thunkAPI) => {
    try {
      return await inventoryAdjustmentService.searchInventoryAdjustments(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

