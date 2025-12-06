import { SUB_CATEGORY_API } from "../constants/api";
import type { SubCategory } from "../types/subCategory";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getSubCategory = async (): Promise<SubCategory[]> => {
  return authFetch(SUB_CATEGORY_API);
};

export const getSubCategoryPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<SubCategory[]>> => {
  return authFetch(`${SUB_CATEGORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSubCategoryById = async (id: number): Promise<SubCategory> => {
  return authFetch(`${SUB_CATEGORY_API}/${id}`);
};

export const createSubCategory = async (SubCategory: SubCategory): Promise<SubCategory> => {
  return authFetch(SUB_CATEGORY_API, {
    method: "POST",
    body: JSON.stringify(SubCategory),
  });
};

export const updateSubCategory = async (SubCategory: SubCategory): Promise<void> => {
  await authFetch(`${SUB_CATEGORY_API}/${SubCategory.id}`, {
    method: "PUT",
    body: JSON.stringify(SubCategory),
  });
};

export const deleteSubCategory = async (id: number): Promise<void> => {
  await authFetch(`${SUB_CATEGORY_API}/${id}`, { method: "DELETE" });
};

export const searchSubCategory = async (keyword: string): Promise<SubCategory[]> => {
  return authFetch(`${SUB_CATEGORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
