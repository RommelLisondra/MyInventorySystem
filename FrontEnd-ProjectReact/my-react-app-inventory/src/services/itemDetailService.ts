import { ITEMDETAIL_API } from "../constants/api";
import type { ItemDetail } from "../types/itemDetail";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getItemDetails = async (): Promise<ItemDetail[]> => {
  return authFetch(ITEMDETAIL_API);
};

export const getItemDetailsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<ItemDetail[]>> => {
  return authFetch(`${ITEMDETAIL_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemDetailById = async (id: number): Promise<ItemDetail> => {
  return authFetch(`${ITEMDETAIL_API}/${id}`);
};

export const createItemDetail = async (itemDetail: ItemDetail): Promise<ItemDetail> => {
  return authFetch(ITEMDETAIL_API, {
    method: "POST",
    body: JSON.stringify(itemDetail),
  });
};

export const updateItemDetail = async (itemDetail: ItemDetail): Promise<void> => {
  await authFetch(`${ITEMDETAIL_API}/${itemDetail.id}`, {
    method: "PUT",
    body: JSON.stringify(itemDetail),
  });
};

export const deleteItemDetail = async (id: number): Promise<void> => {
  await authFetch(`${ITEMDETAIL_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchItemDetails = async (keyword: string): Promise<ItemDetail[]> => {
  return authFetch(`${ITEMDETAIL_API}/search?keyword=${encodeURIComponent(keyword)}`);
};