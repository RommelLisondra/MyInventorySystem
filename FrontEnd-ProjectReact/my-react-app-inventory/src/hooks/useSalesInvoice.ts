import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchSalesInvoices,
  fetchSalesInvoiceById,
  searchSalesInvoices,
  addSalesInvoice,
  updateSalesInvoiceById,
  deleteSalesInvoiceById,
} from "../features/salesInvoice/salesInvoiceThunk";
import type { SalesInvoiceMaster } from "../types/salesInvoiceMaster";

interface UseSalesInvoiceReturn {
  salesInvoice: SalesInvoiceMaster[];
  selectedSalesInvoiceMaster: SalesInvoiceMaster | null;
  loading: boolean;
  error: string | null;
  addNewSalesInvoice: (SalesInvoice: SalesInvoiceMaster) => void;
  updateSalesInvoice: (SalesInvoice: SalesInvoiceMaster) => void;
  deleteSalesInvoice: (id: number) => void;
  reloadSalesInvoices: () => void;
  getSalesInvoiceById: (id: number) => void;
  searchSalesInvoice: (name: string) => void;
}

export const useSalesInvoice = (): UseSalesInvoiceReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { salesInvoice, selectedSalesInvoiceMaster, loading, error } = useSelector(
    (state: RootState) => state.salesInvoice
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchSalesInvoices());
  }, [dispatch]);

  const addNewSalesInvoice = useCallback(
    (salesInvoice: SalesInvoiceMaster) => {
      dispatch(addSalesInvoice(salesInvoice));
    },
    [dispatch]
  );

  const updateSalesInvoice = useCallback(
    (salesInvoice: SalesInvoiceMaster) => {
      dispatch(updateSalesInvoiceById(salesInvoice));
    },
    [dispatch]
  );

  const deleteSalesInvoice = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this SalesInvoice?")) {
        dispatch(deleteSalesInvoiceById(id));
      }
    },
    [dispatch]
  );

  const reloadSalesInvoices = useCallback(() => {
    dispatch(fetchSalesInvoices());
  }, [dispatch]);

  const getSalesInvoiceById = useCallback(
    (id: number) => {
      dispatch(fetchSalesInvoiceById(id));
    },
    [dispatch]
  );

  const searchSalesInvoice = useCallback(
    (keyword: string) => {
      dispatch(searchSalesInvoices(keyword));
    },
    [dispatch]
  );

  return {
    salesInvoice,
    selectedSalesInvoiceMaster,
    loading,
    error,
    addNewSalesInvoice,
    updateSalesInvoice,
    deleteSalesInvoice,
    reloadSalesInvoices,
    getSalesInvoiceById,
    searchSalesInvoice,
  };
};
