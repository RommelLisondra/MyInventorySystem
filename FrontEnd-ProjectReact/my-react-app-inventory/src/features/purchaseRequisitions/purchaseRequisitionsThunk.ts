
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as purchaseRequisitionService from "../../services/purchaseRequisitionService";
import type { PurchaseRequisitionMaster } from "../../types/purchaseRequisitionMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all purchaseRequisition
export const fetchpurchaseRequisitions = createAsyncThunk(
  "purchaseRequisition/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await purchaseRequisitionService.getPurchaseRequisitions();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchpurchaseRequisitionsPaged = createAsyncThunk<PagedResponse<PurchaseRequisitionMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "purchaseRequisition/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await purchaseRequisitionService.getPurchaseRequisitionsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single purchaseRequisition
export const fetchpurchaseRequisitionById = createAsyncThunk(
  "purchaseRequisition/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await purchaseRequisitionService.getPurchaseRequisitionById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new purchaseRequisition
export const addpurchaseRequisition = createAsyncThunk(
  "purchaseRequisition/add",
  async (purchaseRequisition: PurchaseRequisitionMaster, thunkAPI) => {
    try {
      return await purchaseRequisitionService.createPurchaseRequisition(purchaseRequisition);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updatepurchaseRequisitionById = createAsyncThunk<PurchaseRequisitionMaster, PurchaseRequisitionMaster>(
  "purchaseRequisition/update",
  async (purchaseRequisition, thunkAPI) => {
    try {
      await purchaseRequisitionService.updatePurchaseRequisition(purchaseRequisition);
      // Return updated purchaseRequisition so reducer gets correct payload type
      return purchaseRequisition;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete purchaseRequisition
export const deletepurchaseRequisitionById = createAsyncThunk<number, number>(
  "purchaseRequisition/delete",
  async (id, thunkAPI) => {
    try {
      await purchaseRequisitionService.deletePurchaseRequisition(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search purchaseRequisition
export const searchpurchaseRequisitions = createAsyncThunk(
  "purchaseRequisition/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await purchaseRequisitionService.searchPurchaseRequisitions(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

