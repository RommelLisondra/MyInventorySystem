import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchUserAccounts,
  fetchUserAccountById,
  searchUserAccounts,
  addUserAccount,
  updateUserAccountById,
  deleteUserAccountById,
} from "../features/userAccounts/userAccountsThunk";
import type { UserAccount } from "../types/userAccount";

interface UseUserAccountReturn {
  userAccounts: UserAccount[];
  selectedUserAccount: UserAccount | null;
  loading: boolean;
  error: string | null;
  addNewUserAccount: (UserAccount: UserAccount) => void;
  updateUserAccount: (UserAccount: UserAccount) => void;
  deleteUserAccount: (id: number) => void;
  reloadUserAccounts: () => void;
  getUserAccountById: (id: number) => void;
  searchUserAccount: (name: string) => void;
}

export const useUserAccount = (): UseUserAccountReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { userAccounts, selectedUserAccount, loading, error } = useSelector(
    (state: RootState) => state.userAccounts
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchUserAccounts());
  }, [dispatch]);

  const addNewUserAccount = useCallback(
    (userAccount: UserAccount) => {
      dispatch(addUserAccount(userAccount));
    },
    [dispatch]
  );

  const updateUserAccount = useCallback(
    (userAccount: UserAccount) => {
      dispatch(updateUserAccountById(userAccount));
    },
    [dispatch]
  );

  const deleteUserAccount = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this UserAccount?")) {
        dispatch(deleteUserAccountById(id));
      }
    },
    [dispatch]
  );

  const reloadUserAccounts = useCallback(() => {
    dispatch(fetchUserAccounts());
  }, [dispatch]);

  const getUserAccountById = useCallback(
    (id: number) => {
      dispatch(fetchUserAccountById(id));
    },
    [dispatch]
  );

  const searchUserAccount = useCallback(
    (keyword: string) => {
      dispatch(searchUserAccounts(keyword));
    },
    [dispatch]
  );

  return {
    userAccounts,
    selectedUserAccount,
    loading,
    error,
    addNewUserAccount,
    updateUserAccount,
    deleteUserAccount,
    reloadUserAccounts,
    getUserAccountById,
    searchUserAccount,
  };
};
