
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchCostingHistorys,
  fetchCostingHistoryById,
  addCostingHistory,
  updateCostingHistoryById,
  deleteCostingHistoryById,
  searchCostingHistory,
  fetchCostingHistorysPaged,
} from "./costingHistoryThunk";
import type { CostingHistory } from "../../types/costingHistory";
import type { PagedResponse } from "../../types/pagedResponse";

interface CostingHistoryState {
  costingHistory: CostingHistory[];
  selectedCostingHistory: CostingHistory | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: CostingHistoryState = {
  costingHistory: [],
  selectedCostingHistory: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const CostingHistorySlice = createSlice({
  name: "CostingHistory",
  initialState,
  reducers: {
    clearSelectedCostingHistory: (state) => {
      state.selectedCostingHistory = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchCostingHistorys.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCostingHistorys.fulfilled, (state, action: PayloadAction<CostingHistory[]>) => {
        state.loading = false;
        state.costingHistory = action.payload;
      })
      .addCase(fetchCostingHistorys.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchCostingHistorysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCostingHistorysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<CostingHistory[]>>) => {
          state.loading = false;
          state.costingHistory = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchCostingHistorysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchCostingHistoryById.fulfilled, (state, action: PayloadAction<CostingHistory>) => {
        state.selectedCostingHistory = action.payload;
      })

      // Add
      .addCase(addCostingHistory.fulfilled, (state: { costingHistory: CostingHistory[]; }, action: PayloadAction<CostingHistory>) => {
        state.costingHistory.push(action.payload);
      })

      // Update
      .addCase(updateCostingHistoryById.fulfilled, (state: { costingHistory: any[]; }, action: PayloadAction<CostingHistory>) => {
        const index = state.costingHistory.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.costingHistory[index] = action.payload;
      })

      // Delete
      .addCase(deleteCostingHistoryById.fulfilled, (state: { costingHistory: any[]; }, action: PayloadAction<number>) => {
        state.costingHistory = state.costingHistory.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchCostingHistory.fulfilled, (state: { costingHistory: CostingHistory[]; }, action: PayloadAction<CostingHistory[]>) => {
        state.costingHistory = action.payload;
      });
  },
});

export const { clearSelectedCostingHistory } = CostingHistorySlice.actions;
export default CostingHistorySlice.reducer;
