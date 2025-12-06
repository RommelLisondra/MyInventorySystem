
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as categoryService from "../../services/categoryService";
import type { Category } from "../../types/category";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Categorys
export const fetchCategorys = createAsyncThunk(
  "category/fetchAll",
    async (_, thunkAPI) => {
        try {
            return await categoryService.getCategory();
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch all Categorys By page
export const fetchCategorysPaged = createAsyncThunk<PagedResponse<Category[]>,
  { pageNumber: number; pageSize: number },
  { rejectValue: string } // rejection type
>("category/fetchPaged",
    async ({ pageNumber, pageSize }, thunkAPI) => {
        try 
        {
            return await categoryService.getCategoryPaged(pageNumber, pageSize);
        } 
        catch (error: any) 
        {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch single Category
export const fetchCategoryById = createAsyncThunk(
  "category/fetchById",
    async (id: number, thunkAPI) => {
        try {
            return await categoryService.getCategoryById(id);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Add new Category
export const addCategory = createAsyncThunk(
  "category/add",
    async (category: Category, thunkAPI) => {
        try {
            return await categoryService.createCategory(category);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

export const updateCategoryById = createAsyncThunk<Category, Category>(
  "category/update",
    async (category, thunkAPI) => {
        try {
            await categoryService.updateCategory(category);
        // Return updated Category so reducer gets correct payload type
            return category;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Delete Category
export const deleteCategoryById = createAsyncThunk<number, number>(
  "Category/delete",
    async (id, thunkAPI) => {
        try {
            await categoryService.deleteCategory(id);
            return id;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Search Categorys
export const searchCategorys = createAsyncThunk(
  "Category/search",
    async (name: string, thunkAPI) => {
        try {
            return await categoryService.searchCategory(name);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

