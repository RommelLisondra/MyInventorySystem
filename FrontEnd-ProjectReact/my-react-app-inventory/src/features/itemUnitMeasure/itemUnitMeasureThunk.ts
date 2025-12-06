
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemUnitMeasureService from "../../services/itemUnitMeasureService";
import type { ItemUnitMeasure } from "../../types/itemUnitMeasure";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemUnitMeasure
export const fetchItemUnitMeasures = createAsyncThunk(
  "itemUnitMeasure/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemUnitMeasureService.getItemUnitMeasures();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchItemUnitMeasuresPaged = createAsyncThunk<PagedResponse<ItemUnitMeasure[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemUnitMeasure/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemUnitMeasureService.getItemUnitMeasuresPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemUnitMeasure
export const fetchItemUnitMeasureById = createAsyncThunk(
  "itemUnitMeasure/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemUnitMeasureService.getItemUnitMeasureById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new ItemUnitMeasure
export const addItemUnitMeasure = createAsyncThunk(
  "itemUnitMeasure/add",
  async (ItemUnitMeasure: ItemUnitMeasure, thunkAPI) => {
    try {
      return await itemUnitMeasureService.createItemUnitMeasure(ItemUnitMeasure);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemUnitMeasureById = createAsyncThunk<ItemUnitMeasure, ItemUnitMeasure>(
  "itemUnitMeasure/update",
  async (ItemUnitMeasure, thunkAPI) => {
    try {
      await itemUnitMeasureService.updateItemUnitMeasure(ItemUnitMeasure);
      // Return updated ItemUnitMeasure so reducer gets correct payload type
      return ItemUnitMeasure;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete ItemUnitMeasure
export const deleteItemUnitMeasureById = createAsyncThunk<number, number>(
  "itemUnitMeasure/delete",
  async (id, thunkAPI) => {
    try {
      await itemUnitMeasureService.deleteItemUnitMeasure(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search ItemUnitMeasure
export const searchItemUnitMeasures = createAsyncThunk(
  "itemUnitMeasure/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await itemUnitMeasureService.searchItemUnitMeasures(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

