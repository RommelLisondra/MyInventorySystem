
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchsalesReturns,
  fetchsalesReturnById,
  addsalesReturn,
  updatesalesReturnById,
  deletesalesReturnById,
  searchsalesReturns,
  fetchsalesReturnsPaged
} from "./salesReturnThunk";
import type { SalesReturnMaster } from "../../types/salesReturnMaster";
import type { PagedResponse } from "../../types/pagedResponse";

interface salesReturnState {
  salesReturn: SalesReturnMaster[];
  selectedSalesReturnMaster: SalesReturnMaster | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: salesReturnState = {
  salesReturn: [],
  selectedSalesReturnMaster: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const salesReturnSlice = createSlice({
  name: "salesReturn",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedSalesReturnMaster = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchsalesReturns.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchsalesReturns.fulfilled, (state, action: PayloadAction<SalesReturnMaster[]>) => {
        state.loading = false;
        state.salesReturn = action.payload;
      })
      .addCase(fetchsalesReturns.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchsalesReturnsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchsalesReturnsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<SalesReturnMaster[]>>) => {
        state.loading = false;
        state.salesReturn = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchsalesReturnsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchsalesReturnById.fulfilled, (state, action: PayloadAction<SalesReturnMaster>) => {
        state.selectedSalesReturnMaster = action.payload;
      })

      // Add
      .addCase(addsalesReturn.fulfilled, (state, action: PayloadAction<SalesReturnMaster>) => {
        state.salesReturn.push(action.payload);
      })

      // Update
      .addCase(updatesalesReturnById.fulfilled, (state, action: PayloadAction<SalesReturnMaster>) => {
        const index = state.salesReturn.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.salesReturn[index] = action.payload;
      })

      // Delete
      .addCase(deletesalesReturnById.fulfilled, (state, action: PayloadAction<number>) => {
        state.salesReturn = state.salesReturn.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchsalesReturns.fulfilled, (state, action: PayloadAction<SalesReturnMaster[]>) => {
        state.salesReturn = action.payload;
      });
  },
});

export const { clearSelectedItem } = salesReturnSlice.actions;
export default salesReturnSlice.reducer;
