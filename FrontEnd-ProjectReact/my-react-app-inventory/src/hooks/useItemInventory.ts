import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemInventory,
  fetchItemInventoryById,
  searchItemInventory,
  addItemInventory,
  updateItemInventoryById,
  deleteItemInventoryById,
} from "../features/itemInventory/itemInventoryThunk";
import type { ItemInventory } from "../types/itemInventory";

interface UseItemInventoryReturn {
  itemInventory: ItemInventory[];
  selectedItemInventory: ItemInventory | null;
  loading: boolean;
  error: string | null;
  addNewItemInventory: (item: ItemInventory) => void;
  updateItemInventory: (item: ItemInventory) => void;
  deleteItemInventory: (id: number) => void;
  reloadItemInventorys: () => void;
  getItemInventoryById: (id: number) => void;
  searchItemInventories: (name: string) => void;
}

export const useItemInventory = (): UseItemInventoryReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { itemInventory, selectedItemInventory, loading, error } = useSelector(
    (state: RootState) => state.itemInventory
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchItemInventory());
  }, [dispatch]);

  const addNewItemInventory = useCallback(
    (ItemInventory: ItemInventory) => {
      dispatch(addItemInventory(ItemInventory));
    },
    [dispatch]
  );

  const updateItemInventory = useCallback(
    (ItemInventory: ItemInventory) => {
      dispatch(updateItemInventoryById(ItemInventory));
    },
    [dispatch]
  );

  const deleteItemInventory = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Item?")) {
        dispatch(deleteItemInventoryById(id));
      }
    },
    [dispatch]
  );

  const reloadItemInventorys = useCallback(() => {
    dispatch(fetchItemInventory());
  }, [dispatch]);

  const getItemInventoryById = useCallback(
    (id: number) => {
      dispatch(fetchItemInventoryById(id));
    },
    [dispatch]
  );

  const searchItemInventories = useCallback((keyword: string) => {
      dispatch(searchItemInventory(keyword));
    },
    [dispatch]
  );

  return {
    itemInventory,
    selectedItemInventory,
    loading,
    error,
    addNewItemInventory,
    updateItemInventory,
    deleteItemInventory,
    reloadItemInventorys,
    getItemInventoryById,
    searchItemInventories,
  };
};
