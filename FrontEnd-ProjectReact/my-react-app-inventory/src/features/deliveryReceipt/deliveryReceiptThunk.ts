
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as deliveryReceiptService from "../../services/deliveryReceiptService";
import type { DeliveryReceiptMaster } from "../../types/deliveryReceiptMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all DeliveryReceiptMasters
export const fetchDeliveryReceipts = createAsyncThunk(
  "deliveryReceipt/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await deliveryReceiptService.getDeliveryReceipts();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all DeliveryReceiptMasters By page
export const fetchDeliveryReceiptsPaged = createAsyncThunk<PagedResponse<DeliveryReceiptMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "deliveryReceipt/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await deliveryReceiptService.getDeliveryReceiptsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single DeliveryReceiptMaster
export const fetchDeliveryReceiptById = createAsyncThunk(
  "deliveryReceipt/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await deliveryReceiptService.getDeliveryReceiptById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new DeliveryReceiptMaster
export const addDeliveryReceipt = createAsyncThunk(
  "deliveryReceipt/add",
  async (deliveryReceiptMaster: DeliveryReceiptMaster, thunkAPI) => {
    try {
      return await deliveryReceiptService.createDeliveryReceipt(deliveryReceiptMaster);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateDeliveryReceiptById = createAsyncThunk<DeliveryReceiptMaster, DeliveryReceiptMaster>(
  "deliveryReceipt/update",
  async (deliveryReceiptMaster, thunkAPI) => {
    try {
      await deliveryReceiptService.updateDeliveryReceipt(deliveryReceiptMaster);
      // Return updated DeliveryReceiptMaster so reducer gets correct payload type
      return deliveryReceiptMaster;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete DeliveryReceiptMaster
export const deleteDeliveryReceiptById = createAsyncThunk<number, number>(
  "deliveryReceipt/delete",
  async (id, thunkAPI) => {
    try {
      await deliveryReceiptService.deleteDeliveryReceipt(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search DeliveryReceiptMasters
export const searchDeliveryReceipts = createAsyncThunk(
  "deliveryReceipt/search",
  async (name: string, thunkAPI) => {
    try {
      return await deliveryReceiptService.searchDeliveryReceipts(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

