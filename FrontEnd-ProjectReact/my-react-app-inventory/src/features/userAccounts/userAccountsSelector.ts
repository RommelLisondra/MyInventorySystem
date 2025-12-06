import type { RootState } from "../../app/store";

export const selectUserAccounts = (state: RootState) => state.userAccounts.userAccounts;
export const selectUserAccountsLoading = (state: RootState) => state.userAccounts.loading;
export const selecUserAccountsError = (state: RootState) => state.userAccounts.error;

// Optional: selector para sa isang customer by id
export const selectUserAccountsById = (id: number) => (state: RootState) =>
  state.userAccounts.userAccounts.find((c) => c.id === id);
