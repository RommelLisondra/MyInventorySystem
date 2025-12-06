
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchDeliveryReceipts,
  fetchDeliveryReceiptById,
  addDeliveryReceipt,
  updateDeliveryReceiptById,
  deleteDeliveryReceiptById,
  searchDeliveryReceipts,
  fetchDeliveryReceiptsPaged,
} from "./deliveryReceiptThunk";
import type { DeliveryReceiptMaster } from "../../types/deliveryReceiptMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface DeliveryReceiptState {
  deliveryReceipt: DeliveryReceiptMaster[];
  selectedDeliveryReceipt: DeliveryReceiptMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: DeliveryReceiptState = {
  deliveryReceipt: [],
  selectedDeliveryReceipt: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const DeliveryReceiptSlice = createSlice({
  name: "DeliveryReceipt",
  initialState,
  reducers: {
    clearSelectedDeliveryReceipt: (state) => {
      state.selectedDeliveryReceipt = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchDeliveryReceipts.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchDeliveryReceipts.fulfilled, (state, action: PayloadAction<DeliveryReceiptMaster[]>) => {
        state.loading = false;
        state.deliveryReceipt = action.payload;
      })
      .addCase(fetchDeliveryReceipts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchDeliveryReceiptsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchDeliveryReceiptsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<DeliveryReceiptMaster[]>>) => {
          state.loading = false;
          state.deliveryReceipt = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchDeliveryReceiptsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchDeliveryReceiptById.fulfilled, (state, action: PayloadAction<DeliveryReceiptMaster>) => {
        state.selectedDeliveryReceipt = action.payload;
      })

      // Add
      .addCase(addDeliveryReceipt.fulfilled, (state: { deliveryReceipt: DeliveryReceiptMaster[]; }, action: PayloadAction<DeliveryReceiptMaster>) => {
        state.deliveryReceipt.push(action.payload);
      })

      // Update
      .addCase(updateDeliveryReceiptById.fulfilled, (state: { deliveryReceipt: any[]; }, action: PayloadAction<DeliveryReceiptMaster>) => {
        const index = state.deliveryReceipt.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.deliveryReceipt[index] = action.payload;
      })

      // Delete
      .addCase(deleteDeliveryReceiptById.fulfilled, (state: { deliveryReceipt: any[]; }, action: PayloadAction<number>) => {
        state.deliveryReceipt = state.deliveryReceipt.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchDeliveryReceipts.fulfilled, (state: { deliveryReceipt: DeliveryReceiptMaster[]; }, action: PayloadAction<DeliveryReceiptMaster[]>) => {
        state.deliveryReceipt = action.payload;
      });
  },
});

export const { clearSelectedDeliveryReceipt } = DeliveryReceiptSlice.actions;
export default DeliveryReceiptSlice.reducer;
