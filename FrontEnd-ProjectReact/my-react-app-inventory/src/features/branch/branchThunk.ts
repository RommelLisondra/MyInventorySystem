
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as branchService from "../../services/branchService";
import type { Branch } from "../../types/branch";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Branchs
export const fetchBranchs = createAsyncThunk(
  "branch/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await branchService.getBranchs();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all Branchs By page
export const fetchBranchsPaged = createAsyncThunk<PagedResponse<Branch[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "branch/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await branchService.getBranchPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Branch
export const fetchBranchById = createAsyncThunk(
  "branch/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await branchService.getBranchById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Branch
export const addBranch = createAsyncThunk(
  "branch/add",
  async (Branch: Branch, thunkAPI) => {
    try {
      return await branchService.createBranch(Branch);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateBranchById = createAsyncThunk<Branch, Branch>(
  "branch/update",
  async (Branch, thunkAPI) => {
    try {
      await branchService.updateBranch(Branch);
      // Return updated Branch so reducer gets correct payload type
      return Branch;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Branch
export const deleteBranchById = createAsyncThunk<number, number>(
  "branch/delete",
  async (id, thunkAPI) => {
    try {
      await branchService.deleteBranch(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Branchs
export const searchBranch = createAsyncThunk(
  "branch/search",
  async (name: string, thunkAPI) => {
    try {
      return await branchService.searchBranch(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

