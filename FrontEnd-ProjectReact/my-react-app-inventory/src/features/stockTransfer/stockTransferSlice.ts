
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchstockTransfers,
  fetchstockTransferById,
  addstockTransfer,
  updatestockTransferById,
  deletestockTransferById,
  searchstockTransfers,
  fetchstockTransfersPaged
} from "./stockTransferThunk";
import type { StockTransfer } from "../../types/stockTransfer";
import type { PagedResponse } from "../../types/pagedResponse";

interface StockTransferState {
  stockTransfer: StockTransfer[];
  selectedStockTransfer: StockTransfer | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: StockTransferState = {
  stockTransfer: [],
  selectedStockTransfer: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const stockTransferSlice = createSlice({
  name: "stockTransfer",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedStockTransfer = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchstockTransfers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchstockTransfers.fulfilled, (state, action: PayloadAction<StockTransfer[]>) => {
        state.loading = false;
        state.stockTransfer = action.payload;
      })
      .addCase(fetchstockTransfers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchstockTransfersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchstockTransfersPaged.fulfilled,(state, action: PayloadAction<PagedResponse<StockTransfer[]>>) => {
        state.loading = false;
        state.stockTransfer = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchstockTransfersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchstockTransferById.fulfilled, (state, action: PayloadAction<StockTransfer>) => {
        state.selectedStockTransfer = action.payload;
      })

      // Add
      .addCase(addstockTransfer.fulfilled, (state, action: PayloadAction<StockTransfer>) => {
        state.stockTransfer.push(action.payload);
      })

      // Update
      .addCase(updatestockTransferById.fulfilled, (state, action: PayloadAction<StockTransfer>) => {
        const index = state.stockTransfer.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.stockTransfer[index] = action.payload;
      })

      // Delete
      .addCase(deletestockTransferById.fulfilled, (state, action: PayloadAction<number>) => {
        state.stockTransfer = state.stockTransfer.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchstockTransfers.fulfilled, (state, action: PayloadAction<StockTransfer[]>) => {
        state.stockTransfer = action.payload;
      });
  },
});

export const { clearSelectedItem } = stockTransferSlice.actions;
export default stockTransferSlice.reducer;
