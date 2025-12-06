import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItems,
  fetchItemById,
  searchItems,
  addItem,
  updateItemById,
  deleteItemById,
} from "../features/item/itemThunk";
import type { Item } from "../types/item";

interface UseItemReturn {
  items: Item[];
  selectedItem: Item | null;
  loading: boolean;
  error: string | null;
  addNewItem: (Item: Item) => void;
  updateItem: (Item: Item) => void;
  deleteItem: (id: number) => void;
  reloadItems: () => void;
  getItemById: (id: number) => void;
  searchItem: (name: string) => void;
}

export const useItem = (): UseItemReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { items, selectedItem, loading, error } = useSelector(
    (state: RootState) => state.item
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchItems());
  }, [dispatch]);

  const addNewItem = useCallback(
    (item: Item) => {
      dispatch(addItem(item));
    },
    [dispatch]
  );

  const updateItem = useCallback(
    (item: Item) => {
      dispatch(updateItemById(item));
    },
    [dispatch]
  );

  const deleteItem = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Item?")) {
        dispatch(deleteItemById(id));
      }
    },
    [dispatch]
  );

  const reloadItems = useCallback(() => {
    dispatch(fetchItems());
  }, [dispatch]);

  const getItemById = useCallback(
    (id: number) => {
      dispatch(fetchItemById(id));
    },
    [dispatch]
  );

  const searchItem = useCallback((keyword: string) => {
      dispatch(searchItems(keyword));
    },
    [dispatch]
  );

  return {
    items,
    selectedItem,
    loading,
    error,
    addNewItem,
    updateItem,
    deleteItem,
    reloadItems,
    getItemById,
    searchItem,
  };
};
