
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemSupplierService from "../../services/itemSupplierService";
import type { ItemSupplier } from "../../types/itemSupplier";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemSupplier
export const fetchItemSuppliers = createAsyncThunk(
  "itemSupplier/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemSupplierService.getItemSuppliers();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ItemSupplier By page
export const fetchItemSuppliersPaged = createAsyncThunk<PagedResponse<ItemSupplier[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemSupplier/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemSupplierService.getItemSuppliersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemSupplier
export const fetchItemSupplierById = createAsyncThunk(
  "itemSupplier/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemSupplierService.getItemSupplierById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Item
export const addItemSupplier = createAsyncThunk(
  "itemSupplier/add",
  async (item: ItemSupplier, thunkAPI) => {
    try {
      return await itemSupplierService.createItemSupplier(item);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemSupplierById = createAsyncThunk<ItemSupplier, ItemSupplier>(
  "itemSupplier/update",
  async (item, thunkAPI) => {
    try {
      await itemSupplierService.updateItemSupplier(item);
      // Return updated Item so reducer gets correct payload type
      return item;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Item
export const deleteItemSupplierById = createAsyncThunk<number, number>(
  "itemSupplier/delete",
  async (id, thunkAPI) => {
    try {
      await itemSupplierService.deleteItemSupplier(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Item
export const searchItemSuppliers = createAsyncThunk(
  "itemSupplier/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await itemSupplierService.searchItemSuppliers(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

