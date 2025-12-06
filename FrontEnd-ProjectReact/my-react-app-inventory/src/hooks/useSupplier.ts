import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchSuppliers,
  fetchSupplierById,
  searchSuppliers,
  addSupplier,
  updateSupplierById,
  deleteSupplierById,
} from "../features/supplier/supplierThunk";
import type { Supplier } from "../types/supplier";

interface UseSupplierReturn {
  suppliers: Supplier[];
  selectedSupplier: Supplier | null;
  loading: boolean;
  error: string | null;
  addNewSupplier: (Supplier: Supplier) => void;
  updateSupplier: (Supplier: Supplier) => void;
  deleteSupplier: (id: number) => void;
  reloadSuppliers: () => void;
  getSupplierById: (id: number) => void;
  searchSupplier: (name: string) => void;
}

export const useSupplier = (): UseSupplierReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { suppliers, selectedSupplier, loading, error } = useSelector(
    (state: RootState) => state.supplier
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchSuppliers());
  }, [dispatch]);

  const addNewSupplier = useCallback(
    (Supplier: Supplier) => {
      dispatch(addSupplier(Supplier));
    },
    [dispatch]
  );

  const updateSupplier = useCallback(
    (Supplier: Supplier) => {
      dispatch(updateSupplierById(Supplier));
    },
    [dispatch]
  );

  const deleteSupplier = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Supplier?")) {
        dispatch(deleteSupplierById(id));
      }
    },
    [dispatch]
  );

  const reloadSuppliers = useCallback(() => {
    dispatch(fetchSuppliers());
  }, [dispatch]);

  const getSupplierById = useCallback(
    (id: number) => {
      dispatch(fetchSupplierById(id));
    },
    [dispatch]
  );

  const searchSupplier = useCallback(
    (keyword: string) => {
      dispatch(searchSuppliers(keyword));
    },
    [dispatch]
  );

  return {
    suppliers,
    selectedSupplier,
    loading,
    error,
    addNewSupplier,
    updateSupplier,
    deleteSupplier,
    reloadSuppliers,
    getSupplierById,
    searchSupplier,
  };
};
