import type { RootState } from "../../app/store";

export const selectRolePermission = (state: RootState) => state.rolePermission.rolePermission;
export const selectRolePermissionLoading = (state: RootState) => state.rolePermission.loading;
export const selectRolePermissionError = (state: RootState) => state.rolePermission.error;

// Optional: selector para sa isang customer by id
export const selectRolePermissionById = (id: number) => (state: RootState) =>
  state.rolePermission.rolePermission.find((c) => c.id === id);
