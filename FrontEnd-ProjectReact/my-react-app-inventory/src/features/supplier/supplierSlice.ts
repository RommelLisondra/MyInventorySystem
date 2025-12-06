
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchSuppliers,
  fetchSupplierById,
  addSupplier,
  updateSupplierById,
  deleteSupplierById,
  searchSuppliers,
  fetchSuppliersPaged
} from "./supplierThunk";
import type { Supplier } from "../../types/supplier";
import type { PagedResponse } from "../../types/pagedResponse";

interface SupplierState {
  suppliers: Supplier[];
  selectedSupplier: Supplier | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: SupplierState = {
  suppliers: [],
  selectedSupplier: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const SupplierSlice = createSlice({
  name: "Supplier",
  initialState,
  reducers: {
    clearSelectedSupplier: (state) => {
      state.selectedSupplier = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchSuppliers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSuppliers.fulfilled, (state, action: PayloadAction<Supplier[]>) => {
        state.loading = false;
        state.suppliers = action.payload;
      })
      .addCase(fetchSuppliers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch all by page
      .addCase(fetchSuppliersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
       })
      .addCase(
        fetchSuppliersPaged.fulfilled,
          (state, action: PayloadAction<PagedResponse<Supplier[]>>) => {
            state.loading = false;
            state.suppliers = action.payload.data;
            state.pageNumber = action.payload.pageNumber;
            state.totalPages = action.payload.totalPages;
            state.totalRecords = action.payload.totalRecords;
            state.pageSize = action.payload.pageSize;
      })
      .addCase(fetchSuppliersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchSupplierById.fulfilled, (state, action: PayloadAction<Supplier>) => {
        state.selectedSupplier = action.payload;
      })

      // Add
      .addCase(addSupplier.fulfilled, (state, action: PayloadAction<Supplier>) => {
        state.suppliers.push(action.payload);
      })

      // Update
      .addCase(updateSupplierById.fulfilled, (state, action: PayloadAction<Supplier>) => {
        const index = state.suppliers.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.suppliers[index] = action.payload;
      })

      // Delete
      .addCase(deleteSupplierById.fulfilled, (state, action: PayloadAction<number>) => {
        state.suppliers = state.suppliers.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchSuppliers.fulfilled, (state, action: PayloadAction<Supplier[]>) => {
        state.suppliers = action.payload;
      });
  },
});

export const { clearSelectedSupplier } = SupplierSlice.actions;
export default SupplierSlice.reducer;
