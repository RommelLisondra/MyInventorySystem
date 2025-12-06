
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchExpenseCategorys,
  fetchExpenseCategoryById,
  addExpenseCategory,
  updateExpenseCategoryById,
  deleteExpenseCategoryById,
  searchExpenseCategorys,
  fetchExpenseCategorysPaged,
} from "./expenseCategoryThunk";
import type { ExpenseCategory } from "../../types/expenseCategory";
import type { PagedResponse } from "../../types/pagedResponse";

interface ExpenseCategoryState {
  expenseCategory: ExpenseCategory[];
  selectedExpenseCategory: ExpenseCategory | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ExpenseCategoryState = {
  expenseCategory: [],
  selectedExpenseCategory: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ExpenseCategorySlice = createSlice({
  name: "ExpenseCategory",
  initialState,
  reducers: {
    clearSelectedExpenseCategory: (state) => {
      state.selectedExpenseCategory = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchExpenseCategorys.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchExpenseCategorys.fulfilled, (state, action: PayloadAction<ExpenseCategory[]>) => {
        state.loading = false;
        state.expenseCategory = action.payload;
      })
      .addCase(fetchExpenseCategorys.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchExpenseCategorysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchExpenseCategorysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<ExpenseCategory[]>>) => {
          state.loading = false;
          state.expenseCategory = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchExpenseCategorysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchExpenseCategoryById.fulfilled, (state, action: PayloadAction<ExpenseCategory>) => {
        state.selectedExpenseCategory = action.payload;
      })

      // Add
      .addCase(addExpenseCategory.fulfilled, (state: { expenseCategory: ExpenseCategory[]; }, action: PayloadAction<ExpenseCategory>) => {
        state.expenseCategory.push(action.payload);
      })

      // Update
      .addCase(updateExpenseCategoryById.fulfilled, (state: { expenseCategory: any[]; }, action: PayloadAction<ExpenseCategory>) => {
        const index = state.expenseCategory.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.expenseCategory[index] = action.payload;
      })

      // Delete
      .addCase(deleteExpenseCategoryById.fulfilled, (state: { expenseCategory: any[]; }, action: PayloadAction<number>) => {
        state.expenseCategory = state.expenseCategory.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchExpenseCategorys.fulfilled, (state: { expenseCategory: ExpenseCategory[]; }, action: PayloadAction<ExpenseCategory[]>) => {
        state.expenseCategory = action.payload;
      });
  },
});

export const { clearSelectedExpenseCategory } = ExpenseCategorySlice.actions;
export default ExpenseCategorySlice.reducer;
