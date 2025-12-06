
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchInventoryAdjustments,
  fetchInventoryAdjustmentById,
  addInventoryAdjustment,
  updateInventoryAdjustmentById,
  deleteInventoryAdjustmentById,
  searchInventoryAdjustments,
  fetchInventoryAdjustmentsPaged,
} from "./inventoryAdjustmentThunk";
import type { InventoryAdjustment } from "../../types/inventoryAdjustment";
import type { PagedResponse } from "../../types/pagedResponse";

interface InventoryAdjustmentState {
  inventoryAdjustments: InventoryAdjustment[];
  selectedInventoryAdjustment: InventoryAdjustment | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: InventoryAdjustmentState = {
  inventoryAdjustments: [],
  selectedInventoryAdjustment: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const InventoryAdjustmentSlice = createSlice({
  name: "InventoryAdjustment",
  initialState,
  reducers: {
    clearSelectedInventoryAdjustment: (state) => {
      state.selectedInventoryAdjustment = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchInventoryAdjustments.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchInventoryAdjustments.fulfilled, (state, action: PayloadAction<InventoryAdjustment[]>) => {
        state.loading = false;
        state.inventoryAdjustments = action.payload;
      })
      .addCase(fetchInventoryAdjustments.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchInventoryAdjustmentsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchInventoryAdjustmentsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<InventoryAdjustment[]>>) => {
          state.loading = false;
          state.inventoryAdjustments = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchInventoryAdjustmentsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchInventoryAdjustmentById.fulfilled, (state, action: PayloadAction<InventoryAdjustment>) => {
        state.selectedInventoryAdjustment = action.payload;
      })

      // Add
      .addCase(addInventoryAdjustment.fulfilled, (state: { inventoryAdjustments: InventoryAdjustment[]; }, action: PayloadAction<InventoryAdjustment>) => {
        state.inventoryAdjustments.push(action.payload);
      })

      // Update
      .addCase(updateInventoryAdjustmentById.fulfilled, (state: { inventoryAdjustments: any[]; }, action: PayloadAction<InventoryAdjustment>) => {
        const index = state.inventoryAdjustments.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.inventoryAdjustments[index] = action.payload;
      })

      // Delete
      .addCase(deleteInventoryAdjustmentById.fulfilled, (state: { inventoryAdjustments: any[]; }, action: PayloadAction<number>) => {
        state.inventoryAdjustments = state.inventoryAdjustments.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchInventoryAdjustments.fulfilled, (state: { inventoryAdjustments: InventoryAdjustment[]; }, action: PayloadAction<InventoryAdjustment[]>) => {
        state.inventoryAdjustments = action.payload;
      });
  },
});

export const { clearSelectedInventoryAdjustment } = InventoryAdjustmentSlice.actions;
export default InventoryAdjustmentSlice.reducer;
