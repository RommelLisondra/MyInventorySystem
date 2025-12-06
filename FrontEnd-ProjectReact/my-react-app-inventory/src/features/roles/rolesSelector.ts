import type { RootState } from "../../app/store";

export const selectRole = (state: RootState) => state.role.role;
export const selectRoleLoading = (state: RootState) => state.role.loading;
export const selectRoleError = (state: RootState) => state.role.error;

// Optional: selector para sa isang customer by id
export const selectRoleById = (id: number) => (state: RootState) =>
  state.role.role.find((c) => c.id === id);
