
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchWarehouses,
  fetchWarehouseById,
  addWarehouse,
  updateWarehouseById,
  deleteWarehouseById,
  searchWarehouses,
  fetchWarehousesPaged
} from "./wareHouseThunks";
import type { Warehouse } from "../../types/warehouse";
import type { PagedResponse } from "../../types/pagedResponse";

interface WarehouseState {
  warehouses: Warehouse[];
  selectedWarehouse: Warehouse | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: WarehouseState = {
  warehouses: [],
  selectedWarehouse: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const WarehouseSlice = createSlice({
  name: "Warehouse",
  initialState,
  reducers: {
    clearSelectedWarehouse: (state) => {
      state.selectedWarehouse = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchWarehouses.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchWarehouses.fulfilled, (state, action: PayloadAction<Warehouse[]>) => {
        state.loading = false;
        state.warehouses = action.payload;
      })
      .addCase(fetchWarehouses.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch all by page
      .addCase(fetchWarehousesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
       })
      .addCase(
        fetchWarehousesPaged.fulfilled,
          (state, action: PayloadAction<PagedResponse<Warehouse[]>>) => {
            state.loading = false;
            state.warehouses = action.payload.data;
            state.pageNumber = action.payload.pageNumber;
            state.totalPages = action.payload.totalPages;
            state.totalRecords = action.payload.totalRecords;
            state.pageSize = action.payload.pageSize;
      })
      .addCase(fetchWarehousesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchWarehouseById.fulfilled, (state, action: PayloadAction<Warehouse>) => {
        state.selectedWarehouse = action.payload;
      })

      // Add
      .addCase(addWarehouse.fulfilled, (state, action: PayloadAction<Warehouse>) => {
        state.warehouses.push(action.payload);
      })

      // Update
      .addCase(updateWarehouseById.fulfilled, (state, action: PayloadAction<Warehouse>) => {
        const index = state.warehouses.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.warehouses[index] = action.payload;
      })

      // Delete
      .addCase(deleteWarehouseById.fulfilled, (state, action: PayloadAction<number>) => {
        state.warehouses = state.warehouses.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchWarehouses.fulfilled, (state, action: PayloadAction<Warehouse[]>) => {
        state.warehouses = action.payload;
      });
  },
});

export const { clearSelectedWarehouse } = WarehouseSlice.actions;
export default WarehouseSlice.reducer;
