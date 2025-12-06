
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as itemBarcodeService from "../../services/itemBarcodeService";
import type { ItemBarcode } from "../../types/itemBarcode";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ItemBarcodes
export const fetchItemBarcodes = createAsyncThunk(
  "itemBarcode/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await itemBarcodeService.getItemBarcodes();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ItemBarcodes By page
export const fetchItemBarcodesPaged = createAsyncThunk<PagedResponse<ItemBarcode[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "itemBarcode/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await itemBarcodeService.getItemBarcodePaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ItemBarcode
export const fetchItemBarcodeById = createAsyncThunk(
  "itemBarcode/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await itemBarcodeService.getItemBarcodeById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new ItemBarcode
export const addItemBarcode = createAsyncThunk(
  "itemBarcode/add",
  async (ItemBarcode: ItemBarcode, thunkAPI) => {
    try {
      return await itemBarcodeService.createItemBarcode(ItemBarcode);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateItemBarcodeById = createAsyncThunk<ItemBarcode, ItemBarcode>(
  "itemBarcode/update",
  async (ItemBarcode, thunkAPI) => {
    try {
      await itemBarcodeService.updateItemBarcode(ItemBarcode);
      // Return updated ItemBarcode so reducer gets correct payload type
      return ItemBarcode;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete ItemBarcode
export const deleteItemBarcodeById = createAsyncThunk<number, number>(
  "itemBarcode/delete",
  async (id, thunkAPI) => {
    try {
      await itemBarcodeService.deleteItemBarcode(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search ItemBarcodes
export const searchItemBarcodes = createAsyncThunk(
  "itemBarcode/search",
  async (name: string, thunkAPI) => {
    try {
      return await itemBarcodeService.searchItemBarcode(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

