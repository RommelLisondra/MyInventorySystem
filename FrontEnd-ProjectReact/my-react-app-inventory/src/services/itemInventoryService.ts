import { ITEM_INVENTORY_API } from "../constants/api";
import type { ItemInventory } from "../types/itemInventory";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getItemInventorys = async (): Promise<ItemInventory[]> => {
  return authFetch(ITEM_INVENTORY_API);
};

export const getItemInventorysPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<ItemInventory[]>> => {
  return authFetch(`${ITEM_INVENTORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemInventoryById = async (id: number): Promise<ItemInventory> => {
  return authFetch(`${ITEM_INVENTORY_API}/${id}`);
};

export const createItemInventory = async (ItemInventory: ItemInventory): Promise<ItemInventory> => {
  return authFetch(ITEM_INVENTORY_API, {
    method: "POST",
    body: JSON.stringify(ItemInventory),
  });
};

export const updateItemInventory = async (ItemInventory: ItemInventory): Promise<void> => {
  await authFetch(`${ITEM_INVENTORY_API}/${ItemInventory.id}`, {
    method: "PUT",
    body: JSON.stringify(ItemInventory),
  });
};

export const deleteItemInventory = async (id: number): Promise<void> => {
  await authFetch(`${ITEM_INVENTORY_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchItemInventorys = async (keyword: string): Promise<ItemInventory[]> => {
  return authFetch(`${ITEM_INVENTORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};