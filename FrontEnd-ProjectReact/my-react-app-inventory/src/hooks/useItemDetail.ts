import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemDetails,
  fetchItemDetailById,
  searchItemDetails,
  addItemDetail,
  updateItemDetailById,
  deleteItemDetailById,
} from "../features/itemDetail/itemDetailThunk";
import type { ItemDetail } from "../types/itemDetail";

interface UseItemDetailReturn {
  itemDetails: ItemDetail[];
  selectedItemDetail: ItemDetail | null;
  loading: boolean;
  error: string | null;
  addNewItemDetail: (Item: ItemDetail) => void;
  updateItemDetail: (Item: ItemDetail) => void;
  deleteItemDetail: (id: number) => void;
  reloadItemDetails: () => void;
  getItemDetailById: (id: number) => void;
  searchItemDetail: (name: string) => void;
}

export const useItemDetail = (): UseItemDetailReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { itemDetails, selectedItemDetail, loading, error } = useSelector(
    (state: RootState) => state.itemDetail
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchItemDetails());
  }, [dispatch]);

  const addNewItemDetail = useCallback(
    (itemDetail: ItemDetail) => {
      dispatch(addItemDetail(itemDetail));
    },
    [dispatch]
  );

  const updateItemDetail = useCallback(
    (itemDetail: ItemDetail) => {
      dispatch(updateItemDetailById(itemDetail));
    },
    [dispatch]
  );

  const deleteItemDetail = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Item?")) {
        dispatch(deleteItemDetailById(id));
      }
    },
    [dispatch]
  );

  const reloadItemDetails = useCallback(() => {
    dispatch(fetchItemDetails());
  }, [dispatch]);

  const getItemDetailById = useCallback(
    (id: number) => {
      dispatch(fetchItemDetailById(id));
    },
    [dispatch]
  );

  const searchItemDetail = useCallback((keyword: string) => {
      dispatch(searchItemDetails(keyword));
    },
    [dispatch]
  );

  return {
    itemDetails,
    selectedItemDetail,
    loading,
    error,
    addNewItemDetail,
    updateItemDetail,
    deleteItemDetail,
    reloadItemDetails,
    getItemDetailById,
    searchItemDetail,
  };
};
