
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchpurchaseRequisitions,
  fetchpurchaseRequisitionById,
  addpurchaseRequisition,
  updatepurchaseRequisitionById,
  deletepurchaseRequisitionById,
  searchpurchaseRequisitions,
  fetchpurchaseRequisitionsPaged
} from "./purchaseRequisitionsThunk";
import type { PurchaseRequisitionMaster } from "../../types/purchaseRequisitionMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface purchaseRequisitionState {
  purchaseRequisition: PurchaseRequisitionMaster[];
  selectedpurchaseRequisition: PurchaseRequisitionMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: purchaseRequisitionState = {
  purchaseRequisition: [],
  selectedpurchaseRequisition: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const purchaseRequisitionSlice = createSlice({
  name: "purchaseRequisition",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedpurchaseRequisition = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchpurchaseRequisitions.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchpurchaseRequisitions.fulfilled, (state, action: PayloadAction<PurchaseRequisitionMaster[]>) => {
        state.loading = false;
        state.purchaseRequisition = action.payload;
      })
      .addCase(fetchpurchaseRequisitions.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchpurchaseRequisitionsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchpurchaseRequisitionsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<PurchaseRequisitionMaster[]>>) => {
        state.loading = false;
        state.purchaseRequisition = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchpurchaseRequisitionsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchpurchaseRequisitionById.fulfilled, (state, action: PayloadAction<PurchaseRequisitionMaster>) => {
        state.selectedpurchaseRequisition = action.payload;
      })

      // Add
      .addCase(addpurchaseRequisition.fulfilled, (state, action: PayloadAction<PurchaseRequisitionMaster>) => {
        state.purchaseRequisition.push(action.payload);
      })

      // Update
      .addCase(updatepurchaseRequisitionById.fulfilled, (state, action: PayloadAction<PurchaseRequisitionMaster>) => {
        const index = state.purchaseRequisition.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.purchaseRequisition[index] = action.payload;
      })

      // Delete
      .addCase(deletepurchaseRequisitionById.fulfilled, (state, action: PayloadAction<number>) => {
        state.purchaseRequisition = state.purchaseRequisition.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchpurchaseRequisitions.fulfilled, (state, action: PayloadAction<PurchaseRequisitionMaster[]>) => {
        state.purchaseRequisition = action.payload;
      });
  },
});

export const { clearSelectedItem } = purchaseRequisitionSlice.actions;
export default purchaseRequisitionSlice.reducer;
