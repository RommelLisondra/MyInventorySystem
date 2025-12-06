import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemBarcodes,
  fetchItemBarcodeById,
  searchItemBarcodes,
  addItemBarcode,
  updateItemBarcodeById,
  deleteItemBarcodeById,
} from "../features/itemBarcode/itemBarcodeThunk";
import type { ItemBarcode } from "../types/itemBarcode";

interface UseItemBarcodeReturn {
    itemBarcode: ItemBarcode[];
    selectedItemBarcode: ItemBarcode | null;
    loading: boolean;
    error: string | null;
    addNewItemBarcode: (itemBarcode: ItemBarcode) => void;
    updateItemBarcode: (itemBarcode: ItemBarcode) => void;
    deleteItemBarcode: (id: number) => void;
    reloadItemBarcodes: () => void;
    getItemBarcodeById: (id: number) => void;
    searchItemBarcode: (name: string) => void;
}

export const useItemBarcode = (): UseItemBarcodeReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { itemBarcode, selectedItemBarcode, loading, error } = useSelector(
      (state: RootState) => state.itemBarcode
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchItemBarcodes());
    }, [dispatch]);

    const addNewItemBarcode = useCallback(
      (ItemBarcode: ItemBarcode) => {
        dispatch(addItemBarcode(ItemBarcode));
      },
      [dispatch]
    );

    const updateItemBarcode = useCallback(
      (ItemBarcode: ItemBarcode) => {
        dispatch(updateItemBarcodeById(ItemBarcode));
      },
      [dispatch]
    );

    const deleteItemBarcode = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this ItemBarcode?")) {
          dispatch(deleteItemBarcodeById(id));
        }
      },
      [dispatch]
    );

    const reloadItemBarcodes = useCallback(() => {
      dispatch(fetchItemBarcodes());
    }, [dispatch]);

    const getItemBarcodeById = useCallback(
      (id: number) => {
        dispatch(fetchItemBarcodeById(id));
      },
      [dispatch]
    );

    const searchItemBarcode = useCallback((keyword: string) => {
        dispatch(searchItemBarcodes(keyword));
      },
      [dispatch]
    );

    return {
      itemBarcode,
      selectedItemBarcode,
      loading,
      error,
      addNewItemBarcode,
      updateItemBarcode,
      deleteItemBarcode,
      reloadItemBarcodes,
      getItemBarcodeById,
      searchItemBarcode,
    };
};
