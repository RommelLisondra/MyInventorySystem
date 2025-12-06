
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchApprovalHistorys,
  fetchApprovalHistoryById,
  addApprovalHistory,
  updateApprovalHistoryById,
  deleteApprovalHistoryById,
  searchApprovalHistorys,
  fetchApprovalHistorysPaged,
} from "./approvalHistoryThunk";
import type { ApprovalHistory } from "../../types/approvalHistory";
import type { PagedResponse } from "../../types/pagedResponse";

interface ApprovalHistoryState {
  approvalHistory: ApprovalHistory[];
  selectedApprovalHistory: ApprovalHistory | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ApprovalHistoryState = {
  approvalHistory: [],
  selectedApprovalHistory: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ApprovalHistorySlice = createSlice({
  name: "approvalHistory",
  initialState,
  reducers: {
    clearSelectedApprovalHistory: (state) => {
      state.selectedApprovalHistory = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchApprovalHistorys.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchApprovalHistorys.fulfilled, (state, action: PayloadAction<ApprovalHistory[]>) => {
        state.loading = false;
        state.approvalHistory = action.payload;
      })
      .addCase(fetchApprovalHistorys.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchApprovalHistorysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchApprovalHistorysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ApprovalHistory[]>>) => {
          state.loading = false;
          state.approvalHistory = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchApprovalHistorysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchApprovalHistoryById.fulfilled, (state, action: PayloadAction<ApprovalHistory>) => {
        state.selectedApprovalHistory = action.payload;
      })

      // Add
      .addCase(addApprovalHistory.fulfilled, (state: { approvalHistory: ApprovalHistory[]; }, action: PayloadAction<ApprovalHistory>) => {
        state.approvalHistory.push(action.payload);
      })

      // Update
      .addCase(updateApprovalHistoryById.fulfilled, (state: { approvalHistory: any[]; }, action: PayloadAction<ApprovalHistory>) => {
        const index = state.approvalHistory.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.approvalHistory[index] = action.payload;
      })

      // Delete
      .addCase(deleteApprovalHistoryById.fulfilled, (state: { approvalHistory: any[]; }, action: PayloadAction<number>) => {
        state.approvalHistory = state.approvalHistory.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchApprovalHistorys.fulfilled, (state: { approvalHistory: ApprovalHistory[]; }, action: PayloadAction<ApprovalHistory[]>) => {
        state.approvalHistory = action.payload;
      });
  },
});

export const { clearSelectedApprovalHistory } = ApprovalHistorySlice.actions;
export default ApprovalHistorySlice.reducer;
