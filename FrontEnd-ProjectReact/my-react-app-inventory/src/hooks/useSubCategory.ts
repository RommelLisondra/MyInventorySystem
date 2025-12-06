import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchSubCategorys,
  fetchSubCategoryById,
  searchSubCategorys,
  addSubCategory,
  updateSubCategoryById,
  deleteSubCategoryById,
} from "../features/subCategory/subCategoryThunk";
import type { SubCategory } from "../types/subCategory";

interface UseSubCategoryReturn {
    subCategory: SubCategory[];
    selectedSubCategory: SubCategory | null;
    loading: boolean;
    error: string | null;
    addNewSubCategory: (subCategory: SubCategory) => void;
    updateSubCategory: (subCategory: SubCategory) => void;
    deleteSubCategory: (id: number) => void;
    reloadSubCategorys: () => void;
    getSubCategoryById: (id: number) => void;
    searchSubCategory: (name: string) => void;
}

export const useSubCategory = (): UseSubCategoryReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { subCategory, selectedSubCategory, loading, error } = useSelector(
        (state: RootState) => state.subCategory
    );

    // Load on mount
    useEffect(() => {
        dispatch(fetchSubCategorys());
    }, [dispatch]);

    const addNewSubCategory = useCallback(
        (subCategory: SubCategory) => {
        dispatch(addSubCategory(subCategory));
        },
        [dispatch]
    );

    const updateSubCategory = useCallback(
        (subCategory: SubCategory) => {
        dispatch(updateSubCategoryById(subCategory));
        },
        [dispatch]
    );

    const deleteSubCategory = useCallback(
        (id: number) => {
        if (confirm("Are you sure you want to delete this SubCategory?")) {
            dispatch(deleteSubCategoryById(id));
        }
        },
        [dispatch]
    );

    const reloadSubCategorys = useCallback(() => {
        dispatch(fetchSubCategorys());
    }, [dispatch]);

    const getSubCategoryById = useCallback(
        (id: number) => {
        dispatch(fetchSubCategoryById(id));
        },
        [dispatch]
    );

    const searchSubCategory = useCallback((keyword: string) => {
        dispatch(searchSubCategorys(keyword));
        },
        [dispatch]
    );

    return {
        subCategory,
        selectedSubCategory,
        loading,
        error,
        addNewSubCategory,
        updateSubCategory,
        deleteSubCategory,
        reloadSubCategorys,
        getSubCategoryById,
        searchSubCategory,
    };
};
