import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchExpenseCategorys,
  fetchExpenseCategoryById,
  searchExpenseCategorys,
  addExpenseCategory,
  updateExpenseCategoryById,
  deleteExpenseCategoryById,
} from "../features/expenseCategory/expenseCategoryThunk";
import type { ExpenseCategory } from "../types/expenseCategory";

interface UseExpenseCategoryReturn {
  expenseCategory: ExpenseCategory[];
  selectedExpenseCategory: ExpenseCategory | null;
  loading: boolean;
  error: string | null;
  addNewExpenseCategory: (ExpenseCategory: ExpenseCategory) => void;
  updateExpenseCategory: (ExpenseCategory: ExpenseCategory) => void;
  deleteExpenseCategory: (id: number) => void;
  reloadExpenseCategorys: () => void;
  getExpenseCategoryById: (id: number) => void;
  searchExpenseCategory: (name: string) => void;
}

export const useExpenseCategory = (): UseExpenseCategoryReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { expenseCategory, selectedExpenseCategory, loading, error } = useSelector(
    (state: RootState) => state.expenseCategory
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchExpenseCategorys());
  }, [dispatch]);

  const addNewExpenseCategory = useCallback(
    (expenseCategory: ExpenseCategory) => {
      dispatch(addExpenseCategory(expenseCategory));
    },
    [dispatch]
  );

  const updateExpenseCategory = useCallback(
    (expenseCategory: ExpenseCategory) => {
      dispatch(updateExpenseCategoryById(expenseCategory));
    },
    [dispatch]
  );

  const deleteExpenseCategory = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this ExpenseCategory?")) {
        dispatch(deleteExpenseCategoryById(id));
      }
    },
    [dispatch]
  );

  const reloadExpenseCategorys = useCallback(() => {
    dispatch(fetchExpenseCategorys());
  }, [dispatch]);

  const getExpenseCategoryById = useCallback(
    (id: number) => {
      dispatch(fetchExpenseCategoryById(id));
    },
    [dispatch]
  );

  const searchExpenseCategory = useCallback((keyword: string) => {
      dispatch(searchExpenseCategorys(keyword));
    },
    [dispatch]
  );

  return {
    expenseCategory,
    selectedExpenseCategory,
    loading,
    error,
    addNewExpenseCategory,
    updateExpenseCategory,
    deleteExpenseCategory,
    reloadExpenseCategorys,
    getExpenseCategoryById,
    searchExpenseCategory,
  };
};
