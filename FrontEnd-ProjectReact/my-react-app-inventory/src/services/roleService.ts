import { ROLE_API } from "../constants/api";
import type { Role } from "../types/role";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getRoles = async (): Promise<Role[]> => {
  return authFetch(ROLE_API);
};

export const getRolesPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<Role[]>> => {
  return authFetch(`${ROLE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getRoleById = async (id: number): Promise<Role> => {
  return authFetch(`${ROLE_API}/${id}`);
};

export const createRole = async (role: Role): Promise<Role> => {
  return authFetch(ROLE_API, {
    method: "POST",
    body: JSON.stringify(role),
  });
};

export const updateRole = async (role: Role): Promise<void> => {
  await authFetch(`${ROLE_API}/${role.id}`, {
    method: "PUT",
    body: JSON.stringify(role),
  });
};

export const deleteRole = async (id: number): Promise<void> => {
  await authFetch(`${ROLE_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchRoles = async (keyword: string): Promise<Role[]> => {
  return authFetch(`${ROLE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};