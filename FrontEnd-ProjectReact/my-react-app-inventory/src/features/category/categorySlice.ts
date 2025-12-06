
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchCategorys,
  fetchCategoryById,
  addCategory,
  updateCategoryById,
  deleteCategoryById,
  searchCategorys,
  fetchCategorysPaged,
} from "./categoryThunk";
import type { Category } from "../../types/category";
import type { PagedResponse } from "../../types/pagedResponse";

interface CategoryState {
  category: Category[];
  selectedCategory: Category | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: CategoryState = {
  category: [],
  selectedCategory: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const CategorySlice = createSlice({
  name: "Category",
  initialState,
  reducers: {
    clearSelectedCategory: (state) => {
      state.selectedCategory = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchCategorys.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategorys.fulfilled, (state, action: PayloadAction<Category[]>) => {
        state.loading = false;
        state.category = action.payload;
      })
      .addCase(fetchCategorys.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchCategorysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategorysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Category[]>>) => {
          state.loading = false;
          state.category = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchCategorysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchCategoryById.fulfilled, (state, action: PayloadAction<Category>) => {
        state.selectedCategory = action.payload;
      })

      // Add
      .addCase(addCategory.fulfilled, (state: { category: Category[]; }, action: PayloadAction<Category>) => {
        state.category.push(action.payload);
      })

      // Update
      .addCase(updateCategoryById.fulfilled, (state: { category: any[]; }, action: PayloadAction<Category>) => {
        const index = state.category.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.category[index] = action.payload;
      })

      // Delete
      .addCase(deleteCategoryById.fulfilled, (state: { category: any[]; }, action: PayloadAction<number>) => {
        state.category = state.category.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchCategorys.fulfilled, (state: { category: Category[]; }, action: PayloadAction<Category[]>) => {
        state.category = action.payload;
      });
  },
});

export const { clearSelectedCategory } = CategorySlice.actions;
export default CategorySlice.reducer;
