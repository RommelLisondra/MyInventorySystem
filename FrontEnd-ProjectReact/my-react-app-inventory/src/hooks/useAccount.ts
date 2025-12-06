import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchAccount,
  fetchAccountById,
  searchAccount,
  addAccount,
  updateAccountById,
  deleteAccountById,
} from "../features/account/accountThunk";
import type { Account } from "../types/account";

interface UseAccountReturn {
    account: Account[];
    selectedAccount: Account | null;
    loading: boolean;
    error: string | null;
    addNewAccount: (Account: Account) => void;
    updateAccount: (Account: Account) => void;
    deleteAccount: (id: number) => void;
    reloadAccounts: () => void;
    getAccountById: (id: number) => void;
    searchAccounts: (name: string) => void;
}

export const useAccount = (): UseAccountReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { account, selectedAccount, loading, error } = useSelector(
      (state: RootState) => state.account
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchAccount());
    }, [dispatch]);

    const addNewAccount = useCallback(
      (Account: Account) => {
        dispatch(addAccount(Account));
      },
      [dispatch]
    );

    const updateAccount = useCallback(
      (Account: Account) => {
        dispatch(updateAccountById(Account));
      },
      [dispatch]
    );

    const deleteAccount = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this Account?")) {
          dispatch(deleteAccountById(id));
        }
      },
      [dispatch]
    );

    const reloadAccounts = useCallback(() => {
      dispatch(fetchAccount());
    }, [dispatch]);

    const getAccountById = useCallback(
      (id: number) => {
        dispatch(fetchAccountById(id));
      },
      [dispatch]
    );

    const searchAccounts = useCallback((keyword: string) => {
        dispatch(searchAccount(keyword));
      },
      [dispatch]
    );

    return {
      account,
      selectedAccount,
      loading,
      error,
      addNewAccount,
      updateAccount,
      deleteAccount,
      reloadAccounts,
      getAccountById,
      searchAccounts,
    };
};
