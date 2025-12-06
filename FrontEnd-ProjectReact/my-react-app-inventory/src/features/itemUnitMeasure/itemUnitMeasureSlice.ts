
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchItemUnitMeasures,
  fetchItemUnitMeasureById,
  addItemUnitMeasure,
  updateItemUnitMeasureById,
  deleteItemUnitMeasureById,
  searchItemUnitMeasures,
  fetchItemUnitMeasuresPaged
} from "./itemUnitMeasureThunk";
import type { ItemUnitMeasure } from "../../types/itemUnitMeasure";
import type { PagedResponse } from "../../types/pagedResponse";

interface ItemUnitMeasureState {
  itemUnitMeasures: ItemUnitMeasure[];
  selectedItemUnitMeasure: ItemUnitMeasure | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ItemUnitMeasureState = {
  itemUnitMeasures: [],
  selectedItemUnitMeasure: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ItemUnitMeasureSlice = createSlice({
  name: "Item",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedItemUnitMeasure = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchItemUnitMeasures.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemUnitMeasures.fulfilled, (state, action: PayloadAction<ItemUnitMeasure[]>) => {
        state.loading = false;
        state.itemUnitMeasures = action.payload;
      })
      .addCase(fetchItemUnitMeasures.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchItemUnitMeasuresPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItemUnitMeasuresPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ItemUnitMeasure[]>>) => {
        state.loading = false;
        state.itemUnitMeasures = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchItemUnitMeasuresPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchItemUnitMeasureById.fulfilled, (state, action: PayloadAction<ItemUnitMeasure>) => {
        state.selectedItemUnitMeasure = action.payload;
      })

      // Add
      .addCase(addItemUnitMeasure.fulfilled, (state, action: PayloadAction<ItemUnitMeasure>) => {
        state.itemUnitMeasures.push(action.payload);
      })

      // Update
      .addCase(updateItemUnitMeasureById.fulfilled, (state, action: PayloadAction<ItemUnitMeasure>) => {
        const index = state.itemUnitMeasures.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.itemUnitMeasures[index] = action.payload;
      })

      // Delete
      .addCase(deleteItemUnitMeasureById.fulfilled, (state, action: PayloadAction<number>) => {
        state.itemUnitMeasures = state.itemUnitMeasures.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchItemUnitMeasures.fulfilled, (state, action: PayloadAction<ItemUnitMeasure[]>) => {
        state.itemUnitMeasures = action.payload;
      });
  },
});

export const { clearSelectedItem } = ItemUnitMeasureSlice.actions;
export default ItemUnitMeasureSlice.reducer;
