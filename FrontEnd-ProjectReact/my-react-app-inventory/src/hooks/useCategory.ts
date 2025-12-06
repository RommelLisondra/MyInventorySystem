import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchCategorys,
  fetchCategoryById,
  searchCategorys,
  addCategory,
  updateCategoryById,
  deleteCategoryById,
} from "../features/category/categoryThunk";
import type { Category } from "../types/category";

interface UseCategoryReturn {
  category: Category[];
  selectedCategory: Category | null;
  loading: boolean;
  error: string | null;
  addNewCategory: (category: Category) => void;
  updateCategory: (category: Category) => void;
  deleteCategory: (id: number) => void;
  reloadCategorys: () => void;
  getCategoryById: (id: number) => void;
  searchCategory: (name: string) => void;
}

export const useCategory = (): UseCategoryReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { category, selectedCategory, loading, error } = useSelector(
    (state: RootState) => state.category
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchCategorys());
  }, [dispatch]);

  const addNewCategory = useCallback(
    (category: Category) => {
      dispatch(addCategory(category));
    },
    [dispatch]
  );

  const updateCategory = useCallback(
    (category: Category) => {
      dispatch(updateCategoryById(category));
    },
    [dispatch]
  );

  const deleteCategory = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Category?")) {
        dispatch(deleteCategoryById(id));
      }
    },
    [dispatch]
  );

  const reloadCategorys = useCallback(() => {
    dispatch(fetchCategorys());
  }, [dispatch]);

  const getCategoryById = useCallback(
    (id: number) => {
      dispatch(fetchCategoryById(id));
    },
    [dispatch]
  );

  const searchCategory = useCallback((keyword: string) => {
      dispatch(searchCategorys(keyword));
    },
    [dispatch]
  );

  return {
    category,
    selectedCategory,
    loading,
    error,
    addNewCategory,
    updateCategory,
    deleteCategory,
    reloadCategorys,
    getCategoryById,
    searchCategory,
  };
};
