
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as userAcountService from "../../services/userAcountService";
import type { UserAccount } from "../../types/userAccount";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all UserAccount
export const fetchUserAccounts = createAsyncThunk(
  "userAccount/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await userAcountService.getUserAccounts();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchUserAccountsPaged = createAsyncThunk<PagedResponse<UserAccount[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "userAccount/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await userAcountService.getUserAccountsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single UserAccount
export const fetchUserAccountById = createAsyncThunk(
  "userAccount/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await userAcountService.getUserAccountById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new UserAccount
export const addUserAccount = createAsyncThunk(
  "userAccount/add",
  async (userAccount: UserAccount, thunkAPI) => {
    try {
      return await userAcountService.createUserAccount(userAccount);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateUserAccountById = createAsyncThunk<UserAccount, UserAccount>(
  "userAccount/update",
  async (userAccount, thunkAPI) => {
    try {
      await userAcountService.updateUserAccount(userAccount);
      // Return updated UserAccount so reducer gets correct payload type
      return userAccount;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete UserAccount
export const deleteUserAccountById = createAsyncThunk<number, number>(
  "userAccount/delete",
  async (id, thunkAPI) => {
    try {
      await userAcountService.deleteUserAccount(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search UserAccount
export const searchUserAccounts = createAsyncThunk(
  "userAccount/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await userAcountService.searchUserAccounts(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

