import { WAREHOUSE_API } from "../constants/api";
import type { Warehouse } from "../types/warehouse";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getWarehouses = async (): Promise<Warehouse[]> => {
  return authFetch(WAREHOUSE_API);
};

export const getWarehousesPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<Warehouse[]>> => {
  return authFetch(`${WAREHOUSE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getWarehouseById = async (id: number): Promise<Warehouse> => {
  return authFetch(`${WAREHOUSE_API}/${id}`);
};

export const createWarehouse = async (warehouse: Warehouse): Promise<Warehouse> => {
  return authFetch(WAREHOUSE_API, {
    method: "POST",
    body: JSON.stringify(warehouse),
  });
};

export const updateWarehouse = async (warehouse: Warehouse): Promise<void> => {
  await authFetch(`${WAREHOUSE_API}/${warehouse.id}`, {
    method: "PUT",
    body: JSON.stringify(warehouse),
  });
};

export const deleteWarehouse = async (id: number): Promise<void> => {
  await authFetch(`${WAREHOUSE_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchWarehouses = async (keyword: string): Promise<Warehouse[]> => {
  return authFetch(`${WAREHOUSE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};