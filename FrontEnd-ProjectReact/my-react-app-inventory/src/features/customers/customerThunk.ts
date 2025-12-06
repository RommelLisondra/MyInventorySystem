
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as customerService from "../../services/customerService";
import type { Customer } from "../../types/customer";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all customers
export const fetchCustomers = createAsyncThunk(
  "customer/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await customerService.getCustomers();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all customers By page
export const fetchCustomersPaged = createAsyncThunk<PagedResponse<Customer[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "customers/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await customerService.getCustomersPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single customer
export const fetchCustomerById = createAsyncThunk(
  "customer/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await customerService.getCustomerById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new customer
export const addCustomer = createAsyncThunk(
  "customer/add",
  async (customer: Customer, thunkAPI) => {
    try {
      return await customerService.createCustomer(customer);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateCustomerById = createAsyncThunk<Customer, Customer>(
  "customer/update",
  async (customer, thunkAPI) => {
    try {
      await customerService.updateCustomer(customer);
      // Return updated customer so reducer gets correct payload type
      return customer;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete customer
export const deleteCustomerById = createAsyncThunk<number, number>(
  "customer/delete",
  async (id, thunkAPI) => {
    try {
      await customerService.deleteCustomer(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search customers
export const searchCustomers = createAsyncThunk(
  "customer/search",
  async (name: string, thunkAPI) => {
    try {
      return await customerService.searchCustomers(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

