import type { RootState } from "../../app/store";

export const selectAccounts = (state: RootState) => state.account.account;
export const selectAccountLoading = (state: RootState) => state.account.loading;
export const selectAccountError = (state: RootState) => state.account.error;

// Optional: selector para sa isang customer by id
export const selectAccountById = (id: number) => (state: RootState) =>
  state.account.account.find((c) => c.id === id);
