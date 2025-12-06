
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as subCategoryService from "../../services/subCategoryService";
import type { SubCategory } from "../../types/subCategory";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all SubCategorys
export const fetchSubCategorys = createAsyncThunk(
  "subCategory/fetchAll",
    async (_, thunkAPI) => {
        try {
            return await subCategoryService.getSubCategory();
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch all SubCategorys By page
export const fetchSubCategorysPaged = createAsyncThunk<PagedResponse<SubCategory[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "subCategory/fetchPaged",
    async ({ pageNumber, pageSize }, thunkAPI) => {
        try {
            return await subCategoryService.getSubCategoryPaged(pageNumber, pageSize);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch single SubCategory
export const fetchSubCategoryById = createAsyncThunk(
  "subCategory/fetchById",
    async (id: number, thunkAPI) => {
        try {
            return await subCategoryService.getSubCategoryById(id);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Add new SubCategory
export const addSubCategory = createAsyncThunk(
  "subCategory/add",
    async (subCategory: SubCategory, thunkAPI) => {
        try {
            return await subCategoryService.createSubCategory(subCategory);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

export const updateSubCategoryById = createAsyncThunk<SubCategory, SubCategory>(
  "subCategory/update",
    async (subCategory, thunkAPI) => {
        try {
            await subCategoryService.updateSubCategory(subCategory);
        // Return updated SubCategory so reducer gets correct payload type
            return subCategory;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Delete SubCategory
export const deleteSubCategoryById = createAsyncThunk<number, number>(
  "subCategory/delete",
    async (id, thunkAPI) => {
        try {
            await subCategoryService.deleteSubCategory(id);
            return id;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Search SubCategorys
export const searchSubCategorys = createAsyncThunk(
  "subCategory/search",
    async (name: string, thunkAPI) => {
        try {
            return await subCategoryService.searchSubCategory(name);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

