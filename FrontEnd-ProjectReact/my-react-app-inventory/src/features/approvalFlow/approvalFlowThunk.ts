
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as ApprovalFlowService from "../../services/approvalFlowService";
import type { ApprovalFlow } from "../../types/approvalFlow";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ApprovalFlows
export const fetchApprovalFlows = createAsyncThunk(
  "approvalFlow/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await ApprovalFlowService.getApprovalFlows();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all ApprovalFlows By page
export const fetchApprovalFlowsPaged = createAsyncThunk<PagedResponse<ApprovalFlow[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "approvalFlow/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await ApprovalFlowService.getApprovalFlowsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single ApprovalFlow
export const fetchApprovalFlowById = createAsyncThunk(
  "approvalFlow/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await ApprovalFlowService.getApprovalFlowById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new ApprovalFlow
export const addApprovalFlow = createAsyncThunk(
  "approvalFlow/add",
  async (ApprovalFlow: ApprovalFlow, thunkAPI) => {
    try {
      return await ApprovalFlowService.createApprovalFlow(ApprovalFlow);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateApprovalFlowById = createAsyncThunk<ApprovalFlow, ApprovalFlow>(
  "approvalFlow/update",
  async (ApprovalFlow, thunkAPI) => {
    try {
      await ApprovalFlowService.updateApprovalFlow(ApprovalFlow);
      // Return updated ApprovalFlow so reducer gets correct payload type
      return ApprovalFlow;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete ApprovalFlow
export const deleteApprovalFlowById = createAsyncThunk<number, number>(
  "approvalFlow/delete",
  async (id, thunkAPI) => {
    try {
      await ApprovalFlowService.deleteApprovalFlow(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search ApprovalFlows
export const searchApprovalFlows = createAsyncThunk(
  "approvalFlow/search",
  async (name: string, thunkAPI) => {
    try {
      return await ApprovalFlowService.searchApprovalFlows(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

