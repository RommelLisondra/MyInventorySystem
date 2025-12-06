
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchSalesInvoices,
  fetchSalesInvoiceById,
  addSalesInvoice,
  updateSalesInvoiceById,
  deleteSalesInvoiceById,
  searchSalesInvoices,
  fetchSalesInvoicesPaged
} from "./salesInvoiceThunk";
import type { SalesInvoiceMaster } from "../../types/salesInvoiceMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface SalesInvoiceMasterState {
  salesInvoice: SalesInvoiceMaster[];
  selectedSalesInvoiceMaster: SalesInvoiceMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: SalesInvoiceMasterState = {
  salesInvoice: [],
  selectedSalesInvoiceMaster: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const SalesInvoiceSlice = createSlice({
  name: "salesInvoice",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedSalesInvoiceMaster = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchSalesInvoices.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSalesInvoices.fulfilled, (state, action: PayloadAction<SalesInvoiceMaster[]>) => {
        state.loading = false;
        state.salesInvoice = action.payload;
      })
      .addCase(fetchSalesInvoices.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchSalesInvoicesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSalesInvoicesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<SalesInvoiceMaster[]>>) => {
        state.loading = false;
        state.salesInvoice = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchSalesInvoicesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchSalesInvoiceById.fulfilled, (state, action: PayloadAction<SalesInvoiceMaster>) => {
        state.selectedSalesInvoiceMaster = action.payload;
      })

      // Add
      .addCase(addSalesInvoice.fulfilled, (state, action: PayloadAction<SalesInvoiceMaster>) => {
        state.salesInvoice.push(action.payload);
      })

      // Update
      .addCase(updateSalesInvoiceById.fulfilled, (state, action: PayloadAction<SalesInvoiceMaster>) => {
        const index = state.salesInvoice.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.salesInvoice[index] = action.payload;
      })

      // Delete
      .addCase(deleteSalesInvoiceById.fulfilled, (state, action: PayloadAction<number>) => {
        state.salesInvoice = state.salesInvoice.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchSalesInvoices.fulfilled, (state, action: PayloadAction<SalesInvoiceMaster[]>) => {
        state.salesInvoice = action.payload;
      });
  },
});

export const { clearSelectedItem } = SalesInvoiceSlice.actions;
export default SalesInvoiceSlice.reducer;
