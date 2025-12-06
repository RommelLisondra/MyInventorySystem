
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as expenseService from "../../services/expenseService";
import type { Expense } from "../../types/expense";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Expenses
export const fetchExpenses = createAsyncThunk(
  "expense/fetchAll",
    async (_, thunkAPI) => {
        try {
            return await expenseService.getExpenses();
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch all Expenses By page
export const fetchExpensesPaged = createAsyncThunk<PagedResponse<Expense[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "expense/fetchPaged",
    async ({ pageNumber, pageSize }, thunkAPI) => {
        try {
            return await expenseService.getExpensesPaged(pageNumber, pageSize);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch single Expense
export const fetchExpenseById = createAsyncThunk(
  "expense/fetchById",
    async (id: number, thunkAPI) => {
        try {
            return await expenseService.getExpenseById(id);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Add new Expense
export const addExpense = createAsyncThunk(
  "expense/add",
    async (expense: Expense, thunkAPI) => {
        try {
            return await expenseService.createExpense(expense);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

export const updateExpenseById = createAsyncThunk<Expense, Expense>(
  "expense/update",
    async (expense, thunkAPI) => {
        try {
            await expenseService.updateExpense(expense);
        // Return updated Expense so reducer gets correct payload type
            return expense;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Delete Expense
export const deleteExpenseById = createAsyncThunk<number, number>(
  "expense/delete",
    async (id, thunkAPI) => {
        try {
            await expenseService.deleteExpense(id);
            return id;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Search Expenses
export const searchExpenses = createAsyncThunk(
  "expense/search",
    async (name: string, thunkAPI) => {
        try {
            return await expenseService.searchExpenses(name);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

