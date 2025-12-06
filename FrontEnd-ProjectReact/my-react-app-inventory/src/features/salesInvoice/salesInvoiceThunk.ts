
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as salesInvoiceService from "../../services/salesInvoiceService";
import type { SalesInvoiceMaster } from "../../types/salesInvoiceMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all SalesInvoiceMaster
export const fetchSalesInvoices = createAsyncThunk(
  "salesInvoice/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await salesInvoiceService.getSalesInvoices();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchSalesInvoicesPaged = createAsyncThunk<PagedResponse<SalesInvoiceMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "salesInvoice/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await salesInvoiceService.getSalesInvoicesPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single SalesInvoiceMaster
export const fetchSalesInvoiceById = createAsyncThunk(
  "salesInvoice/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await salesInvoiceService.getSalesInvoiceById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new SalesInvoiceMaster
export const addSalesInvoice = createAsyncThunk(
  "salesInvoice/add",
  async (SalesInvoiceMaster: SalesInvoiceMaster, thunkAPI) => {
    try {
      return await salesInvoiceService.createSalesInvoice(SalesInvoiceMaster);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateSalesInvoiceById = createAsyncThunk<SalesInvoiceMaster, SalesInvoiceMaster>(
  "salesInvoice/update",
  async (SalesInvoiceMaster, thunkAPI) => {
    try {
      await salesInvoiceService.updateSalesInvoice(SalesInvoiceMaster);
      // Return updated SalesInvoiceMaster so reducer gets correct payload type
      return SalesInvoiceMaster;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete SalesInvoiceMaster
export const deleteSalesInvoiceById = createAsyncThunk<number, number>(
  "salesInvoice/delete",
  async (id, thunkAPI) => {
    try {
      await salesInvoiceService.deleteSalesInvoice(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search SalesInvoiceMaster
export const searchSalesInvoices = createAsyncThunk(
  "salesInvoice/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await salesInvoiceService.searchSalesInvoices(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

