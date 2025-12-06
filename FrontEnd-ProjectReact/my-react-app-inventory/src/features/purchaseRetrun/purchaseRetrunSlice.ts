
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchpurchaseReturns,
  fetchpurchaseReturnById,
  addpurchaseReturn,
  updatepurchaseReturnById,
  deletepurchaseReturnById,
  searchpurchaseReturns,
  fetchpurchaseReturnsPaged
} from "./purchaseRetrunThunk";
import type { PurchaseReturnMaster } from "../../types/purchaseReturnMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface purchaseReturnState {
  purchaseReturn: PurchaseReturnMaster[];
  selectedpurchaseReturn: PurchaseReturnMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: purchaseReturnState = {
  purchaseReturn: [],
  selectedpurchaseReturn: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const purchaseReturnSlice = createSlice({
  name: "purchaseReturn",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedpurchaseReturn = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchpurchaseReturns.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchpurchaseReturns.fulfilled, (state, action: PayloadAction<PurchaseReturnMaster[]>) => {
        state.loading = false;
        state.purchaseReturn = action.payload;
      })
      .addCase(fetchpurchaseReturns.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchpurchaseReturnsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchpurchaseReturnsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<PurchaseReturnMaster[]>>) => {
        state.loading = false;
        state.purchaseReturn = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchpurchaseReturnsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchpurchaseReturnById.fulfilled, (state, action: PayloadAction<PurchaseReturnMaster>) => {
        state.selectedpurchaseReturn = action.payload;
      })

      // Add
      .addCase(addpurchaseReturn.fulfilled, (state, action: PayloadAction<PurchaseReturnMaster>) => {
        state.purchaseReturn.push(action.payload);
      })

      // Update
      .addCase(updatepurchaseReturnById.fulfilled, (state, action: PayloadAction<PurchaseReturnMaster>) => {
        const index = state.purchaseReturn.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.purchaseReturn[index] = action.payload;
      })

      // Delete
      .addCase(deletepurchaseReturnById.fulfilled, (state, action: PayloadAction<number>) => {
        state.purchaseReturn = state.purchaseReturn.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchpurchaseReturns.fulfilled, (state, action: PayloadAction<PurchaseReturnMaster[]>) => {
        state.purchaseReturn = action.payload;
      });
  },
});

export const { clearSelectedItem } = purchaseReturnSlice.actions;
export default purchaseReturnSlice.reducer;
