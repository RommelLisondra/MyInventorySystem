
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchofficialReceipts,
  fetchofficialReceiptById,
  addofficialReceipt,
  updateofficialReceiptById,
  deleteofficialReceiptById,
  searchofficialReceipts,
  fetchofficialReceiptsPaged
} from "./officialReceiptThunk";
import type { OfficialReceiptMaster } from "../../types/officialReceiptMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface OfficialReceiptMasterState {
  OfficialReceipt: OfficialReceiptMaster[];
  selectedOfficialReceipt: OfficialReceiptMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: OfficialReceiptMasterState = {
  OfficialReceipt: [],
  selectedOfficialReceipt: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const OfficialReceiptSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedOfficialReceipt = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchofficialReceipts.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchofficialReceipts.fulfilled, (state, action: PayloadAction<OfficialReceiptMaster[]>) => {
        state.loading = false;
        state.OfficialReceipt = action.payload;
      })
      .addCase(fetchofficialReceipts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchofficialReceiptsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchofficialReceiptsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<OfficialReceiptMaster[]>>) => {
        state.loading = false;
        state.OfficialReceipt = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchofficialReceiptsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchofficialReceiptById.fulfilled, (state, action: PayloadAction<OfficialReceiptMaster>) => {
        state.selectedOfficialReceipt = action.payload;
      })

      // Add
      .addCase(addofficialReceipt.fulfilled, (state, action: PayloadAction<OfficialReceiptMaster>) => {
        state.OfficialReceipt.push(action.payload);
      })

      // Update
      .addCase(updateofficialReceiptById.fulfilled, (state, action: PayloadAction<OfficialReceiptMaster>) => {
        const index = state.OfficialReceipt.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.OfficialReceipt[index] = action.payload;
      })

      // Delete
      .addCase(deleteofficialReceiptById.fulfilled, (state, action: PayloadAction<number>) => {
        state.OfficialReceipt = state.OfficialReceipt.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchofficialReceipts.fulfilled, (state, action: PayloadAction<OfficialReceiptMaster[]>) => {
        state.OfficialReceipt = action.payload;
      });
  },
});

export const { clearSelectedItem } = OfficialReceiptSlice.actions;
export default OfficialReceiptSlice.reducer;
