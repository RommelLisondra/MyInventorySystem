import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemImages,
  fetchItemImageById,
  searchItemImages,
  addItemImage,
  updateItemImageById,
  deleteItemImageById,
} from "../features/itemImage/itemImageThunk";
import type { ItemImage } from "../types/itemImage";

interface UseItemImageReturn {
  itemImages: ItemImage[];
  selectedItemImage: ItemImage | null;
  loading: boolean;
  error: string | null;
  addNewItemImage: (ItemImage: ItemImage) => void;
  updateItemImage: (ItemImage: ItemImage) => void;
  deleteItemImage: (id: number) => void;
  reloadItemImages: () => void;
  getItemImageById: (id: number) => void;
  searchItemImage: (name: string) => void;
}

export const useItemImage = (): UseItemImageReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { itemImages, selectedItemImage, loading, error } = useSelector(
    (state: RootState) => state.itemImage
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchItemImages());
  }, [dispatch]);

  const addNewItemImage = useCallback(
    (ItemImage: ItemImage) => {
      dispatch(addItemImage(ItemImage));
    },
    [dispatch]
  );

  const updateItemImage = useCallback(
    (ItemImage: ItemImage) => {
      dispatch(updateItemImageById(ItemImage));
    },
    [dispatch]
  );

  const deleteItemImage = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this ItemImage?")) {
        dispatch(deleteItemImageById(id));
      }
    },
    [dispatch]
  );

  const reloadItemImages = useCallback(() => {
    dispatch(fetchItemImages());
  }, [dispatch]);

  const getItemImageById = useCallback(
    (id: number) => {
      dispatch(fetchItemImageById(id));
    },
    [dispatch]
  );

  const searchItemImage = useCallback((keyword: string) => {
      dispatch(searchItemImages(keyword));
    },
    [dispatch]
  );

  return {
    itemImages,
    selectedItemImage,
    loading,
    error,
    addNewItemImage,
    updateItemImage,
    deleteItemImage,
    reloadItemImages,
    getItemImageById,
    searchItemImage,
  };
};
