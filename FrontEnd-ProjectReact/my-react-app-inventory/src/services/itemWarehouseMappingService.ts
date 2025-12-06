import { ITEM_WAREHOUSE_API } from "../constants/api";
import type { ItemWarehouseMapping } from "../types/itemWarehouse";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getItemWarehouseMappings = async (): Promise<ItemWarehouseMapping[]> => {
  return authFetch(ITEM_WAREHOUSE_API);
};

export const getItemWarehouseMappingPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<ItemWarehouseMapping[]>> => {
  return authFetch(`${ITEM_WAREHOUSE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemWarehouseMappingById = async (id: number): Promise<ItemWarehouseMapping> => {
  return authFetch(`${ITEM_WAREHOUSE_API}/${id}`);
};

export const createItemWarehouseMapping = async (ItemWarehouseMapping: ItemWarehouseMapping): Promise<ItemWarehouseMapping> => {
  return authFetch(ITEM_WAREHOUSE_API, {
    method: "POST",
    body: JSON.stringify(ItemWarehouseMapping),
  });
};

export const updateItemWarehouseMapping = async (ItemWarehouseMapping: ItemWarehouseMapping): Promise<void> => {
  await authFetch(`${ITEM_WAREHOUSE_API}/${ItemWarehouseMapping.id}`, {
    method: "PUT",
    body: JSON.stringify(ItemWarehouseMapping),
  });
};

export const deleteItemWarehouseMapping = async (id: number): Promise<void> => {
  await authFetch(`${ITEM_WAREHOUSE_API}/${id}`, { method: "DELETE" });
};

export const searchItemWarehouseMapping = async (keyword: string): Promise<ItemWarehouseMapping[]> => {
  return authFetch(`${ITEM_WAREHOUSE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
