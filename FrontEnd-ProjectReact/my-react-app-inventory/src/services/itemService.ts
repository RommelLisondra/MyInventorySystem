import { ITEM_API } from "../constants/api";
import type { Item } from "../types/item";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getItems = async (): Promise<Item[]> => {
  return authFetch(ITEM_API);
};

export const getItemsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<Item[]>> => {
  return authFetch(`${ITEM_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemById = async (id: number): Promise<Item> => {
  return authFetch(`${ITEM_API}/${id}`);;
};

export const createItem = async (item: Item): Promise<Item> => {
  return authFetch(ITEM_API, {
    method: "POST",
    body: JSON.stringify(item),
  });
};

export const updateItem = async (item: Item): Promise<void> => {
  await authFetch(`${ITEM_API}/${item.id}`, {
    method: "PUT",
    body: JSON.stringify(item),
  });
};

export const deleteItem = async (id: number): Promise<void> => {
 await authFetch(`${ITEM_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchItems = async (keyword: string): Promise<Item[]> => {
  return authFetch(`${ITEM_API}/search?keyword=${encodeURIComponent(keyword)}`);
};