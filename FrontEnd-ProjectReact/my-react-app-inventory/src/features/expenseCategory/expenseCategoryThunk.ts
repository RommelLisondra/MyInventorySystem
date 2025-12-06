
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as expenseCategoryService from "../../services/expenseCategoryService";
import type { ExpenseCategory } from "../../types/expenseCategory";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all ExpenseCategorys
export const fetchExpenseCategorys = createAsyncThunk(
  "expenseCategory/fetchAll",
    async (_, thunkAPI) => {
        try {
            return await expenseCategoryService.getExpenseCategory();
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch all ExpenseCategorys By page
export const fetchExpenseCategorysPaged = createAsyncThunk<PagedResponse<ExpenseCategory[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "expenseCategory/fetchPaged",
    async ({ pageNumber, pageSize }, thunkAPI) => {
        try {
            return await expenseCategoryService.getExpenseCategoryPaged(pageNumber, pageSize);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch single ExpenseCategory
export const fetchExpenseCategoryById = createAsyncThunk(
  "expenseCategory/fetchById",
    async (id: number, thunkAPI) => {
        try {
            return await expenseCategoryService.getExpenseCategoryById(id);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Add new ExpenseCategory
export const addExpenseCategory = createAsyncThunk(
  "expenseCategory/add",
    async (expenseCategory: ExpenseCategory, thunkAPI) => {
        try {
            return await expenseCategoryService.createExpenseCategory(expenseCategory);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

export const updateExpenseCategoryById = createAsyncThunk<ExpenseCategory, ExpenseCategory>(
  "expenseCategory/update",
    async (expenseCategory, thunkAPI) => {
        try {
            await expenseCategoryService.updateExpenseCategory(expenseCategory);
        // Return updated ExpenseCategory so reducer gets correct payload type
            return expenseCategory;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Delete ExpenseCategory
export const deleteExpenseCategoryById = createAsyncThunk<number, number>(
  "expenseCategory/delete",
    async (id, thunkAPI) => {
        try {
            await expenseCategoryService.deleteExpenseCategory(id);
            return id;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Search ExpenseCategorys
export const searchExpenseCategorys = createAsyncThunk(
  "expenseCategory/search",
    async (name: string, thunkAPI) => {
        try {
            return await expenseCategoryService.searchExpenseCategory(name);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

