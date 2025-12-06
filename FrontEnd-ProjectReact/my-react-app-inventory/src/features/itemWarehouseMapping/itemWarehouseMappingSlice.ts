
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemWarehouseMappings,
  fetchItemWarehouseMappingById,
  addItemWarehouseMapping,
  updateItemWarehouseMappingById,
  deleteItemWarehouseMappingById,
  searchItemWarehouseMappings,
  fetchItemWarehouseMappingsPaged,
} from "./itemWarehouseMappingThunk";
import type { ItemWarehouseMapping } from "../../types/itemWarehouse";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemWarehouseMappingState {
  itemWarehouseMapping: ItemWarehouseMapping[];
  selectedItemWarehouseMapping: ItemWarehouseMapping | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemWarehouseMappingState = {
  itemWarehouseMapping: [],
  selectedItemWarehouseMapping: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemWarehouseMappingSlice = createSlice({
  name: "ItemWarehouseMapping",
  initialState,
  reducers: {
    clearSelectedItemWarehouseMapping: (state) => {
      state.selectedItemWarehouseMapping = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemWarehouseMappings.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemWarehouseMappings.fulfilled, (state, action: PayloadAction<ItemWarehouseMapping[]>) => {
        state.loading = false;
        state.itemWarehouseMapping = action.payload;
      })
      .addCase(fetchItemWarehouseMappings.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchItemWarehouseMappingsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemWarehouseMappingsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemWarehouseMapping[]>>) => {
          state.loading = false;
          state.itemWarehouseMapping = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemWarehouseMappingsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemWarehouseMappingById.fulfilled, (state, action: PayloadAction<ItemWarehouseMapping>) => {
        state.selectedItemWarehouseMapping = action.payload;
      })

      // Add
      .addCase(addItemWarehouseMapping.fulfilled, (state: { itemWarehouseMapping: ItemWarehouseMapping[]; }, action: PayloadAction<ItemWarehouseMapping>) => {
        state.itemWarehouseMapping.push(action.payload);
      })

      // Update
      .addCase(updateItemWarehouseMappingById.fulfilled, (state: { itemWarehouseMapping: any[]; }, action: PayloadAction<ItemWarehouseMapping>) => {
        const index = state.itemWarehouseMapping.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemWarehouseMapping[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemWarehouseMappingById.fulfilled, (state: { itemWarehouseMapping: any[]; }, action: PayloadAction<number>) => {
        state.itemWarehouseMapping = state.itemWarehouseMapping.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemWarehouseMappings.fulfilled, (state: { itemWarehouseMapping: ItemWarehouseMapping[]; }, action: PayloadAction<ItemWarehouseMapping[]>) => {
        state.itemWarehouseMapping = action.payload;
      });
  },
});

export const { clearSelectedItemWarehouseMapping } = ItemWarehouseMappingSlice.actions;
export default ItemWarehouseMappingSlice.reducer;
