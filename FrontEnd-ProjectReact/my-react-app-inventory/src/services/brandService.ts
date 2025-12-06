import { BRAND_API } from "../constants/api";
import type { Brand } from "../types/brand";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getBrands = async (): Promise<Brand[]> => {
  return authFetch(BRAND_API);
};

export const getBrandsPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<Brand[]>> => {
  return authFetch(`${BRAND_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getBrandById = async (id: number): Promise<Brand> => {
  return authFetch(`${BRAND_API}/${id}`);
};

export const createBrand = async (Brand: Brand): Promise<Brand> => {
  return authFetch(BRAND_API, {
    method: "POST",
    body: JSON.stringify(Brand),
  });
};

export const updateBrand = async (Brand: Brand): Promise<void> => {
  await authFetch(`${BRAND_API}/${Brand.id}`, {
    method: "PUT",
    body: JSON.stringify(Brand),
  });
};

export const deleteBrand = async (id: number): Promise<void> => {
  await authFetch(`${BRAND_API}/${id}`, { method: "DELETE" });
};

export const searchBrands = async (keyword: string): Promise<Brand[]> => {
  return authFetch(`${BRAND_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
