import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchstockTransfers,
  fetchstockTransferById,
  searchstockTransfers,
  addstockTransfer,
  updatestockTransferById,
  deletestockTransferById,
} from "../features/stockTransfer/stockTransferThunk";
import type { StockTransfer } from "../types/stockTransfer";

interface UsestockTransferReturn {
  stockTransfer: StockTransfer[];
  selectedStockTransfer: StockTransfer | null;
  loading: boolean;
  error: string | null;
  addNewstockTransfer: (stockTransfer: StockTransfer) => void;
  updatestockTransfer: (stockTransfer: StockTransfer) => void;
  deletestockTransfer: (id: number) => void;
  reloadstockTransfers: () => void;
  getstockTransferById: (id: number) => void;
  searchstockTransfer: (name: string) => void;
}

export const usestockTransfer = (): UsestockTransferReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { stockTransfer, selectedStockTransfer, loading, error } = useSelector(
    (state: RootState) => state.stockTransfer
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchstockTransfers());
  }, [dispatch]);

  const addNewstockTransfer = useCallback(
    (stockTransfer: StockTransfer) => {
      dispatch(addstockTransfer(stockTransfer));
    },
    [dispatch]
  );

  const updatestockTransfer = useCallback(
    (stockTransfer: StockTransfer) => {
      dispatch(updatestockTransferById(stockTransfer));
    },
    [dispatch]
  );

  const deletestockTransfer = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this stockTransfer?")) {
        dispatch(deletestockTransferById(id));
      }
    },
    [dispatch]
  );

  const reloadstockTransfers = useCallback(() => {
    dispatch(fetchstockTransfers());
  }, [dispatch]);

  const getstockTransferById = useCallback(
    (id: number) => {
      dispatch(fetchstockTransferById(id));
    },
    [dispatch]
  );

  const searchstockTransfer = useCallback(
    (keyword: string) => {
      dispatch(searchstockTransfers(keyword));
    },
    [dispatch]
  );

  return {
    stockTransfer,
    selectedStockTransfer,
    loading,
    error,
    addNewstockTransfer,
    updatestockTransfer,
    deletestockTransfer,
    reloadstockTransfers,
    getstockTransferById,
    searchstockTransfer,
  };
};
