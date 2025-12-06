
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchExpenses,
  fetchExpenseById,
  addExpense,
  updateExpenseById,
  deleteExpenseById,
  searchExpenses,
  fetchExpensesPaged,
} from "./expenseThunk";
import type { Expense } from "../../types/expense";
import type { PagedResponse } from "../../types/pagedResponse";

interface ExpenseState {
  expenses: Expense[];
  selectedExpense: Expense | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ExpenseState = {
  expenses: [],
  selectedExpense: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ExpenseSlice = createSlice({
  name: "Expense",
  initialState,
  reducers: {
    clearSelectedExpense: (state) => {
      state.selectedExpense = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchExpenses.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchExpenses.fulfilled, (state, action: PayloadAction<Expense[]>) => {
        state.loading = false;
        state.expenses = action.payload;
      })
      .addCase(fetchExpenses.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchExpensesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchExpensesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Expense[]>>) => {
          state.loading = false;
          state.expenses = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchExpensesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchExpenseById.fulfilled, (state, action: PayloadAction<Expense>) => {
        state.selectedExpense = action.payload;
      })

      // Add
      .addCase(addExpense.fulfilled, (state: { expenses: Expense[]; }, action: PayloadAction<Expense>) => {
        state.expenses.push(action.payload);
      })

      // Update
      .addCase(updateExpenseById.fulfilled, (state: { expenses: any[]; }, action: PayloadAction<Expense>) => {
        const index = state.expenses.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.expenses[index] = action.payload;
      })

      // Delete
      .addCase(deleteExpenseById.fulfilled, (state: { expenses: any[]; }, action: PayloadAction<number>) => {
        state.expenses = state.expenses.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchExpenses.fulfilled, (state: { expenses: Expense[]; }, action: PayloadAction<Expense[]>) => {
        state.expenses = action.payload;
      });
  },
});

export const { clearSelectedExpense } = ExpenseSlice.actions;
export default ExpenseSlice.reducer;
