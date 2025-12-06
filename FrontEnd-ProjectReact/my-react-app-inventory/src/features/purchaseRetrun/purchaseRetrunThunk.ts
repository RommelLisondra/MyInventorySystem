
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as purchaseReturnService from "../../services/purchaseReturnService";
import type { PurchaseReturnMaster } from "../../types/purchaseReturnMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all purchaseReturn
export const fetchpurchaseReturns = createAsyncThunk(
  "purchaseReturn/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await purchaseReturnService.getPurchaseReturns();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchpurchaseReturnsPaged = createAsyncThunk<PagedResponse<PurchaseReturnMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "purchaseReturn/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await purchaseReturnService.getPurchaseReturnsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single purchaseReturn
export const fetchpurchaseReturnById = createAsyncThunk(
  "purchaseReturn/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await purchaseReturnService.getPurchaseReturnById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new purchaseReturn
export const addpurchaseReturn = createAsyncThunk(
  "purchaseReturn/add",
  async (purchaseReturn: PurchaseReturnMaster, thunkAPI) => {
    try {
      return await purchaseReturnService.createPurchaseReturn(purchaseReturn);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updatepurchaseReturnById = createAsyncThunk<PurchaseReturnMaster, PurchaseReturnMaster>(
  "purchaseReturn/update",
  async (purchaseReturn, thunkAPI) => {
    try {
      await purchaseReturnService.updatePurchaseReturn(purchaseReturn);
      // Return updated purchaseReturn so reducer gets correct payload type
      return purchaseReturn;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete purchaseReturn
export const deletepurchaseReturnById = createAsyncThunk<number, number>(
  "purchaseReturn/delete",
  async (id, thunkAPI) => {
    try {
      await purchaseReturnService.deletePurchaseReturn(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search purchaseReturn
export const searchpurchaseReturns = createAsyncThunk(
  "purchaseReturn/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await purchaseReturnService.searchPurchaseReturns(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

