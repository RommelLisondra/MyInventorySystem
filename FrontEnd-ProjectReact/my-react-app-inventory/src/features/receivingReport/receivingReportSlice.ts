
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchreceivingReports,
  fetchreceivingReportById,
  addreceivingReport,
  updatereceivingReportById,
  deletereceivingReportById,
  searchreceivingReports,
  fetchreceivingReportsPaged
} from "./receivingReportThunk";
import type { ReceivingReportMaster } from "../../types/receivingReportMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface receivingReportState {
  receivingReport: ReceivingReportMaster[];
  selectedreceivingReport: ReceivingReportMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: receivingReportState = {
  receivingReport: [],
  selectedreceivingReport: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const receivingReportSlice = createSlice({
  name: "receivingReport",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedreceivingReport = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchreceivingReports.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchreceivingReports.fulfilled, (state, action: PayloadAction<ReceivingReportMaster[]>) => {
        state.loading = false;
        state.receivingReport = action.payload;
      })
      .addCase(fetchreceivingReports.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchreceivingReportsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchreceivingReportsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ReceivingReportMaster[]>>) => {
        state.loading = false;
        state.receivingReport = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchreceivingReportsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchreceivingReportById.fulfilled, (state, action: PayloadAction<ReceivingReportMaster>) => {
        state.selectedreceivingReport = action.payload;
      })

      // Add
      .addCase(addreceivingReport.fulfilled, (state, action: PayloadAction<ReceivingReportMaster>) => {
        state.receivingReport.push(action.payload);
      })

      // Update
      .addCase(updatereceivingReportById.fulfilled, (state, action: PayloadAction<ReceivingReportMaster>) => {
        const index = state.receivingReport.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.receivingReport[index] = action.payload;
      })

      // Delete
      .addCase(deletereceivingReportById.fulfilled, (state, action: PayloadAction<number>) => {
        state.receivingReport = state.receivingReport.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchreceivingReports.fulfilled, (state, action: PayloadAction<ReceivingReportMaster[]>) => {
        state.receivingReport = action.payload;
      });
  },
});

export const { clearSelectedItem } = receivingReportSlice.actions;
export default receivingReportSlice.reducer;
