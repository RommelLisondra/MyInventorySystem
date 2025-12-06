
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemPriceHistorys,
  fetchItemPriceHistoryById,
  addItemPriceHistory,
  updateItemPriceHistoryById,
  deleteItemPriceHistoryById,
  searchItemPriceHistorys,
  fetchItemPriceHistorysPaged,
} from "./itemPriceHistoryThunk";
import type { ItemPriceHistory } from "../../types/itemPriceHistory";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemPriceHistoryState {
  itemPriceHistory: ItemPriceHistory[];
  selectedItemPriceHistory: ItemPriceHistory | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemPriceHistoryState = {
  itemPriceHistory: [],
  selectedItemPriceHistory: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemPriceHistorySlice = createSlice({
  name: "ItemPriceHistory",
  initialState,
  reducers: {
    clearSelectedItemPriceHistory: (state) => {
      state.selectedItemPriceHistory = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemPriceHistorys.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemPriceHistorys.fulfilled, (state, action: PayloadAction<ItemPriceHistory[]>) => {
        state.loading = false;
        state.itemPriceHistory = action.payload;
      })
      .addCase(fetchItemPriceHistorys.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchItemPriceHistorysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemPriceHistorysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemPriceHistory[]>>) => {
          state.loading = false;
          state.itemPriceHistory = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemPriceHistorysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemPriceHistoryById.fulfilled, (state, action: PayloadAction<ItemPriceHistory>) => {
        state.selectedItemPriceHistory = action.payload;
      })

      // Add
      .addCase(addItemPriceHistory.fulfilled, (state: { itemPriceHistory: ItemPriceHistory[]; }, action: PayloadAction<ItemPriceHistory>) => {
        state.itemPriceHistory.push(action.payload);
      })

      // Update
      .addCase(updateItemPriceHistoryById.fulfilled, (state: { itemPriceHistory: any[]; }, action: PayloadAction<ItemPriceHistory>) => {
        const index = state.itemPriceHistory.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemPriceHistory[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemPriceHistoryById.fulfilled, (state: { itemPriceHistory: any[]; }, action: PayloadAction<number>) => {
        state.itemPriceHistory = state.itemPriceHistory.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemPriceHistorys.fulfilled, (state: { itemPriceHistory: ItemPriceHistory[]; }, action: PayloadAction<ItemPriceHistory[]>) => {
        state.itemPriceHistory = action.payload;
      });
  },
});

export const { clearSelectedItemPriceHistory } = ItemPriceHistorySlice.actions;
export default ItemPriceHistorySlice.reducer;
