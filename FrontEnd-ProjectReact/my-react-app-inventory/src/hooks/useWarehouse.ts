import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchWarehouses,
  fetchWarehouseById,
  searchWarehouses,
  addWarehouse,
  updateWarehouseById,
  deleteWarehouseById,
} from "../features/warehouse/wareHouseThunks";
import type { Warehouse } from "../types/warehouse";

interface UseWarehouseReturn {
  warehouses: Warehouse[];
  selectedWarehouse: Warehouse | null;
  loading: boolean;
  error: string | null;
  addNewWarehouse: (Warehouse: Warehouse) => void;
  updateWarehouse: (Warehouse: Warehouse) => void;
  deleteWarehouse: (id: number) => void;
  reloadWarehouses: () => void;
  getWarehouseById: (id: number) => void;
  searchWarehouse: (name: string) => void;
}

export const useWarehouse = (): UseWarehouseReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { warehouses, selectedWarehouse, loading, error } = useSelector(
    (state: RootState) => state.warehouse
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchWarehouses());
  }, [dispatch]);

  const addNewWarehouse = useCallback(
    (Warehouse: Warehouse) => {
      dispatch(addWarehouse(Warehouse));
    },
    [dispatch]
  );

  const updateWarehouse = useCallback(
    (Warehouse: Warehouse) => {
      dispatch(updateWarehouseById(Warehouse));
    },
    [dispatch]
  );

  const deleteWarehouse = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Warehouse?")) {
        dispatch(deleteWarehouseById(id));
      }
    },
    [dispatch]
  );

  const reloadWarehouses = useCallback(() => {
    dispatch(fetchWarehouses());
  }, [dispatch]);

  const getWarehouseById = useCallback(
    (id: number) => {
      dispatch(fetchWarehouseById(id));
    },
    [dispatch]
  );

  const searchWarehouse = useCallback(
    (keyword: string) => {
      dispatch(searchWarehouses(keyword));
    },
    [dispatch]
  );

  return {
    warehouses,
    selectedWarehouse,
    loading,
    error,
    addNewWarehouse,
    updateWarehouse,
    deleteWarehouse,
    reloadWarehouses,
    getWarehouseById,
    searchWarehouse,
  };
};
