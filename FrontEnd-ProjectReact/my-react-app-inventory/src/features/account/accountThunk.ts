
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as accountService from "../../services/accountService";
import type { Account } from "../../types/account";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Accounts
export const fetchAccount = createAsyncThunk(
  "account/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await accountService.getAccount();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all Accounts By page
export const fetchAccountPaged = createAsyncThunk<PagedResponse<Account[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "account/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await accountService.getAccountsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Account
export const fetchAccountById = createAsyncThunk(
  "account/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await accountService.getAccountById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Account
export const addAccount = createAsyncThunk(
  "account/add",
  async (Account: Account, thunkAPI) => {
    try {
      return await accountService.createAccount(Account);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateAccountById = createAsyncThunk<Account, Account>(
  "account/update",
  async (Account, thunkAPI) => {
    try {
      await accountService.updateAccount(Account);
      // Return updated Account so reducer gets correct payload type
      return Account;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Account
export const deleteAccountById = createAsyncThunk<number, number>(
  "account/delete",
  async (id, thunkAPI) => {
    try {
      await accountService.deleteAccount(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Accounts
export const searchAccount = createAsyncThunk(
  "account/search",
  async (name: string, thunkAPI) => {
    try {
      return await accountService.searchAccount(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

