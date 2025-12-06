
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchSalesOrders,
  fetchSalesOrderById,
  addSalesOrder,
  updateSalesOrderById,
  deleteSalesOrderById,
  searchSalesOrders,
  fetchSalesOrdersPaged
} from "./salesOrderThunk";
import type { SalesOrderMaster } from "../../types/salesOrderMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface SalesOrderState {
  salesOrder: SalesOrderMaster[];
  selectedSalesOrder: SalesOrderMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: SalesOrderState = {
  salesOrder: [],
  selectedSalesOrder: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const SalesOrderSlice = createSlice({
  name: "salesOrder",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedSalesOrder = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchSalesOrders.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSalesOrders.fulfilled, (state, action: PayloadAction<SalesOrderMaster[]>) => {
        state.loading = false;
        state.salesOrder = action.payload;
      })
      .addCase(fetchSalesOrders.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchSalesOrdersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSalesOrdersPaged.fulfilled,(state, action: PayloadAction<PagedResponse<SalesOrderMaster[]>>) => {
        state.loading = false;
        state.salesOrder = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchSalesOrdersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchSalesOrderById.fulfilled, (state, action: PayloadAction<SalesOrderMaster>) => {
        state.selectedSalesOrder = action.payload;
      })

      // Add
      .addCase(addSalesOrder.fulfilled, (state, action: PayloadAction<SalesOrderMaster>) => {
        state.salesOrder.push(action.payload);
      })

      // Update
      .addCase(updateSalesOrderById.fulfilled, (state, action: PayloadAction<SalesOrderMaster>) => {
        const index = state.salesOrder.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.salesOrder[index] = action.payload;
      })

      // Delete
      .addCase(deleteSalesOrderById.fulfilled, (state, action: PayloadAction<number>) => {
        state.salesOrder = state.salesOrder.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchSalesOrders.fulfilled, (state, action: PayloadAction<SalesOrderMaster[]>) => {
        state.salesOrder = action.payload;
      });
  },
});

export const { clearSelectedItem } = SalesOrderSlice.actions;
export default SalesOrderSlice.reducer;
