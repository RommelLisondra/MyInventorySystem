
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as supplierService from "../../services/supplierService";
import type { Supplier } from "../../types/supplier";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Supplier
export const fetchSuppliers = createAsyncThunk(
  "supplier/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await supplierService.getSuppliers();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchSuppliersPaged = createAsyncThunk<PagedResponse<Supplier[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "supplier/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await supplierService.getSuppliersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Supplier
export const fetchSupplierById = createAsyncThunk(
  "supplier/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await supplierService.getSupplierById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Supplier
export const addSupplier = createAsyncThunk(
  "supplier/add",
  async (Supplier: Supplier, thunkAPI) => {
    try {
      return await supplierService.createSupplier(Supplier);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateSupplierById = createAsyncThunk<Supplier, Supplier>(
  "supplier/update",
  async (Supplier, thunkAPI) => {
    try {
      await supplierService.updateSupplier(Supplier);
      // Return updated Supplier so reducer gets correct payload type
      return Supplier;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Supplier
export const deleteSupplierById = createAsyncThunk<number, number>(
  "supplier/delete",
  async (id, thunkAPI) => {
    try {
      await supplierService.deleteSupplier(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Supplier
export const searchSuppliers = createAsyncThunk(
  "supplier/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await supplierService.searchSuppliers(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

