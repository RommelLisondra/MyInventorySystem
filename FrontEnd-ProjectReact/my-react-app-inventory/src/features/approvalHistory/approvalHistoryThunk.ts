
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as approvalHistoryService from "../../services/approvalHistoryService";
import type { ApprovalHistory } from "../../types/approvalHistory";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ApprovalHistorys
export const fetchApprovalHistorys = createAsyncThunk(
  "approvalHistory/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await approvalHistoryService.getApprovalHistorys();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ApprovalHistorys By page
export const fetchApprovalHistorysPaged = createAsyncThunk<PagedResponse<ApprovalHistory[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "approvalHistory/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await approvalHistoryService.getApprovalHistorysPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ApprovalHistory
export const fetchApprovalHistoryById = createAsyncThunk(
  "approvalHistory/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await approvalHistoryService.getApprovalHistoryById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new ApprovalHistory
export const addApprovalHistory = createAsyncThunk(
  "approvalHistory/add",
  async (ApprovalHistory: ApprovalHistory, thunkAPI) => {
    try {
      return await approvalHistoryService.createApprovalHistory(ApprovalHistory);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateApprovalHistoryById = createAsyncThunk<ApprovalHistory, ApprovalHistory>(
  "approvalHistory/update",
  async (ApprovalHistory, thunkAPI) => {
    try {
      await approvalHistoryService.updateApprovalHistory(ApprovalHistory);
      // Return updated ApprovalHistory so reducer gets correct payload type
      return ApprovalHistory;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete ApprovalHistory
export const deleteApprovalHistoryById = createAsyncThunk<number, number>(
  "approvalHistory/delete",
  async (id, thunkAPI) => {
    try {
      await approvalHistoryService.deleteApprovalHistory(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search ApprovalHistorys
export const searchApprovalHistorys = createAsyncThunk(
  "approvalHistory/search",
  async (name: string, thunkAPI) => {
    try {
      return await approvalHistoryService.searchApprovalHistorys(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

