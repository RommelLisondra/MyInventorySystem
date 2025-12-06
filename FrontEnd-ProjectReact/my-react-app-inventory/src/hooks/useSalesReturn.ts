import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchsalesReturns,
  fetchsalesReturnById,
  searchsalesReturns,
  addsalesReturn,
  updatesalesReturnById,
  deletesalesReturnById,
} from "../features/salesReturn/salesReturnThunk";
import type { SalesReturnMaster } from "../types/salesReturnMaster";

interface UsesalesReturnReturn {
  salesReturn: SalesReturnMaster[];
  selectedSalesReturnMaster: SalesReturnMaster | null;
  loading: boolean;
  error: string | null;
  addNewsalesReturn: (salesReturn: SalesReturnMaster) => void;
  updatesalesReturn: (salesReturn: SalesReturnMaster) => void;
  deletesalesReturn: (id: number) => void;
  reloadsalesReturns: () => void;
  getsalesReturnById: (id: number) => void;
  searchsalesReturn: (name: string) => void;
}

export const usesalesReturn = (): UsesalesReturnReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { salesReturn, selectedSalesReturnMaster, loading, error } = useSelector(
    (state: RootState) => state.salesReturn
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchsalesReturns());
  }, [dispatch]);

  const addNewsalesReturn = useCallback(
    (salesReturn: SalesReturnMaster) => {
      dispatch(addsalesReturn(salesReturn));
    },
    [dispatch]
  );

  const updatesalesReturn = useCallback(
    (salesReturn: SalesReturnMaster) => {
      dispatch(updatesalesReturnById(salesReturn));
    },
    [dispatch]
  );

  const deletesalesReturn = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this salesReturn?")) {
        dispatch(deletesalesReturnById(id));
      }
    },
    [dispatch]
  );

  const reloadsalesReturns = useCallback(() => {
    dispatch(fetchsalesReturns());
  }, [dispatch]);

  const getsalesReturnById = useCallback(
    (id: number) => {
      dispatch(fetchsalesReturnById(id));
    },
    [dispatch]
  );

  const searchsalesReturn = useCallback(
    (keyword: string) => {
      dispatch(searchsalesReturns(keyword));
    },
    [dispatch]
  );

  return {
    salesReturn,
    selectedSalesReturnMaster,
    loading,
    error,
    addNewsalesReturn,
    updatesalesReturn,
    deletesalesReturn,
    reloadsalesReturns,
    getsalesReturnById,
    searchsalesReturn,
  };
};
