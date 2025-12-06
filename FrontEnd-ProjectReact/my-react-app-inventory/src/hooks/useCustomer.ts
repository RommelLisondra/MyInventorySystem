import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchCustomers,
  fetchCustomerById,
  searchCustomers,
  addCustomer,
  updateCustomerById,
  deleteCustomerById,
} from "../features/customers/customerThunk";
import type { Customer } from "../types/customer";

interface UseCustomerReturn {
  customers: Customer[];
  selectedCustomer: Customer | null;
  loading: boolean;
  error: string | null;
  addNewCustomer: (customer: Customer) => void;
  updateCustomer: (customer: Customer) => void;
  deleteCustomer: (id: number) => void;
  reloadCustomers: () => void;
  getCustomerById: (id: number) => void;
  searchCustomer: (name: string) => void;
}

export const useCustomer = (): UseCustomerReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { customers, selectedCustomer, loading, error } = useSelector(
    (state: RootState) => state.customer
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchCustomers());
  }, [dispatch]);

  const addNewCustomer = useCallback(
    (customer: Customer) => {
      dispatch(addCustomer(customer));
    },
    [dispatch]
  );

  const updateCustomer = useCallback(
    (customer: Customer) => {
      dispatch(updateCustomerById(customer));
    },
    [dispatch]
  );

  const deleteCustomer = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this customer?")) {
        dispatch(deleteCustomerById(id));
      }
    },
    [dispatch]
  );

  const reloadCustomers = useCallback(() => {
    dispatch(fetchCustomers());
  }, [dispatch]);

  const getCustomerById = useCallback(
    (id: number) => {
      dispatch(fetchCustomerById(id));
    },
    [dispatch]
  );

  const searchCustomer = useCallback((keyword: string) => {
      dispatch(searchCustomers(keyword));
    },
    [dispatch]
  );

  return {
    customers,
    selectedCustomer,
    loading,
    error,
    addNewCustomer,
    updateCustomer,
    deleteCustomer,
    reloadCustomers,
    getCustomerById,
    searchCustomer,
  };
};
