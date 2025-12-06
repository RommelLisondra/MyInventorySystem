
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemInventoryService from "../../services/itemInventoryService";
import type { ItemInventory } from "../../types/itemInventory";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemInventory
export const fetchItemInventory = createAsyncThunk(
  "itemInventory/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemInventoryService.getItemInventorys();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ItemInventory By page
export const fetchItemInventoryPaged = createAsyncThunk<PagedResponse<ItemInventory[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemInventory/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemInventoryService.getItemInventorysPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemInventory
export const fetchItemInventoryById = createAsyncThunk(
  "itemInventory/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemInventoryService.getItemInventoryById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Item
export const addItemInventory = createAsyncThunk(
  "itemInventory/add",
  async (item: ItemInventory, thunkAPI) => {
    try {
      return await itemInventoryService.createItemInventory(item);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemInventoryById = createAsyncThunk<ItemInventory, ItemInventory>(
  "itemInventory/update",
  async (item, thunkAPI) => {
    try {
      await itemInventoryService.updateItemInventory(item);
      // Return updated Item so reducer gets correct payload type
      return item;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Item
export const deleteItemInventoryById = createAsyncThunk<number, number>(
  "itemInventory/delete",
  async (id, thunkAPI) => {
    try {
      await itemInventoryService.deleteItemInventory(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Item
export const searchItemInventory = createAsyncThunk(
  "itemInventory/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await itemInventoryService.searchItemInventorys(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

