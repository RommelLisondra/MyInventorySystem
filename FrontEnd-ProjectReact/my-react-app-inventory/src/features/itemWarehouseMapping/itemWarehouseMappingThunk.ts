
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemWarehouseMappingService from "../../services/itemWarehouseMappingService";
import type { ItemWarehouseMapping } from "../../types/itemWarehouse";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemWarehouseMappings
export const fetchItemWarehouseMappings = createAsyncThunk(
  "itemWarehouseMapping/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemWarehouseMappingService.getItemWarehouseMappings();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ItemWarehouseMappings By page
export const fetchItemWarehouseMappingsPaged = createAsyncThunk<PagedResponse<ItemWarehouseMapping[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemWarehouseMapping/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemWarehouseMappingService.getItemWarehouseMappingPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemWarehouseMapping
export const fetchItemWarehouseMappingById = createAsyncThunk(
  "itemWarehouseMapping/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemWarehouseMappingService.getItemWarehouseMappingById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new ItemWarehouseMapping
export const addItemWarehouseMapping = createAsyncThunk(
  "itemWarehouseMapping/add",
  async (ItemWarehouseMapping: ItemWarehouseMapping, thunkAPI) => {
    try {
      return await itemWarehouseMappingService.createItemWarehouseMapping(ItemWarehouseMapping);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemWarehouseMappingById = createAsyncThunk<ItemWarehouseMapping, ItemWarehouseMapping>(
  "itemWarehouseMapping/update",
  async (ItemWarehouseMapping, thunkAPI) => {
    try {
      await itemWarehouseMappingService.updateItemWarehouseMapping(ItemWarehouseMapping);
      // Return updated ItemWarehouseMapping so reducer gets correct payload type
      return ItemWarehouseMapping;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete ItemWarehouseMapping
export const deleteItemWarehouseMappingById = createAsyncThunk<number, number>(
  "itemWarehouseMapping/delete",
  async (id, thunkAPI) => {
    try {
      await itemWarehouseMappingService.deleteItemWarehouseMapping(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search ItemWarehouseMappings
export const searchItemWarehouseMappings = createAsyncThunk(
  "itemWarehouseMapping/search",
  async (name: string, thunkAPI) => {
    try {
      return await itemWarehouseMappingService.searchItemWarehouseMapping(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

