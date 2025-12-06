
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItems,
  fetchItemById,
  addItem,
  updateItemById,
  deleteItemById,
  searchItems,
  fetchItemsPaged
} from "./itemThunk";
import type { Item } from "../../types/item";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemState {
  items: Item[];
  selectedItem: Item | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemState = {
  items: [],
  selectedItem: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedItem = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItems.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItems.fulfilled, (state, action: PayloadAction<Item[]>) => {
        state.loading = false;
        state.items = action.payload;
      })
      .addCase(fetchItems.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchItemsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Item[]>>) => {
        state.loading = false;
        state.items = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemById.fulfilled, (state, action: PayloadAction<Item>) => {
        state.selectedItem = action.payload;
      })

      // Add
      .addCase(addItem.fulfilled, (state, action: PayloadAction<Item>) => {
        state.items.push(action.payload);
      })

      // Update
      .addCase(updateItemById.fulfilled, (state, action: PayloadAction<Item>) => {
        const index = state.items.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.items[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemById.fulfilled, (state, action: PayloadAction<number>) => {
        state.items = state.items.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItems.fulfilled, (state, action: PayloadAction<Item[]>) => {
        state.items = action.payload;
      });
  },
});

export const { clearSelectedItem } = ItemSlice.actions;
export default ItemSlice.reducer;
