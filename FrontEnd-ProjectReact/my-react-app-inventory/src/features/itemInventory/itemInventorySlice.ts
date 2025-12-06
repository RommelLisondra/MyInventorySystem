
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemInventory,
  fetchItemInventoryById,
  addItemInventory,
  updateItemInventoryById,
  deleteItemInventoryById,
  searchItemInventory,
  fetchItemInventoryPaged
} from "./itemInventoryThunk";
import type { ItemInventory } from "../../types/itemInventory";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemInventorytate {
  itemInventory: ItemInventory[];
  selectedItemInventory: ItemInventory | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemInventorytate = {
  itemInventory: [],
  selectedItemInventory: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemInventorylice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedItemInventory = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemInventory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemInventory.fulfilled, (state, action: PayloadAction<ItemInventory[]>) => {
        state.loading = false;
        state.itemInventory = action.payload;
      })
      .addCase(fetchItemInventory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchItemInventoryPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemInventoryPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemInventory[]>>) => {
        state.loading = false;
        state.itemInventory = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemInventoryPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemInventoryById.fulfilled, (state, action: PayloadAction<ItemInventory>) => {
        state.selectedItemInventory = action.payload;
      })

      // Add
      .addCase(addItemInventory.fulfilled, (state, action: PayloadAction<ItemInventory>) => {
        state.itemInventory.push(action.payload);
      })

      // Update
      .addCase(updateItemInventoryById.fulfilled, (state, action: PayloadAction<ItemInventory>) => {
        const index = state.itemInventory.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemInventory[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemInventoryById.fulfilled, (state, action: PayloadAction<number>) => {
        state.itemInventory = state.itemInventory.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemInventory.fulfilled, (state, action: PayloadAction<ItemInventory[]>) => {
        state.itemInventory = action.payload;
      });
  },
});

export const { clearSelectedItem } = ItemInventorylice.actions;
export default ItemInventorylice.reducer;
