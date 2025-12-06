import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchSalesOrders,
  fetchSalesOrderById,
  searchSalesOrders,
  addSalesOrder,
  updateSalesOrderById,
  deleteSalesOrderById,
} from "../features/salesOrder/salesOrderThunk";
import type { SalesOrderMaster } from "../types/salesOrderMaster";

interface UseSalesOrderReturn {
  salesOrder: SalesOrderMaster[];
  selectedSalesOrder: SalesOrderMaster | null;
  loading: boolean;
  error: string | null;
  addNewSalesOrder: (SalesOrder: SalesOrderMaster) => void;
  updateSalesOrder: (SalesOrder: SalesOrderMaster) => void;
  deleteSalesOrder: (id: number) => void;
  reloadSalesOrders: () => void;
  getSalesOrderById: (id: number) => void;
  searchSalesOrder: (name: string) => void;
}

export const useSalesOrder = (): UseSalesOrderReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { salesOrder, selectedSalesOrder, loading, error } = useSelector(
    (state: RootState) => state.salesOrder
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchSalesOrders());
  }, [dispatch]);

  const addNewSalesOrder = useCallback(
    (salesOrder: SalesOrderMaster) => {
      dispatch(addSalesOrder(salesOrder));
    },
    [dispatch]
  );

  const updateSalesOrder = useCallback(
    (salesOrder: SalesOrderMaster) => {
      dispatch(updateSalesOrderById(salesOrder));
    },
    [dispatch]
  );

  const deleteSalesOrder = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this SalesOrder?")) {
        dispatch(deleteSalesOrderById(id));
      }
    },
    [dispatch]
  );

  const reloadSalesOrders = useCallback(() => {
    dispatch(fetchSalesOrders());
  }, [dispatch]);

  const getSalesOrderById = useCallback(
    (id: number) => {
      dispatch(fetchSalesOrderById(id));
    },
    [dispatch]
  );

  const searchSalesOrder = useCallback(
    (keyword: string) => {
      dispatch(searchSalesOrders(keyword));
    },
    [dispatch]
  );

  return {
    salesOrder,
    selectedSalesOrder,
    loading,
    error,
    addNewSalesOrder,
    updateSalesOrder,
    deleteSalesOrder,
    reloadSalesOrders,
    getSalesOrderById,
    searchSalesOrder,
  };
};
