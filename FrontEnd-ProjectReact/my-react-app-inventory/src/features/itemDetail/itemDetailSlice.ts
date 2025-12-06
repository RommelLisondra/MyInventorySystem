
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemDetails,
  fetchItemDetailById,
  addItemDetail,
  updateItemDetailById,
  deleteItemDetailById,
  searchItemDetails,
  fetchItemDetailsPaged
} from "./itemDetailThunk";
import type { ItemDetail } from "../../types/itemDetail";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemDetailState {
  itemDetails: ItemDetail[];
  selectedItemDetail: ItemDetail | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemDetailState = {
  itemDetails: [],
  selectedItemDetail: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemDetailSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedItemDetail = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemDetails.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemDetails.fulfilled, (state, action: PayloadAction<ItemDetail[]>) => {
        state.loading = false;
        state.itemDetails = action.payload;
      })
      .addCase(fetchItemDetails.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchItemDetailsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemDetailsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemDetail[]>>) => {
        state.loading = false;
        state.itemDetails = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemDetailsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemDetailById.fulfilled, (state, action: PayloadAction<ItemDetail>) => {
        state.selectedItemDetail = action.payload;
      })

      // Add
      .addCase(addItemDetail.fulfilled, (state, action: PayloadAction<ItemDetail>) => {
        state.itemDetails.push(action.payload);
      })

      // Update
      .addCase(updateItemDetailById.fulfilled, (state, action: PayloadAction<ItemDetail>) => {
        const index = state.itemDetails.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemDetails[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemDetailById.fulfilled, (state, action: PayloadAction<number>) => {
        state.itemDetails = state.itemDetails.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemDetails.fulfilled, (state, action: PayloadAction<ItemDetail[]>) => {
        state.itemDetails = action.payload;
      });
  },
});

export const { clearSelectedItem } = ItemDetailSlice.actions;
export default ItemDetailSlice.reducer;
