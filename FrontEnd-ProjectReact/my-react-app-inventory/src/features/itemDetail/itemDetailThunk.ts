
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemDetailService from "../../services/itemDetailService";
import type { ItemDetail } from "../../types/itemDetail";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all itemDetail
export const fetchItemDetails = createAsyncThunk(
  "itemDetail/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemDetailService.getItemDetails();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all itemDetail By page
export const fetchItemDetailsPaged = createAsyncThunk<PagedResponse<ItemDetail[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemDetail/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemDetailService.getItemDetailsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single itemDetail
export const fetchItemDetailById = createAsyncThunk(
  "itemDetail/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemDetailService.getItemDetailById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Item
export const addItemDetail = createAsyncThunk(
  "itemDetail/add",
  async (item: ItemDetail, thunkAPI) => {
    try {
      return await itemDetailService.createItemDetail(item);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemDetailById = createAsyncThunk<ItemDetail, ItemDetail>(
  "itemDetail/update",
  async (item, thunkAPI) => {
    try {
      await itemDetailService.updateItemDetail(item);
      // Return updated Item so reducer gets correct payload type
      return item;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Item
export const deleteItemDetailById = createAsyncThunk<number, number>(
  "itemDetail/delete",
  async (id, thunkAPI) => {
    try {
      await itemDetailService.deleteItemDetail(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Item
export const searchItemDetails = createAsyncThunk(
  "itemDetail/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await itemDetailService.searchItemDetails(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

