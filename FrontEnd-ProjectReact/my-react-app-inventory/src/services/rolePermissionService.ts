import { ROLE_PERMISSION_API } from "../constants/api";
import type { RolePermission } from "../types/rolePermission";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getRolePermissions = async (): Promise<RolePermission[]> => {
  return authFetch(ROLE_PERMISSION_API);
};

export const getRolePermissionPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<RolePermission[]>> => {
  return authFetch(`${ROLE_PERMISSION_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getRolePermissionById = async (id: number): Promise<RolePermission> => {
  return authFetch(`${ROLE_PERMISSION_API}/${id}`);
};

export const createRolePermission = async (role: RolePermission): Promise<RolePermission> => {
  return authFetch(ROLE_PERMISSION_API, {
    method: "POST",
    body: JSON.stringify(role),
  });
};

export const updateRolePermission = async (role: RolePermission): Promise<void> => {
  await authFetch(`${ROLE_PERMISSION_API}/${role.id}`, {
    method: "PUT",
    body: JSON.stringify(role),
  });
};

export const deleteRolePermission = async (id: number): Promise<void> => {
  await authFetch(`${ROLE_PERMISSION_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchRolePermissions = async (keyword: string): Promise<RolePermission[]> => {
  return authFetch(`${ROLE_PERMISSION_API}/search?keyword=${encodeURIComponent(keyword)}`);
};