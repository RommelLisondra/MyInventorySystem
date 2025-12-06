import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemPriceHistorys,
  fetchItemPriceHistoryById,
  searchItemPriceHistorys,
  addItemPriceHistory,
  updateItemPriceHistoryById,
  deleteItemPriceHistoryById,
} from "../features/itemPriceHistory/itemPriceHistoryThunk";
import type { ItemPriceHistory } from "../types/itemPriceHistory";

interface UseItemPriceHistoryReturn {
    itemPriceHistory: ItemPriceHistory[];
    selectedItemPriceHistory: ItemPriceHistory | null;
    loading: boolean;
    error: string | null;
    addNewItemPriceHistory: (ItemPriceHistory: ItemPriceHistory) => void;
    updateItemPriceHistory: (ItemPriceHistory: ItemPriceHistory) => void;
    deleteItemPriceHistory: (id: number) => void;
    reloadItemPriceHistorys: () => void;
    getItemPriceHistoryById: (id: number) => void;
    searchItemPriceHistory: (name: string) => void;
}

export const useItemPriceHistory = (): UseItemPriceHistoryReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { itemPriceHistory, selectedItemPriceHistory, loading, error } = useSelector(
      (state: RootState) => state.itemPriceHistory
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchItemPriceHistorys());
    }, [dispatch]);

    const addNewItemPriceHistory = useCallback(
      (ItemPriceHistory: ItemPriceHistory) => {
        dispatch(addItemPriceHistory(ItemPriceHistory));
      },
      [dispatch]
    );

    const updateItemPriceHistory = useCallback(
      (ItemPriceHistory: ItemPriceHistory) => {
        dispatch(updateItemPriceHistoryById(ItemPriceHistory));
      },
      [dispatch]
    );

    const deleteItemPriceHistory = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this ItemPriceHistory?")) {
          dispatch(deleteItemPriceHistoryById(id));
        }
      },
      [dispatch]
    );

    const reloadItemPriceHistorys = useCallback(() => {
      dispatch(fetchItemPriceHistorys());
    }, [dispatch]);

    const getItemPriceHistoryById = useCallback(
      (id: number) => {
        dispatch(fetchItemPriceHistoryById(id));
      },
      [dispatch]
    );

    const searchItemPriceHistory = useCallback((keyword: string) => {
        dispatch(searchItemPriceHistorys(keyword));
      },
      [dispatch]
    );

    return {
      itemPriceHistory,
      selectedItemPriceHistory,
      loading,
      error,
      addNewItemPriceHistory,
      updateItemPriceHistory,
      deleteItemPriceHistory,
      reloadItemPriceHistorys,
      getItemPriceHistoryById,
      searchItemPriceHistory,
    };
};
