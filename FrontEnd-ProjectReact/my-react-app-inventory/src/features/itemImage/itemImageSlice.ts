
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemImages,
  fetchItemImageById,
  addItemImage,
  updateItemImageById,
  deleteItemImageById,
  searchItemImages,
  fetchItemImagesPaged
} from "./itemImageThunk";
import type { ItemImage } from "../../types/itemImage";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemImageState {
  itemImages: ItemImage[];
  selectedItemImage: ItemImage | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemImageState = {
  itemImages: [],
  selectedItemImage: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemImageSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedItemImage = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemImages.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemImages.fulfilled, (state, action: PayloadAction<ItemImage[]>) => {
        state.loading = false;
        state.itemImages = action.payload;
      })
      .addCase(fetchItemImages.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchItemImagesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemImagesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemImage[]>>) => {
        state.loading = false;
        state.itemImages = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemImagesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemImageById.fulfilled, (state, action: PayloadAction<ItemImage>) => {
        state.selectedItemImage = action.payload;
      })

      // Add
      .addCase(addItemImage.fulfilled, (state, action: PayloadAction<ItemImage>) => {
        state.itemImages.push(action.payload);
      })

      // Update
      .addCase(updateItemImageById.fulfilled, (state, action: PayloadAction<ItemImage>) => {
        const index = state.itemImages.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemImages[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemImageById.fulfilled, (state, action: PayloadAction<number>) => {
        state.itemImages = state.itemImages.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemImages.fulfilled, (state, action: PayloadAction<ItemImage[]>) => {
        state.itemImages = action.payload;
      });
  },
});

export const { clearSelectedItem } = ItemImageSlice.actions;
export default ItemImageSlice.reducer;
