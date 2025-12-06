
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemImageService from "../../services/itemImageService";
import type { ItemImage } from "../../types/itemImage";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemImage
export const fetchItemImages = createAsyncThunk(
  "itemImage/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemImageService.getItemImages();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ItemImage By page
export const fetchItemImagesPaged = createAsyncThunk<PagedResponse<ItemImage[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemImage/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemImageService.getItemImagesPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemImage
export const fetchItemImageById = createAsyncThunk(
  "itemImage/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemImageService.getItemImageById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Item
export const addItemImage = createAsyncThunk(
  "itemImage/add",
  async (item: ItemImage, thunkAPI) => {
    try {
      return await itemImageService.createItemImage(item);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemImageById = createAsyncThunk<ItemImage, ItemImage>(
  "itemImage/update",
  async (item, thunkAPI) => {
    try {
      await itemImageService.updateItemImage(item);
      // Return updated Item so reducer gets correct payload type
      return item;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Item
export const deleteItemImageById = createAsyncThunk<number, number>(
  "itemImage/delete",
  async (id, thunkAPI) => {
    try {
      await itemImageService.deleteItemImage(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Item
export const searchItemImages = createAsyncThunk(
  "itemImage/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await itemImageService.searchItemImages(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

