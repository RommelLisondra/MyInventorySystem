
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchSubCategorys,
  fetchSubCategoryById,
  addSubCategory,
  updateSubCategoryById,
  deleteSubCategoryById,
  searchSubCategorys,
  fetchSubCategorysPaged,
} from "./subCategoryThunk";
import type { SubCategory } from "../../types/subCategory";
import type { PagedResponse } from "../../types/pagedResponse";

interface SubCategoryState {
    subCategory: SubCategory[];
    selectedSubCategory: SubCategory | null;
    loading: boolean;
    error: string | null;
    pageNumber: number;
    totalPages: number;
    totalRecords: number;
    pageSize: number;
}

const initialState: SubCategoryState = {
    subCategory: [],
    selectedSubCategory: null,
    loading: false,
    error: null,
    pageNumber: 1,
    totalPages: 0,
    totalRecords: 0,
    pageSize: 20,
};

export const SubCategorySlice = createSlice({
    name: "SubCategory",
    initialState,
    reducers: {
        clearSelectedSubCategory: (state) => {
        state.selectedSubCategory = null;
        },
    },
    extraReducers: (builder) => {
        builder
        // Fetch all
        .addCase(fetchSubCategorys.pending, (state) => {
            state.loading = true;
            state.error = null;
        })
        .addCase(fetchSubCategorys.fulfilled, (state, action: PayloadAction<SubCategory[]>) => {
            state.loading = false;
            state.subCategory = action.payload;
        })
        .addCase(fetchSubCategorys.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })
        // Fetch all by page
        .addCase(fetchSubCategorysPaged.pending, (state) => {
            state.loading = true;
            state.error = null;
        })
        .addCase(fetchSubCategorysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<SubCategory[]>>) => {
            state.loading = false;
            state.subCategory = action.payload.data;
            state.pageNumber = action.payload.pageNumber;
            state.totalPages = action.payload.totalPages;
            state.totalRecords = action.payload.totalRecords;
            state.pageSize = action.payload.pageSize;
            }
        )
        .addCase(fetchSubCategorysPaged.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })

        // Fetch by ID
        .addCase(fetchSubCategoryById.fulfilled, (state, action: PayloadAction<SubCategory>) => {
            state.selectedSubCategory = action.payload;
        })

        // Add
        .addCase(addSubCategory.fulfilled, (state: { subCategory: SubCategory[]; }, action: PayloadAction<SubCategory>) => {
            state.subCategory.push(action.payload);
        })

        // Update
        .addCase(updateSubCategoryById.fulfilled, (state: { subCategory: any[]; }, action: PayloadAction<SubCategory>) => {
            const index = state.subCategory.findIndex((c) => c.id === action.payload.id);
            if (index >= 0) state.subCategory[index] = action.payload;
        })

        // Delete
        .addCase(deleteSubCategoryById.fulfilled, (state: { subCategory: any[]; }, action: PayloadAction<number>) => {
            state.subCategory = state.subCategory.filter((c) => c.id !== action.payload);
        })

        // Search
        .addCase(searchSubCategorys.fulfilled, (state: { subCategory: SubCategory[]; }, action: PayloadAction<SubCategory[]>) => {
            state.subCategory = action.payload;
        });
    },
});

export const { clearSelectedSubCategory } = SubCategorySlice.actions;
export default SubCategorySlice.reducer;
