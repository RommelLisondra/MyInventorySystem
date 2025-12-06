import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchItemWarehouseMappings,
  fetchItemWarehouseMappingById,
  searchItemWarehouseMappings,
  addItemWarehouseMapping,
  updateItemWarehouseMappingById,
  deleteItemWarehouseMappingById,
} from "../features/itemWarehouseMapping/itemWarehouseMappingThunk";
import type { ItemWarehouseMapping } from "../types/itemWarehouse";

interface UseItemWarehouseMappingReturn {
    itemWarehouseMapping: ItemWarehouseMapping[];
    selectedItemWarehouseMapping: ItemWarehouseMapping | null;
    loading: boolean;
    error: string | null;
    addNewItemWarehouseMapping: (itemWarehouseMapping: ItemWarehouseMapping) => void;
    updateItemWarehouseMapping: (itemWarehouseMapping: ItemWarehouseMapping) => void;
    deleteItemWarehouseMapping: (id: number) => void;
    reloadItemWarehouseMappings: () => void;
    getItemWarehouseMappingById: (id: number) => void;
    searchItemWarehouseMapping: (name: string) => void;
}

export const useItemWarehouseMapping = (): UseItemWarehouseMappingReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { itemWarehouseMapping, selectedItemWarehouseMapping, loading, error } = useSelector(
      (state: RootState) => state.itemWarehouseMapping
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchItemWarehouseMappings());
    }, [dispatch]);

    const addNewItemWarehouseMapping = useCallback(
      (ItemWarehouseMapping: ItemWarehouseMapping) => {
        dispatch(addItemWarehouseMapping(ItemWarehouseMapping));
      },
      [dispatch]
    );

    const updateItemWarehouseMapping = useCallback(
      (ItemWarehouseMapping: ItemWarehouseMapping) => {
        dispatch(updateItemWarehouseMappingById(ItemWarehouseMapping));
      },
      [dispatch]
    );

    const deleteItemWarehouseMapping = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this ItemWarehouseMapping?")) {
          dispatch(deleteItemWarehouseMappingById(id));
        }
      },
      [dispatch]
    );

    const reloadItemWarehouseMappings = useCallback(() => {
      dispatch(fetchItemWarehouseMappings());
    }, [dispatch]);

    const getItemWarehouseMappingById = useCallback(
      (id: number) => {
        dispatch(fetchItemWarehouseMappingById(id));
      },
      [dispatch]
    );

    const searchItemWarehouseMapping = useCallback((keyword: string) => {
        dispatch(searchItemWarehouseMappings(keyword));
      },
      [dispatch]
    );

    return {
      itemWarehouseMapping,
      selectedItemWarehouseMapping,
      loading,
      error,
      addNewItemWarehouseMapping,
      updateItemWarehouseMapping,
      deleteItemWarehouseMapping,
      reloadItemWarehouseMappings,
      getItemWarehouseMappingById,
      searchItemWarehouseMapping,
    };
};
