import { ITEMIMAGE_API } from "../constants/api";
import type { ItemImage } from "../types/itemImage";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getItemImages = async (): Promise<ItemImage[]> => {
  return authFetch(ITEMIMAGE_API);
};

export const getItemImagesPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<ItemImage[]>> => {
  return authFetch(`${ITEMIMAGE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemImageById = async (id: number): Promise<ItemImage> => {
  return authFetch(`${ITEMIMAGE_API}/${id}`);
};

export const createItemImage = async (itemImage: ItemImage): Promise<ItemImage> => {
  return authFetch(ITEMIMAGE_API, {
    method: "POST",
    body: JSON.stringify(itemImage),
  });
};

export const updateItemImage = async (itemImage: ItemImage): Promise<void> => {
  await authFetch(`${ITEMIMAGE_API}/${itemImage.id}`, {
    method: "PUT",
    body: JSON.stringify(itemImage),
  });
};

export const deleteItemImage = async (id: number): Promise<void> => {
  await authFetch(`${ITEMIMAGE_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchItemImages = async (keyword: string): Promise<ItemImage[]> => {
  return authFetch(`${ITEMIMAGE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};