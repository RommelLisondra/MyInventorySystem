
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemBarcodes,
  fetchItemBarcodeById,
  addItemBarcode,
  updateItemBarcodeById,
  deleteItemBarcodeById,
  searchItemBarcodes,
  fetchItemBarcodesPaged,
} from "./itemBarcodeThunk";
import type { ItemBarcode } from "../../types/itemBarcode";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemBarcodeState {
  itemBarcode: ItemBarcode[];
  selectedItemBarcode: ItemBarcode | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemBarcodeState = {
  itemBarcode: [],
  selectedItemBarcode: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemBarcodeSlice = createSlice({
  name: "ItemBarcode",
  initialState,
  reducers: {
    clearSelectedItemBarcode: (state) => {
      state.selectedItemBarcode = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemBarcodes.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemBarcodes.fulfilled, (state, action: PayloadAction<ItemBarcode[]>) => {
        state.loading = false;
        state.itemBarcode = action.payload;
      })
      .addCase(fetchItemBarcodes.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchItemBarcodesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemBarcodesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemBarcode[]>>) => {
          state.loading = false;
          state.itemBarcode = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemBarcodesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemBarcodeById.fulfilled, (state, action: PayloadAction<ItemBarcode>) => {
        state.selectedItemBarcode = action.payload;
      })

      // Add
      .addCase(addItemBarcode.fulfilled, (state: { itemBarcode: ItemBarcode[]; }, action: PayloadAction<ItemBarcode>) => {
        state.itemBarcode.push(action.payload);
      })

      // Update
      .addCase(updateItemBarcodeById.fulfilled, (state: { itemBarcode: any[]; }, action: PayloadAction<ItemBarcode>) => {
        const index = state.itemBarcode.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemBarcode[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemBarcodeById.fulfilled, (state: { itemBarcode: any[]; }, action: PayloadAction<number>) => {
        state.itemBarcode = state.itemBarcode.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemBarcodes.fulfilled, (state: { itemBarcode: ItemBarcode[]; }, action: PayloadAction<ItemBarcode[]>) => {
        state.itemBarcode = action.payload;
      });
  },
});

export const { clearSelectedItemBarcode } = ItemBarcodeSlice.actions;
export default ItemBarcodeSlice.reducer;
