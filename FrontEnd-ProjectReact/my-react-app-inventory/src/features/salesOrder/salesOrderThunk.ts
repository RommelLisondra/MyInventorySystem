
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as saleOrderService from "../../services/saleOrderService";
import type { SalesOrderMaster } from "../../types/salesOrderMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all SalesOrderMaster
export const fetchSalesOrders = createAsyncThunk(
  "salesOrder/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await saleOrderService.getSalesOrders();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchSalesOrdersPaged = createAsyncThunk<PagedResponse<SalesOrderMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "salesOrder/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await saleOrderService.getSalesOrdersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single SalesOrderMaster
export const fetchSalesOrderById = createAsyncThunk(
  "salesOrder/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await saleOrderService.getSalesOrderById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new SalesOrderMaster
export const addSalesOrder = createAsyncThunk(
  "salesOrder/add",
  async (salesOrder: SalesOrderMaster, thunkAPI) => {
    try {
      return await saleOrderService.createSalesOrder(salesOrder);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateSalesOrderById = createAsyncThunk<SalesOrderMaster, SalesOrderMaster>(
  "salesOrder/update",
  async (salesOrder, thunkAPI) => {
    try {
      await saleOrderService.updateSalesOrder(salesOrder);
      // Return updated SalesOrderMaster so reducer gets correct payload type
      return salesOrder;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete SalesOrderMaster
export const deleteSalesOrderById = createAsyncThunk<number, number>(
  "salesOrder/delete",
  async (id, thunkAPI) => {
    try {
      await saleOrderService.deleteSalesOrder(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search SalesOrderMaster
export const searchSalesOrders = createAsyncThunk(
  "salesOrder/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await saleOrderService.searchSalesOrders(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

