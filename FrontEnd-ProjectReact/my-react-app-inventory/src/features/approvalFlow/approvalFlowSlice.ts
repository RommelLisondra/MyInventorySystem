
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchApprovalFlows,
  fetchApprovalFlowById,
  addApprovalFlow,
  updateApprovalFlowById,
  deleteApprovalFlowById,
  searchApprovalFlows,
  fetchApprovalFlowsPaged,
} from "./approvalFlowThunk";
import type { ApprovalFlow } from "../../types/approvalFlow";
import type { PagedResponse } from "../../types/pagedResponse";

interface ApprovalFlowState {
  approvalFlows: ApprovalFlow[];
  selectedApprovalFlow: ApprovalFlow | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ApprovalFlowState = {
  approvalFlows: [],
  selectedApprovalFlow: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ApprovalFlowSlice = createSlice({
  name: "approvalFlow",
  initialState,
  reducers: {
    clearSelectedApprovalFlow: (state) => {
      state.selectedApprovalFlow = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchApprovalFlows.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchApprovalFlows.fulfilled, (state, action: PayloadAction<ApprovalFlow[]>) => {
        state.loading = false;
        state.approvalFlows = action.payload;
      })
      .addCase(fetchApprovalFlows.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchApprovalFlowsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchApprovalFlowsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ApprovalFlow[]>>) => {
          state.loading = false;
          state.approvalFlows = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchApprovalFlowsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchApprovalFlowById.fulfilled, (state, action: PayloadAction<ApprovalFlow>) => {
        state.selectedApprovalFlow = action.payload;
      })

      // Add
      .addCase(addApprovalFlow.fulfilled, (state: { approvalFlows: ApprovalFlow[]; }, action: PayloadAction<ApprovalFlow>) => {
        state.approvalFlows.push(action.payload);
      })

      // Update
      .addCase(updateApprovalFlowById.fulfilled, (state: { approvalFlows: any[]; }, action: PayloadAction<ApprovalFlow>) => {
        const index = state.approvalFlows.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.approvalFlows[index] = action.payload;
      })

      // Delete
      .addCase(deleteApprovalFlowById.fulfilled, (state: { approvalFlows: any[]; }, action: PayloadAction<number>) => {
        state.approvalFlows = state.approvalFlows.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchApprovalFlows.fulfilled, (state: { approvalFlows: ApprovalFlow[]; }, action: PayloadAction<ApprovalFlow[]>) => {
        state.approvalFlows = action.payload;
      });
  },
});

export const { clearSelectedApprovalFlow } = ApprovalFlowSlice.actions;
export default ApprovalFlowSlice.reducer;
