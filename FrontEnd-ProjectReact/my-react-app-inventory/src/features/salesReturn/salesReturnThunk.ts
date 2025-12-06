
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as salesReturnService from "../../services/salesReturnService";
import type { SalesReturnMaster } from "../../types/salesReturnMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all SalesReturnMaster
export const fetchsalesReturns = createAsyncThunk(
  "salesReturn/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await salesReturnService.getSalesReturns();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchsalesReturnsPaged = createAsyncThunk<PagedResponse<SalesReturnMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "salesReturn/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await salesReturnService.getSalesReturnsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single SalesReturnMaster
export const fetchsalesReturnById = createAsyncThunk(
  "salesReturn/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await salesReturnService.getSalesReturnById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new SalesReturnMaster
export const addsalesReturn = createAsyncThunk(
  "salesReturn/add",
  async (salesReturn: SalesReturnMaster, thunkAPI) => {
    try {
      return await salesReturnService.createSalesReturn(salesReturn);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updatesalesReturnById = createAsyncThunk<SalesReturnMaster, SalesReturnMaster>(
  "salesReturn/update",
  async (salesReturn, thunkAPI) => {
    try {
      await salesReturnService.updateSalesReturn(salesReturn);
      // Return updated SalesReturnMaster so reducer gets correct payload type
      return salesReturn;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete SalesReturnMaster
export const deletesalesReturnById = createAsyncThunk<number, number>(
  "salesReturn/delete",
  async (id, thunkAPI) => {
    try {
      await salesReturnService.deleteSalesReturn(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search SalesReturnMaster
export const searchsalesReturns = createAsyncThunk(
  "salesReturn/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await salesReturnService.searchSalesReturns(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

