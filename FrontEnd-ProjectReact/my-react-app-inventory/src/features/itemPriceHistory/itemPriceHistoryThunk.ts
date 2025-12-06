
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemPriceHistoryService from "../../services/itemPriceHistoryService";
import type { ItemPriceHistory } from "../../types/itemPriceHistory";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemPriceHistorys
export const fetchItemPriceHistorys = createAsyncThunk(
  "itemPriceHistory/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemPriceHistoryService.getItemPriceHistory();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ItemPriceHistorys By page
export const fetchItemPriceHistorysPaged = createAsyncThunk<PagedResponse<ItemPriceHistory[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemPriceHistory/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemPriceHistoryService.getItemPriceHistorysPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemPriceHistory
export const fetchItemPriceHistoryById = createAsyncThunk(
  "itemPriceHistory/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemPriceHistoryService.getItemPriceHistoryById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new ItemPriceHistory
export const addItemPriceHistory = createAsyncThunk(
  "itemPriceHistory/add",
  async (ItemPriceHistory: ItemPriceHistory, thunkAPI) => {
    try {
      return await itemPriceHistoryService.createItemPriceHistory(ItemPriceHistory);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemPriceHistoryById = createAsyncThunk<ItemPriceHistory, ItemPriceHistory>(
  "itemPriceHistory/update",
  async (ItemPriceHistory, thunkAPI) => {
    try {
      await itemPriceHistoryService.updateItemPriceHistory(ItemPriceHistory);
      // Return updated ItemPriceHistory so reducer gets correct payload type
      return ItemPriceHistory;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete ItemPriceHistory
export const deleteItemPriceHistoryById = createAsyncThunk<number, number>(
  "itemPriceHistory/delete",
  async (id, thunkAPI) => {
    try {
      await itemPriceHistoryService.deleteItemPriceHistory(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search ItemPriceHistorys
export const searchItemPriceHistorys = createAsyncThunk(
  "itemPriceHistory/search",
  async (name: string, thunkAPI) => {
    try {
      return await itemPriceHistoryService.searchItemPriceHistory(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

