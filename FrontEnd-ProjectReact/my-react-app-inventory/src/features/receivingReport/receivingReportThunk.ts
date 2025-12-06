
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as receivingReportService from "../../services/receivingReportService";
import type { ReceivingReportMaster } from "../../types/receivingReportMaster";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all receivingReport
export const fetchreceivingReports = createAsyncThunk(
  "receivingReport/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await receivingReportService.getReceivingReports();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const fetchreceivingReportsPaged = createAsyncThunk<PagedResponse<ReceivingReportMaster[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "receivingReport/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await receivingReportService.getReceivingReportsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single receivingReport
export const fetchreceivingReportById = createAsyncThunk(
  "receivingReport/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await receivingReportService.getReceivingReportById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new receivingReport
export const addreceivingReport = createAsyncThunk(
  "receivingReport/add",
  async (receivingReport: ReceivingReportMaster, thunkAPI) => {
    try {
      return await receivingReportService.createReceivingReport(receivingReport);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updatereceivingReportById = createAsyncThunk<ReceivingReportMaster, ReceivingReportMaster>(
  "receivingReport/update",
  async (receivingReport, thunkAPI) => {
    try {
      await receivingReportService.updateReceivingReport(receivingReport);
      // Return updated receivingReport so reducer gets correct payload type
      return receivingReport;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete receivingReport
export const deletereceivingReportById = createAsyncThunk<number, number>(
  "receivingReport/delete",
  async (id, thunkAPI) => {
    try {
      await receivingReportService.deleteReceivingReport(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search receivingReport
export const searchreceivingReports = createAsyncThunk(
  "receivingReport/search",
  async (keyword: string, thunkAPI) => {
    try {
      return await receivingReportService.searchReceivingReports(keyword);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

