import type { RootState } from "../../app/store";

export const selectBranch = (state: RootState) => state.branch.branch;
export const selectBranchLoading = (state: RootState) => state.branch.loading;
export const selectBranchError = (state: RootState) => state.branch.error;

// Optional: selector para sa isang customer by id
export const selectBranchById = (id: number) => (state: RootState) =>
  state.branch.branch.find((c) => c.id === id);
