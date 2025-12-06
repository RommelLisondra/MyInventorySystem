
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchpurchaseOrders,
  fetchpurchaseOrderById,
  addpurchaseOrder,
  updatepurchaseOrderById,
  deletepurchaseOrderById,
  searchpurchaseOrders,
  fetchpurchaseOrdersPaged
} from "./purchaseOrderThunk";
import type { PurchaseOrderMaster } from "../../types/purchaseOrderMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface PurchaseOrderState {
  purchaseOrder: PurchaseOrderMaster[];
  selectedPurchaseOrder: PurchaseOrderMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: PurchaseOrderState = {
  purchaseOrder: [],
  selectedPurchaseOrder: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const PurchaseOrderSlice = createSlice({
  name: "purchaseOrder",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedPurchaseOrder = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchpurchaseOrders.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchpurchaseOrders.fulfilled, (state, action: PayloadAction<PurchaseOrderMaster[]>) => {
        state.loading = false;
        state.purchaseOrder = action.payload;
      })
      .addCase(fetchpurchaseOrders.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchpurchaseOrdersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchpurchaseOrdersPaged.fulfilled,(state, action: PayloadAction<PagedResponse<PurchaseOrderMaster[]>>) => {
        state.loading = false;
        state.purchaseOrder = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchpurchaseOrdersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchpurchaseOrderById.fulfilled, (state, action: PayloadAction<PurchaseOrderMaster>) => {
        state.selectedPurchaseOrder = action.payload;
      })

      // Add
      .addCase(addpurchaseOrder.fulfilled, (state, action: PayloadAction<PurchaseOrderMaster>) => {
        state.purchaseOrder.push(action.payload);
      })

      // Update
      .addCase(updatepurchaseOrderById.fulfilled, (state, action: PayloadAction<PurchaseOrderMaster>) => {
        const index = state.purchaseOrder.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.purchaseOrder[index] = action.payload;
      })

      // Delete
      .addCase(deletepurchaseOrderById.fulfilled, (state, action: PayloadAction<number>) => {
        state.purchaseOrder = state.purchaseOrder.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchpurchaseOrders.fulfilled, (state, action: PayloadAction<PurchaseOrderMaster[]>) => {
        state.purchaseOrder = action.payload;
      });
  },
});

export const { clearSelectedItem } = PurchaseOrderSlice.actions;
export default PurchaseOrderSlice.reducer;
