
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as officialReceiptService from "../../services/officialReceiptService";
import type { OfficialReceiptMaster } from "../../types/officialReceiptMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all officialReceipt
export const fetchofficialReceipts = createAsyncThunk(
  "officialReceipt/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await officialReceiptService.getOfficialReceipts();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchofficialReceiptsPaged = createAsyncThunk<PagedResponse<OfficialReceiptMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "officialReceipt/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await officialReceiptService.getOfficialReceiptsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single officialReceipt
export const fetchofficialReceiptById = createAsyncThunk(
  "officialReceipt/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await officialReceiptService.getOfficialReceiptById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new officialReceipt
export const addofficialReceipt = createAsyncThunk(
  "officialReceipt/add",
  async (officialReceipt: OfficialReceiptMaster, thunkAPI) => {
    try {
      return await officialReceiptService.createOfficialReceipt(officialReceipt);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateofficialReceiptById = createAsyncThunk<OfficialReceiptMaster, OfficialReceiptMaster>(
  "officialReceipt/update",
  async (officialReceipt, thunkAPI) => {
    try {
      await officialReceiptService.updateOfficialReceipt(officialReceipt);
      // Return updated officialReceipt so reducer gets correct payload type
      return officialReceipt;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete officialReceipt
export const deleteofficialReceiptById = createAsyncThunk<number, number>(
  "officialReceipt/delete",
  async (id, thunkAPI) => {
    try {
      await officialReceiptService.deleteOfficialReceipt(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search officialReceipt
export const searchofficialReceipts = createAsyncThunk(
  "officialReceipt/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await officialReceiptService.searchOfficialReceipts(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

