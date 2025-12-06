import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemUnitMeasures,
  fetchItemUnitMeasureById,
  searchItemUnitMeasures,
  addItemUnitMeasure,
  updateItemUnitMeasureById,
  deleteItemUnitMeasureById,
} from "../features/itemUnitMeasure/itemUnitMeasureThunk";
import type { ItemUnitMeasure } from "../types/itemUnitMeasure";

interface UseItemUnitMeasureReturn {
  itemUnitMeasures: ItemUnitMeasure[];
  selectedItemUnitMeasure: ItemUnitMeasure | null;
  loading: boolean;
  error: string | null;
  addNewItemUnitMeasure: (ItemUnitMeasure: ItemUnitMeasure) => void;
  updateItemUnitMeasure: (ItemUnitMeasure: ItemUnitMeasure) => void;
  deleteItemUnitMeasure: (id: number) => void;
  reloadItemUnitMeasures: () => void;
  getItemUnitMeasureById: (id: number) => void;
  searchItemUnitMeasure: (name: string) => void;
}

export const useItemUnitMeasure = (): UseItemUnitMeasureReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { itemUnitMeasures, selectedItemUnitMeasure, loading, error } = useSelector(
    (state: RootState) => state.itemUnitMeasure
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchItemUnitMeasures());
  }, [dispatch]);

  const addNewItemUnitMeasure = useCallback(
    (ItemUnitMeasure: ItemUnitMeasure) => {
      dispatch(addItemUnitMeasure(ItemUnitMeasure));
    },
    [dispatch]
  );

  const updateItemUnitMeasure = useCallback(
    (ItemUnitMeasure: ItemUnitMeasure) => {
      dispatch(updateItemUnitMeasureById(ItemUnitMeasure));
    },
    [dispatch]
  );

  const deleteItemUnitMeasure = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this ItemUnitMeasure?")) {
        dispatch(deleteItemUnitMeasureById(id));
      }
    },
    [dispatch]
  );

  const reloadItemUnitMeasures = useCallback(() => {
    dispatch(fetchItemUnitMeasures());
  }, [dispatch]);

  const getItemUnitMeasureById = useCallback(
    (id: number) => {
      dispatch(fetchItemUnitMeasureById(id));
    },
    [dispatch]
  );

  const searchItemUnitMeasure = useCallback((keyword: string) => {
      dispatch(searchItemUnitMeasures(keyword));
    },
    [dispatch]
  );

  return {
    itemUnitMeasures,
    selectedItemUnitMeasure,
    loading,
    error,
    addNewItemUnitMeasure,
    updateItemUnitMeasure,
    deleteItemUnitMeasure,
    reloadItemUnitMeasures,
    getItemUnitMeasureById,
    searchItemUnitMeasure,
  };
};
