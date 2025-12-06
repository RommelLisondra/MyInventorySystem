import { ITEM_PRICEHISTORY_API } from "../constants/api";
import type { ItemPriceHistory } from "../types/itemPriceHistory";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getItemPriceHistory = async (): Promise<ItemPriceHistory[]> => {
  return authFetch(ITEM_PRICEHISTORY_API);
};

export const getItemPriceHistorysPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<ItemPriceHistory[]>> => {
  return authFetch(`${ITEM_PRICEHISTORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemPriceHistoryById = async (id: number): Promise<ItemPriceHistory> => {
  return authFetch(`${ITEM_PRICEHISTORY_API}/${id}`);
};

export const createItemPriceHistory = async (ItemPriceHistory: ItemPriceHistory): Promise<ItemPriceHistory> => {
  return authFetch(ITEM_PRICEHISTORY_API, {
    method: "POST",
    body: JSON.stringify(ItemPriceHistory),
  });
};

export const updateItemPriceHistory = async (ItemPriceHistory: ItemPriceHistory): Promise<void> => {
  await authFetch(`${ITEM_PRICEHISTORY_API}/${ItemPriceHistory.id}`, {
    method: "PUT",
    body: JSON.stringify(ItemPriceHistory),
  });
};

export const deleteItemPriceHistory = async (id: number): Promise<void> => {
  await authFetch(`${ITEM_PRICEHISTORY_API}/${id}`, { method: "DELETE" });
};

export const searchItemPriceHistory = async (keyword: string): Promise<ItemPriceHistory[]> => {
  return authFetch(`${ITEM_PRICEHISTORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
