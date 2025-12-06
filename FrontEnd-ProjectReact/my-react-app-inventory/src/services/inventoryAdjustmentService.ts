import { INVENTORY_ADJUSTMENT_API } from "../constants/api";
import { authFetch } from "./authService";
import type { InventoryAdjustment } from "../types/inventoryAdjustment";
import type { PagedResponse } from "../types/pagedResponse";

export const getInventoryAdjustments = async (): Promise<InventoryAdjustment[]> => {
  return authFetch(INVENTORY_ADJUSTMENT_API);
};

export const getInventoryAdjustmentsPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<InventoryAdjustment[]>> => {
  return authFetch(`${INVENTORY_ADJUSTMENT_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getInventoryAdjustmentById = async (id: number): Promise<InventoryAdjustment> => {
  return authFetch(`${INVENTORY_ADJUSTMENT_API}/${id}`);
};

export const createInventoryAdjustment = async (inventoryAdjustment: InventoryAdjustment): Promise<InventoryAdjustment> => {
  return authFetch(INVENTORY_ADJUSTMENT_API, {
    method: "POST",
    body: JSON.stringify(inventoryAdjustment),
  });
};

export const updateInventoryAdjustment = async (inventoryAdjustment: InventoryAdjustment): Promise<void> => {
  await authFetch(`${INVENTORY_ADJUSTMENT_API}/${inventoryAdjustment.id}`, {
    method: "PUT",
    body: JSON.stringify(inventoryAdjustment),
  });
};

export const deleteInventoryAdjustment = async (id: number): Promise<void> => {
  await authFetch(`${INVENTORY_ADJUSTMENT_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchInventoryAdjustments = async (keyword: string): Promise<InventoryAdjustment[]> => {
  return authFetch(`${INVENTORY_ADJUSTMENT_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
