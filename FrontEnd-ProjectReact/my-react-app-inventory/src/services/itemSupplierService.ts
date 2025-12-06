import { ITEM_SUPPLIER_API } from "../constants/api";
import type { ItemSupplier } from "../types/itemSupplier";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getItemSuppliers = async (): Promise<ItemSupplier[]> => {
  return authFetch(ITEM_SUPPLIER_API);
};

export const getItemSuppliersPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<ItemSupplier[]>> => {
  return authFetch(`${ITEM_SUPPLIER_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemSupplierById = async (id: number): Promise<ItemSupplier> => {
  return authFetch(`${ITEM_SUPPLIER_API}/${id}`);
};

export const createItemSupplier = async (itemSupplier: ItemSupplier): Promise<ItemSupplier> => {
  return authFetch(ITEM_SUPPLIER_API, {
    method: "POST",
    body: JSON.stringify(itemSupplier),
  });
};

export const updateItemSupplier = async (itemSupplier: ItemSupplier): Promise<void> => {
  await authFetch(`${ITEM_SUPPLIER_API}/${itemSupplier.id}`, {
    method: "PUT",
    body: JSON.stringify(itemSupplier),
  });
};

export const deleteItemSupplier = async (id: number): Promise<void> => {
  await authFetch(`${ITEM_SUPPLIER_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchItemSuppliers = async (keyword: string): Promise<ItemSupplier[]> => {
  return authFetch(`${ITEM_SUPPLIER_API}/search?keyword=${encodeURIComponent(keyword)}`);
};