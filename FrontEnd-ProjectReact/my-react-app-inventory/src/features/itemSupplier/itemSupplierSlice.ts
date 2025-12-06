
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemSuppliers,
  fetchItemSupplierById,
  addItemSupplier,
  updateItemSupplierById,
  deleteItemSupplierById,
  searchItemSuppliers,
  fetchItemSuppliersPaged
} from "./itemSupplierThunk";
import type { ItemSupplier } from "../../types/itemSupplier";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemSupplierState {
  itemSuppliers: ItemSupplier[];
  selectedItemSupplier: ItemSupplier | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemSupplierState = {
  itemSuppliers: [],
  selectedItemSupplier: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemSupplierSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedItemSupplier = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemSuppliers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemSuppliers.fulfilled, (state, action: PayloadAction<ItemSupplier[]>) => {
        state.loading = false;
        state.itemSuppliers = action.payload;
      })
      .addCase(fetchItemSuppliers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchItemSuppliersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemSuppliersPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemSupplier[]>>) => {
        state.loading = false;
        state.itemSuppliers = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemSuppliersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemSupplierById.fulfilled, (state, action: PayloadAction<ItemSupplier>) => {
        state.selectedItemSupplier = action.payload;
      })

      // Add
      .addCase(addItemSupplier.fulfilled, (state, action: PayloadAction<ItemSupplier>) => {
        state.itemSuppliers.push(action.payload);
      })

      // Update
      .addCase(updateItemSupplierById.fulfilled, (state, action: PayloadAction<ItemSupplier>) => {
        const index = state.itemSuppliers.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemSuppliers[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemSupplierById.fulfilled, (state, action: PayloadAction<number>) => {
        state.itemSuppliers = state.itemSuppliers.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemSuppliers.fulfilled, (state, action: PayloadAction<ItemSupplier[]>) => {
        state.itemSuppliers = action.payload;
      });
  },
});

export const { clearSelectedItem } = ItemSupplierSlice.actions;
export default ItemSupplierSlice.reducer;
