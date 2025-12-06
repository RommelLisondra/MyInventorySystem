
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemService from "../../services/itemService";
import type { Item } from "../../types/item";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Item
export const fetchItems = createAsyncThunk(
  "item/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemService.getItems();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all Item By page
export const fetchItemsPaged = createAsyncThunk<PagedResponse<Item[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "item/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemService.getItemsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Item
export const fetchItemById = createAsyncThunk(
  "item/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemService.getItemById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Item
export const addItem = createAsyncThunk(
  "item/add",
  async (item: Item, thunkAPI) => {
    try {
      return await itemService.createItem(item);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemById = createAsyncThunk<Item, Item>(
  "item/update",
  async (item, thunkAPI) => {
    try {
      await itemService.updateItem(item);
      // Return updated Item so reducer gets correct payload type
      return item;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Item
export const deleteItemById = createAsyncThunk<number, number>(
  "item/delete",
  async (id, thunkAPI) => {
    try {
      await itemService.deleteItem(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Item
export const searchItems = createAsyncThunk(
  "item/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await itemService.searchItems(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

