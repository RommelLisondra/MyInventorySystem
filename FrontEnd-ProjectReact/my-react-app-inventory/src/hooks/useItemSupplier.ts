import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemSuppliers,
  fetchItemSupplierById,
  searchItemSuppliers,
  addItemSupplier,
  updateItemSupplierById,
  deleteItemSupplierById,
} from "../features/itemSupplier/itemSupplierThunk";
import type { ItemSupplier } from "../types/itemSupplier";

interface UseItemSupplierReturn {
  itemSuppliers: ItemSupplier[];
  selectedItemSupplier: ItemSupplier | null;
  loading: boolean;
  error: string | null;
  addNewItemSupplier: (ItemSupplier: ItemSupplier) => void;
  updateItemSupplier: (ItemSupplier: ItemSupplier) => void;
  deleteItemSupplier: (id: number) => void;
  reloadItemSuppliers: () => void;
  getItemSupplierById: (id: number) => void;
  searchItemSupplier: (name: string) => void;
}

export const useItemSupplier = (): UseItemSupplierReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { itemSuppliers, selectedItemSupplier, loading, error } = useSelector(
    (state: RootState) => state.itemSupplier
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchItemSuppliers());
  }, [dispatch]);

  const addNewItemSupplier = useCallback(
    (itemSupplier: ItemSupplier) => {
      dispatch(addItemSupplier(itemSupplier));
    },
    [dispatch]
  );

  const updateItemSupplier = useCallback(
    (itemSupplier: ItemSupplier) => {
      dispatch(updateItemSupplierById(itemSupplier));
    },
    [dispatch]
  );

  const deleteItemSupplier = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this ItemSupplier?")) {
        dispatch(deleteItemSupplierById(id));
      }
    },
    [dispatch]
  );

  const reloadItemSuppliers = useCallback(() => {
    dispatch(fetchItemSuppliers());
  }, [dispatch]);

  const getItemSupplierById = useCallback(
    (id: number) => {
      dispatch(fetchItemSupplierById(id));
    },
    [dispatch]
  );

  const searchItemSupplier = useCallback((keyword: string) => {
      dispatch(searchItemSuppliers(keyword));
    },
    [dispatch]
  );

  return {
    itemSuppliers,
    selectedItemSupplier,
    loading,
    error,
    addNewItemSupplier,
    updateItemSupplier,
    deleteItemSupplier,
    reloadItemSuppliers,
    getItemSupplierById,
    searchItemSupplier,
  };
};
