import { BRANCH_API } from "../constants/api";
import type { Branch } from "../types/branch";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getBranchs = async (): Promise<Branch[]> => {
  return authFetch(BRANCH_API);
};

export const getBranchPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<Branch[]>> => {
  return authFetch(`${BRANCH_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getBranchById = async (id: number): Promise<Branch> => {
  return authFetch(`${BRANCH_API}/${id}`);
};

export const createBranch = async (Branch: Branch): Promise<Branch> => {
  return authFetch(BRANCH_API, {
    method: "POST",
    body: JSON.stringify(Branch),
  });
};

export const updateBranch = async (Branch: Branch): Promise<void> => {
  await authFetch(`${BRANCH_API}/${Branch.id}`, {
    method: "PUT",
    body: JSON.stringify(Branch),
  });
};

export const deleteBranch = async (id: number): Promise<void> => {
  await authFetch(`${BRANCH_API}/${id}`, { method: "DELETE" });
};

export const searchBranch = async (keyword: string): Promise<Branch[]> => {
  return authFetch(`${BRANCH_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
