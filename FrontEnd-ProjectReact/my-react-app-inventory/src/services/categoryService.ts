import { CATEGORY_API } from "../constants/api";
import type { Category } from "../types/category";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getCategory = async (): Promise<Category[]> => {
  return authFetch(CATEGORY_API);
};

export const getCategoryPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<Category[]>> => {
  return authFetch(`${CATEGORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getCategoryById = async (id: number): Promise<Category> => {
  return authFetch(`${CATEGORY_API}/${id}`);
};

export const createCategory = async (Category: Category): Promise<Category> => {
  return authFetch(CATEGORY_API, {
    method: "POST",
    body: JSON.stringify(Category),
  });
};

export const updateCategory = async (Category: Category): Promise<void> => {
  await authFetch(`${CATEGORY_API}/${Category.id}`, {
    method: "PUT",
    body: JSON.stringify(Category),
  });
};

export const deleteCategory = async (id: number): Promise<void> => {
  await authFetch(`${CATEGORY_API}/${id}`, { method: "DELETE" });
};

export const searchCategory = async (keyword: string): Promise<Category[]> => {
  return authFetch(`${CATEGORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
