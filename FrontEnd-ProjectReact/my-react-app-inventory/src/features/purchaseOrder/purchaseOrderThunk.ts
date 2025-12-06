
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as purchaseOrderService from "../../services/purchaseOrderService";
import type { PurchaseOrderMaster } from "../../types/purchaseOrderMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all purchaseOrder
export const fetchpurchaseOrders = createAsyncThunk(
  "purchaseOrder/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await purchaseOrderService.getpurchaseOrders();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchpurchaseOrdersPaged = createAsyncThunk<PagedResponse<PurchaseOrderMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "purchaseOrder/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await purchaseOrderService.getpurchaseOrdersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single purchaseOrder
export const fetchpurchaseOrderById = createAsyncThunk(
  "purchaseOrder/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await purchaseOrderService.getpurchaseOrderById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new purchaseOrder
export const addpurchaseOrder = createAsyncThunk(
  "purchaseOrder/add",
  async (purchaseOrder: PurchaseOrderMaster, thunkAPI) => {
    try {
      return await purchaseOrderService.createpurchaseOrder(purchaseOrder);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updatepurchaseOrderById = createAsyncThunk<PurchaseOrderMaster, PurchaseOrderMaster>(
  "purchaseOrder/update",
  async (purchaseOrder, thunkAPI) => {
    try {
      await purchaseOrderService.updatepurchaseOrder(purchaseOrder);
      // Return updated purchaseOrder so reducer gets correct payload type
      return purchaseOrder;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete purchaseOrder
export const deletepurchaseOrderById = createAsyncThunk<number, number>(
  "purchaseOrder/delete",
  async (id, thunkAPI) => {
    try {
      await purchaseOrderService.deletepurchaseOrder(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search purchaseOrder
export const searchpurchaseOrders = createAsyncThunk(
  "purchaseOrder/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await purchaseOrderService.searchpurchaseOrders(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

