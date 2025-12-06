
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchBrands,
  fetchBrandById,
  addBrand,
  updateBrandById,
  deleteBrandById,
  searchBrands,
  fetchBrandsPaged,
} from "./brandThunk";
import type { Brand } from "../../types/brand";
import type { PagedResponse } from "../../types/pagedResponse";

interface BrandState {
  brands: Brand[];
  selectedBrand: Brand | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: BrandState = {
  brands: [],
  selectedBrand: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const BrandSlice = createSlice({
  name: "brand",
  initialState,
  reducers: {
    clearSelectedBrand: (state) => {
      state.selectedBrand = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchBrands.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchBrands.fulfilled, (state, action: PayloadAction<Brand[]>) => {
        state.loading = false;
        state.brands = action.payload;
      })
      .addCase(fetchBrands.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchBrandsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchBrandsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Brand[]>>) => {
          state.loading = false;
          state.brands = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchBrandsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchBrandById.fulfilled, (state, action: PayloadAction<Brand>) => {
        state.selectedBrand = action.payload;
      })

      // Add
      .addCase(addBrand.fulfilled, (state: { brands: Brand[]; }, action: PayloadAction<Brand>) => {
        state.brands.push(action.payload);
      })

      // Update
      .addCase(updateBrandById.fulfilled, (state: { brands: any[]; }, action: PayloadAction<Brand>) => {
        const index = state.brands.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.brands[index] = action.payload;
      })

      // Delete
      .addCase(deleteBrandById.fulfilled, (state: { brands: any[]; }, action: PayloadAction<number>) => {
        state.brands = state.brands.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchBrands.fulfilled, (state: { brands: Brand[]; }, action: PayloadAction<Brand[]>) => {
        state.brands = action.payload;
      });
  },
});

export const { clearSelectedBrand } = BrandSlice.actions;
export default BrandSlice.reducer;
